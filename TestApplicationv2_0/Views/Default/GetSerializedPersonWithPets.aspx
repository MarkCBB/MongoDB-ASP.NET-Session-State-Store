<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TestApplicationv2_0.Models.PersonPetsList>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>GetSerializedPersonWithPets</title>
</head>
<body>
    <fieldset>
        <legend>PersonPetsList</legend>

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
        <div class="display-field">
            <%: Html.DisplayFor(model => model.City) %>
        </div>        
        <%foreach (string petName in Model.PetsList)
          {
        %>
        <div class="display-field">
            <%: Html.DisplayFor(model => petName) %>
        </div>
        <%} %>
    </fieldset>
    <p>
        <%: Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>
