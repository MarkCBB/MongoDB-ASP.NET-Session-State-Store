using System;
using System.Globalization;
using System.Web.SessionState;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Linq;

namespace MongoSessionStateStore
{
    /// <summary>
    /// For further information about parameters see this page in the project wiki: 
    /// https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Web.config-parameters
    /// </summary>
    public sealed class MongoSessionStateStore : SessionStateStoreProviderBase
    {
        private SessionStateSection _config;
        private ConnectionStringSettings _connectionStringSettings;
        private string _applicationName;
        private string _connectionString;
        private string _databaseName;
        private string _collectionName;
        private bool _writeExceptionsToEventLog;
        internal const string EXCEPTION_MESSAGE = "An exception occurred. Please contact your administrator.";
        internal const string EVENT_SOURCE = "MongoSessionStateStore";
        internal const string EVENT_LOG = "Application";
        private int _maxUpsertAttempts = 220;
        private int _msWaitingForAttempt = 500;
        private bool _autoCreateTTLIndex = true;
        private MongoDatabaseSettings _databaseSettings;

        /// <summary>
        /// The ApplicationName property is used to differentiate sessions
        /// in the data source by application.
        ///</summary>
        public string ApplicationName
        {
            get { return _applicationName; }
        }

        /// <summary>
        /// If false, exceptions are thrown to the caller. If true,
        /// exceptions are written to the event log. 
        /// </summary>
        public bool WriteExceptionsToEventLog
        {
            get { return _writeExceptionsToEventLog; }
        }

        /// <summary>
        /// The max number of attempts that will try to send
        /// an upsert to a replicaSet in case of primary elections.    
        /// </summary>
        public int MaxUpsertAttempts
        {
            get { return _maxUpsertAttempts; }
        }

        /// <summary>
        /// Is the time in milliseconds that will wait between each attempt if
        /// an upsert fails due a primary elections.
        /// </summary>
        public int MsWaitingForAttempt
        {
            get { return _msWaitingForAttempt; }
        }

        public bool AutoCreateTTLIndex
        {
            get { return _autoCreateTTLIndex; }
        }

        /// <summary>
        /// Returns a reference to the collection in MongoDB that holds the Session state
        /// data.
        /// </summary>
        /// <param name="cli">MongoDB server connection</param>
        /// <returns>MongoCollection</returns>
        private IMongoCollection<BsonDocument> GetSessionCollection(MongoClient cli)
        {
            return cli.GetDatabase(_databaseName, _databaseSettings).GetCollection<BsonDocument>(_collectionName);
        }

        /// <summary>
        /// Returns a connection to the MongoDB server holding the session state data.
        /// </summary>
        /// <returns>MongoServer</returns>
        private MongoClient GetConnection()
        {
            var client = new MongoClient(_connectionString);
            return client;
        }

        /// <summary>
        /// Initialise the session state store.
        /// </summary>
        /// <param name="name">session state store name. Defaults to "MongoSessionStateStore" if not supplied</param>
        /// <param name="config">configuration settings</param>
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            // Initialize values from web.config.
            if (config == null)
                throw new ArgumentNullException("config");

            if (name.Length == 0)
                name = "MongoSessionStateStore";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "MongoDB Session State Store provider");
            }

            _databaseName = "SessionState";
            var databaseNameStr = this.GetConfigVal(config, "databaseName");
            if (!string.IsNullOrEmpty(databaseNameStr))
                _databaseName = databaseNameStr;

            _collectionName = "Sessions";
            var collectionNameStr = this.GetConfigVal(config, "collectionName");
            if (!string.IsNullOrEmpty(collectionNameStr))
                _collectionName = collectionNameStr;


            // Initialize the abstract base class.
            base.Initialize(name, config);

            // Initialize the ApplicationName property.
            _applicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;

            // Get <sessionState> configuration element.
            Configuration cfg = WebConfigurationManager.OpenWebConfiguration(ApplicationName);
            _config = (SessionStateSection)cfg.GetSection("system.web/sessionState");

            //Changes the application name if specified and after get the Config reference
            var applicationName = this.GetConfigVal(config, "applicationName");
            if (!string.IsNullOrEmpty(applicationName))
                _applicationName = applicationName;

            // Initialize connection string.
            _connectionStringSettings = ConfigurationManager.ConnectionStrings[this.GetConfigVal(config, "connectionStringName")];

            if (_connectionStringSettings == null || _connectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            _connectionString = _connectionStringSettings.ConnectionString;

            // Initialize WriteExceptionsToEventLog
            _writeExceptionsToEventLog = false;
            var writeExceptionsToEventLogStr = this.GetConfigVal(config, "writeExceptionsToEventLog");

            if (!string.IsNullOrEmpty(writeExceptionsToEventLogStr))
            {
                if (writeExceptionsToEventLogStr.ToUpper() == "TRUE")
                    _writeExceptionsToEventLog = true;
            }

            // Write concern options j (journal) and w (write ack #)

            bool journal = false;
            var journalStr = this.GetConfigVal(config, "Journal");
            if (!string.IsNullOrEmpty(journalStr))
            {
                if (!bool.TryParse(journalStr, out journal))
                    throw new Exception("Journal must be a valid value (true or false)");
            }

            WriteConcern wc = new WriteConcern(journal: journal, w: 1);

            var writeConcernStr = this.GetConfigVal(config, "WriteConcern");
            if (!string.IsNullOrEmpty(writeConcernStr))
            {
                writeConcernStr = writeConcernStr.ToUpper();
                switch (writeConcernStr)
                {
                    case "W1":
                        wc = new WriteConcern(w: 1, journal: journal);
                        break;
                    case "W2":
                        wc = new WriteConcern(w: 2, journal: journal);
                        break;
                    case "W3":
                        wc = new WriteConcern(w: 3, journal: journal);
                        break;
                    case "WMAJORITY":
                        MongoDB.Driver.WriteConcern.WValue aux = "majority";
                        wc = new WriteConcern(aux, journal: journal);
                        break;
                    default:
                        throw new Exception("WriteConcern must be a valid value W1, W2, W3, or WMAJORITY");
                }
            }

            _databaseSettings = new MongoDatabaseSettings()
            {
                WriteConcern = wc
            };

            // Initialize maxUpsertAttempts
            _maxUpsertAttempts = 220;
            var maxUpsertAttemptsStr = this.GetConfigVal(config, "maxUpsertAttempts");
            if (!string.IsNullOrEmpty(maxUpsertAttemptsStr))
            {
                if (!int.TryParse(maxUpsertAttemptsStr, out _maxUpsertAttempts))
                    throw new Exception("maxUpsertAttempts must be a valid integer");
            }

            //initialize msWaitingForAttempt
            _msWaitingForAttempt = 500;
            var msWaitingForAttemptStr = this.GetConfigVal(config, "msWaitingForAttempt");
            if (!string.IsNullOrEmpty(msWaitingForAttemptStr))
            {
                if (!int.TryParse(msWaitingForAttemptStr, out _msWaitingForAttempt))
                    throw new Exception("msWaitingForAttempt must be a valid integer");
            }

            //Initialize AutoCreateTTLIndex
            _autoCreateTTLIndex = true;
            var autoCreateTTLIndexStr = this.GetConfigVal(config, "AutoCreateTTLIndex");
            if (!string.IsNullOrEmpty(autoCreateTTLIndexStr))
            {
                if (!bool.TryParse(autoCreateTTLIndexStr, out _autoCreateTTLIndex))
                    throw new Exception("AutoCreateTTLIndex must be true or false");
            }

            //Create TTL index if AutoCreateTTLIndex config parameter is true.
            if (_autoCreateTTLIndex)
            {
                var conn = GetConnection();
                var sessionCollection = GetSessionCollection(conn);
                MongoSessionStateStoreHelpers.CreateTTLIndex(sessionCollection);
            }

            //Checks if the parameter BSONDefaultSerialize is trying to be used 
            var BSONDefaultSerializeStr = this.GetConfigVal(config, "BSONDefaultSerialize");
            if (!string.IsNullOrEmpty(BSONDefaultSerializeStr))
                throw new Exception("The Json serialization is no longer supported from version 3.0.0. "
                    + "BSONDefaultSerialize parameter has been removed and all data are serialized as BSON value.");
            
        }

        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return new SessionStateStoreData(new SessionStateItemCollection(),
                SessionStateUtility.GetSessionStaticObjects(context),
                timeout);
        }

        /// <summary>
        /// SessionStateProviderBase.SetItemExpireCallback
        /// </summary>
        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return false;
        }

        /// <summary>
        /// SessionStateProviderBase.SetAndReleaseItemExclusive
        /// </summary>
        public override void SetAndReleaseItemExclusive(
            HttpContext context,
            string id,
            SessionStateStoreData item,
            object lockId,
            bool newItem)
        {
            BsonArray bsonArraySession;
            this.Serialize(item, out bsonArraySession);

            MongoClient conn = GetConnection();
            IMongoCollection<BsonDocument> sessionCollection = GetSessionCollection(conn);

            if (newItem)
            {
                var insertDoc = MongoSessionStateStoreHelpers.GetNewBsonSessionDocument(
                    id: id,
                    applicationName: ApplicationName,
                    created: DateTime.Now.ToUniversalTime(),
                    lockDate: DateTime.Now.ToUniversalTime(),
                    lockId: 0,
                    timeout: item.Timeout,
                    locked: false,
                    bsonSessioNItemsArray: bsonArraySession,
                    flags: 0);

                this.UpsertEntireSessionDocument(sessionCollection, insertDoc);
            }
            else
            {
                var filterBuilder = Builders<BsonDocument>.Filter;
                var filter = filterBuilder.And(
                    filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)),
                    filterBuilder.Eq("LockId", (Int32)lockId));
                var update = Builders<BsonDocument>.Update.Set(
                    "Expires", DateTime.Now.AddMinutes(item.Timeout).ToUniversalTime()).Set(
                    "SessionItemBSON", bsonArraySession).Set("Locked", false);
                this.UpdateSessionCollection(sessionCollection, filter, update);
            }
        }

        /// <summary>
        /// SessionStateProviderBase.GetItem
        /// </summary>
        public override SessionStateStoreData GetItem(
            HttpContext context,
            string id,
            out bool locked,
            out TimeSpan lockAge,
            out object lockId,
            out SessionStateActions actionFlags)
        {
            return GetSessionStoreItem(false, context, id, out locked,
              out lockAge, out lockId, out actionFlags);
        }

        /// <summary>
        /// SessionStateProviderBase.GetItemExclusive
        /// </summary>
        public override SessionStateStoreData GetItemExclusive(
            HttpContext context,
            string id,
            out bool locked,
            out TimeSpan lockAge,
            out object lockId,
            out SessionStateActions actionFlags)
        {
            return GetSessionStoreItem(true, context, id, out locked,
              out lockAge, out lockId, out actionFlags);
        }

        /// <summary>
        /// GetSessionStoreItem is called by both the GetItem and 
        /// GetItemExclusive methods. GetSessionStoreItem retrieves the 
        /// session data from the data source. If the lockRecord parameter
        /// is true (in the case of GetItemExclusive), then GetSessionStoreItem
        /// locks the record and sets a new LockId and LockDate.
        /// </summary>
        private SessionStateStoreData GetSessionStoreItem(
            bool lockRecord,
            HttpContext context,
            string id,
            out bool locked,
            out TimeSpan lockAge,
            out object lockId,
            out SessionStateActions actionFlags)
        {
            // Initial values for return value and out parameters.
            SessionStateStoreData item = null;
            lockAge = TimeSpan.Zero;
            lockId = null;
            locked = false;
            actionFlags = 0;

            MongoClient conn = GetConnection();
            IMongoCollection<BsonDocument> sessionCollection = GetSessionCollection(conn);

            // DateTime to check if current session item is expired.
            // String to hold serialized SessionStateItemCollection.
            BsonArray bsonSerializedItems = new BsonArray();
            // True if a record is found in the database.
            bool foundRecord = false;
            // True if the returned session item is expired and needs to be deleted.
            bool deleteData = false;
            // Timeout value from the data store.
            int timeout = 0;


            // lockRecord is true when called from GetItemExclusive and
            // false when called from GetItem.
            // Obtain a lock if possible. Ignore the record if it is expired.
            var filterBuilder = Builders<BsonDocument>.Filter;
            if (lockRecord)
            {
                var filter = filterBuilder.And(
                filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)),
                filterBuilder.Eq("Locked", false),
                filterBuilder.Gt("Expires", DateTime.Now.ToUniversalTime()));
                var update = Builders<BsonDocument>.Update.Set(
                    "Locked", true).Set(
                    "LockDate", DateTime.Now.ToUniversalTime());
                bool result = this.UpdateSessionCollection(sessionCollection, filter, update);

                locked = !result; // result is false when no record was updated because the record was locked or not found.
            }

            // Retrieve the current session item information.
            var filter2 = filterBuilder.And(
                filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)));
            var results = this.FindOneSessionItem(sessionCollection, filter2);

            if (results != null)
            {
                DateTime expires = results["Expires"].ToUniversalTime();

                if (expires < DateTime.Now.ToUniversalTime())
                {
                    // The record was expired. Mark it as not locked.
                    locked = false;
                    // The session was expired. Mark the data for deletion.
                    deleteData = true;
                }
                else
                    foundRecord = true;

                bsonSerializedItems = results["SessionItemBSON"].AsBsonArray;
                lockId = results["LockId"].AsInt32;
                lockAge = DateTime.Now.ToUniversalTime().Subtract(results["LockDate"].ToUniversalTime());
                actionFlags = (SessionStateActions)results["Flags"].AsInt32;
                timeout = results["Timeout"].AsInt32;
            }

            // If the returned session item is expired, 
            // delete the record from the data source.
            if (deleteData)
            {
                filter2 = filterBuilder.And(filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)));
                this.DeleteSessionDocument(sessionCollection, filter2);
            }

            // The record was not found. Ensure that locked is false.
            if (!foundRecord)
                locked = false;

            // If the record was found and you obtained a lock, then set 
            // the lockId, clear the actionFlags,
            // and create the SessionStateStoreItem to return.
            if (foundRecord && !locked)
            {
                lockId = (int)lockId + 1;

                filter2 = filterBuilder.And(filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)));
                var update = Builders<BsonDocument>.Update.Set(
                    "LockId", (int)lockId).Set(
                    "Flags", 0);
                this.UpdateSessionCollection(sessionCollection, filter2, update);

                // If the actionFlags parameter is not InitializeItem, 
                // deserialize the stored SessionStateItemCollection.
                item = actionFlags == SessionStateActions.InitializeItem
                    ? CreateNewStoreData(context, (int)_config.Timeout.TotalMinutes)
                    : MongoSessionStateStoreHelpers.Deserialize(context, bsonSerializedItems, timeout);
            }

            return item;
        }

        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            var conn = GetConnection();
            var sessionCollection = GetSessionCollection(conn);
            var doc = MongoSessionStateStoreHelpers.GetNewBsonSessionDocument(
                id: id,
                applicationName: ApplicationName,
                created: DateTime.Now.ToUniversalTime(),
                lockDate: DateTime.Now.ToUniversalTime(),
                lockId: 0,
                timeout: timeout,
                locked: false,
                bsonSessioNItemsArray: new BsonArray(),
                flags: 1);

            this.UpsertEntireSessionDocument(sessionCollection, doc);
        }

        public override void Dispose()
        {
        }

        public override void EndRequest(HttpContext context)
        {

        }

        public override void InitializeRequest(HttpContext context)
        {

        }

        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {
            var conn = GetConnection();
            var sessionCollection = GetSessionCollection(conn);

            var filterBuilder = Builders<BsonDocument>.Filter;
            var filter = filterBuilder.And(
                filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)),
                filterBuilder.Eq("LockId", (Int32)lockId));

            var update = Builders<BsonDocument>.Update.Set(
                "Locked", false).Set(
                "Expires", DateTime.Now.AddMinutes(_config.Timeout.TotalMinutes).ToUniversalTime());

            this.UpdateSessionCollection(sessionCollection, filter, update);
        }

        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            var conn = GetConnection();
            var sessionCollection = GetSessionCollection(conn);

            var filterBuilder = Builders<BsonDocument>.Filter;
            var query = filterBuilder.And(
                filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)),
                filterBuilder.Eq("LockId", (Int32)lockId));

            this.DeleteSessionDocument(sessionCollection, query);
        }

        public override void ResetItemTimeout(HttpContext context, string id)
        {
            var conn = GetConnection();
            var sessionCollection = GetSessionCollection(conn);
            var filterBuilder = Builders<BsonDocument>.Filter;
            var query = filterBuilder.And(filterBuilder.Eq("_id", MongoSessionStateStoreHelpers.GetDocumentSessionId(id, ApplicationName)));
            var update = Builders<BsonDocument>.Update.Set("Expires", DateTime.Now.AddMinutes(_config.Timeout.TotalMinutes).ToUniversalTime());

            this.UpdateSessionCollection(sessionCollection, query, update);
        }
    }
}