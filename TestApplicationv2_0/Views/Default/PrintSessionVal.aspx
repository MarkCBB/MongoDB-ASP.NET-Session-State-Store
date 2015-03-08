<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>PrintSessionVal</title>
</head>
<body>
    <div>
        <sessionVal><%=ViewData["sessionVal"] %></sessionVal>
    </div>
</body>
</html>
