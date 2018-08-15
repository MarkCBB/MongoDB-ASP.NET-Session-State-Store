This project is not built for ASP.NET Core, if you need a session provider for ASP.NET Core [check this out](https://github.com/MarkCBB/aspnet-mongodb-session-sample#aspnet-core-mongodb-session-sample).

Usage
=====

1 - Install the [nuGet package](https://www.nuget.org/packages/MongoSessionStateStore/) into your solution.

The current version is built in 4.5 version of .NET framework. To use the 4.0 version of .NET framework [install the version 2.0.0 of this controller](https://www.nuget.org/packages/MongoSessionStateStore/2.0.0)

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

**Now you can get started using MongoDB Session State Store.**

Chose one of these serialization types: [Bson](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/tree/Branch_raw_serialization#bson-serialization) (default) or [RAW](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/tree/Branch_raw_serialization#raw-serialization). See the [documentation about types of serialization](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Types-of-serialization) to select the most suitable for you and the advantages and disadvantages.

## Bson serialization (default)

To get started working with Bson serialization you don't need to set any parameter to any value, it's the default serialization.

A helper class is included in the assembly with static extensions. **It's strongly recommended to use these helpers** as shown in the examples.

You can personalize all methods of this helper class [following these instructions](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Customizing-the-helpers).

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

## RAW serialization

To get started working with RAW serialization you need to set the parameter SerializationType to RAW value in the web.config file. See [parameters detail](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Web.config-parameters#parameters-detail) to view the documentation about all parameters and a complete example of the config string.

```C#
// Declare the objects with Serializable attribute.
[Serializable]
public class Person
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string City { get; set; }
}
	
// The usage is the same as usual
Person personSet = new Person() { Name = "Marc", Surname = "Cortada", City = "Barcelona" };
Session["key"] = personSet;
Person personGet = (Person)Session["key"];
// Or even better
personGet = Session["key"] as Person;
if (personGet != null) {...}
```

For further information read about [parameters config](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Web.config-parameters#parameters-detail)

[Here you'll find all release notes](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Release-notes-history-and-compatibility-between-versions)

**If you are moving from 3.X.X to 4.X.X, as a major release, keep in mind [these compatibility notes.](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Release-notes-history-and-compatibility-between-versions#v400)**

**If you are moving from 2.X.X to 3.X.X, as a major release, keep in mind [these compatibility notes.](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Release-notes-history-and-compatibility-between-versions#v300)**

**If you are moving from 1.X.X to 2.X.X, as a major release, keep in mind [these compatibility notes.](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Release-notes-history-and-compatibility-between-versions#v200)**

To set sessions without expiration time **do not use Session.Timeout value to 0** [disable TTL index creation](https://github.com/MarkCBB/MongoDB-ASP.NET-Session-State-Store/wiki/Web.config-parameters#autocreatettlindex)
