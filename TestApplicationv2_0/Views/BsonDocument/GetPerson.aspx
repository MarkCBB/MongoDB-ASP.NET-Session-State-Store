<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>GetPerson</title>
</head>
<body>
    <div>
        <result>
            Name: <%=ViewBag.Name %>
            Surname: <%=ViewBag.Surname %>
            City: <%=ViewBag.City %>
        </result>
    </div>
</body>
</html>
