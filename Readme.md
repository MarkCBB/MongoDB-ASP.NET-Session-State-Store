Compatibility with 1.0.0 version (in v2.0.0)
============================================

The compatibility with the first version only is guaranteed if you set the parameter BSONDefaultSerialize to false **explicitly**
and is not used any kind of BsonValue including subclasses (i.e. BsonDocument).

If one or both of these premises are not accomplished the old session data stored could not work properly.

**The JSON serializer will be removed in next versions.**

Usage
=====

1. Install the [nuGet package](https://www.nuget.org/packages/MongoSessionStateStore/) into your solution.

2. Into web.config file add a <connectionStrings> section as detailed following **set connection parameters properly.**
```xml
    <configuration>
      <connectionStrings>
        <add name="MongoSessionServices"
             connectionString="mongodb://mongo1:27018,mongo1:27019,mongo1:27020/?connect=replicaset"/>
      </connectionStrings>
    </configuration>
```

3. Configure the <sessionState> provider section as detailed following:
```xml
    <system.web>
    <sessionState mode="Custom" customProvider="MongoSessionStateProvider">
      <providers>
        <add name="MongoSessionStateProvider"
             type="MongoSessionStateStore.MongoSessionStateStore"
             connectionStringName="MongoSessionServices" />
      </providers>
    </sessionState>
    </system.web>
```

Now you can get started using MongoDB Session State Store. 

A helper file is provided with the nuget package and this file will be available in the target project when package has been installed 
**It's strongly recommended to use these helpers** as shown in the examples(also you can extend it).

```C#
// Sets 1314 in key named sessoinKey
Session.Mongo<int>("sessoinKey", 1314);

// Gets the value from key named "sessoinKey"
int n = Session.Mongo<int>("sessoinKey");
```

Note that decimal values must be converted to double.

For non primitive objects you can use the same helper methods.

```C#
// Creates and store the object personSetted (Person type) in session key named person
Person personSetted = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };
Session.Mongo<Person>("person", personSetted);

// Retrieves the object stored in session key named "person"
Person personGetted = Session.Mongo<Person>("person");
```

Also, for primitive types you can use a direct way (not recommended).

```C#
// Set primitive value
Session["counter"] = 1;

//Get value from another request
int n = Session["counter"];
```

For further information read about [parameters config](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Web.config-parameters#parameters-detail)
