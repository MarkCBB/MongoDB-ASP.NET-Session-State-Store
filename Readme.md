Usage
=====

1 - Install the [nuGet package](https://www.nuget.org/packages/MongoSessionStateStore/) into your solution.

The current version is built in 4.5 version of .NET framework.

To use the 4.0 version of .NET framework [install the version 2.0.0 of this controller](https://www.nuget.org/packages/MongoSessionStateStore/2.0.0) with the following command in the Package Manager Console: **Install-Package MongoSessionStateStore -Version 2.0.0**

2 - Into web.config file add a <connectionStrings> section as detailed following **set connection parameters properly.**
```xml
    <configuration>
      <connectionStrings>
        <add name="MongoSessionServices"
             connectionString="mongodb://mongo1:27018,mongo1:27019,mongo1:27020/?connect=replicaset"/>
      </connectionStrings>
    </configuration>
```

3 - Configure the <sessionState> provider section as detailed following:
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

A helper file is provided with the nuget package and this file will be available in the target project when package has been installed. **It's strongly recommended to use these helpers** as shown in the examples. Also you can extend it.

```C#
// Sets 1314 in key named sessionKey
Session.Mongo<int>("sessionKey", 1314);

// Gets the value from key named "sessionKey"
int n = Session.Mongo<int>("sessionKey");

/* Note that decimal values must be converted to double.
   For non primitive objects you can use the same helper methods. */

// Creates and store the object personSetted (Person type) in session key named person
Person personSetted = new Person()
	{
		Name = "Marc",
		Surname = "Cortada",
		City = "Barcelona"
	};
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

[Here you'll find all release notes](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Release-notes-history-and-compatibility-between-versions)

**If you are moving from 1.0.0 to 2.0.0, as a major release, keep in mind [these compatibility notes.](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Release-notes-history-and-compatibility-between-versions#v200)**

To make session data without expiration time **do not use Session.Timeout value to 0** [disable TTL index creation](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Web.config-parameters#autocreatettlindex)
