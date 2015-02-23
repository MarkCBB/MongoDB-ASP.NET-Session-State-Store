Usage
=====

Install the [nuGet package](https://www.nuget.org/packages/MongoSessionStateStore/) into your solution.
Add these two sections into your web.config setting connection parameters properly.

1.
    <configuration>
    ...
      <connectionStrings>
        <add name="MongoSessionServices"
             connectionString="mongodb://mongo1:27018,mongo1:27019,mongo1:27020/?connect=replicaset"/>
      </connectionStrings>
    ...
    </configuration>

2.
    <system.web>
    ...
    <sessionState mode="Custom" customProvider="MongoSessionStateProvider">
      <providers>
        <add name="MongoSessionStateProvider"
             type="MongoSessionStateStore.MongoSessionStateStore"
             connectionStringName="MongoSessionServices" />
      </providers>
    </sessionState>
    ...
    </system.web>
Now you can get stated using Session State Store.


For primitive types you can use a direct way:

```C#
//Set primitive value
Session[“counter”] = 1;
//Get value from another request
int n = Session[“counter”];
To serialize objects 
// Set
Session[“person”] = new Person() { Name = “Marc” };
// Getting from another request (if is the same request cast is not needed)
// Consider additional null value checks.
var pJSON = Session["person"] as Newtonsoft.Json.Linq.JObject;
Person p = pJSON.ToObject<Person>();
```
