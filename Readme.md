Usage
=====

Install the [nuGet package](https://www.nuget.org/packages/MongoSessionStateStore/) into your solution.

Add these two sections into your web.config. **Set connection parameters properly.**

1.
```xml
    <configuration>
      <connectionStrings>
        <add name="MongoSessionServices"
             connectionString="mongodb://mongo1:27018,mongo1:27019,mongo1:27020/?connect=replicaset"/>
      </connectionStrings>
    </configuration>
```
2.
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

Now you can get started using Session State Store.

For primitive types you can use a direct way. **For objects in different requests JSON parse is required. Parse is not required in the same request code block**:

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
