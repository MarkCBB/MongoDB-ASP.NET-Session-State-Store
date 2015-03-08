<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TestApplicationv2_0.Models.Person>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>GetSerializedPerson</title>
</head>
<body>
    <fieldset>
        <legend>Person</legend>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Name) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Name) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.Surname) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.Surname) %>
        </div>
    
        <div class="display-label">
            <%: Html.DisplayNameFor(model => model.City) %>
        </div>
        <div class="display-field">
            <%: Html.DisplayFor(model => model.City) %>
        </div>
    </fieldset>
    <p>
        <%: Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>
