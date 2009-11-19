<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.List<BalloonShop.Model.Department>>" %>
<table style="width: 200px; border-collapse: collapse;" border="0" cellspacing="0"
    class="DepartmentListContent">
    <thead>
        <tr>
            <th class="DepartmentListHead">
                Choose a Department
            </th>
        </tr>
    </thead>
    <tbody>
        <% foreach (var department in Model) { %>
        <tr>
            <td>
                &nbsp;&raquo;
                    <a href="<%= Url.Action("Show", "Department", new { Id = department.Id }) %>" class="<%= department.Id == (int)ViewData["DepartmentId"] ? "DepartmentSelected" : "DepartmentUnselected" %>" title="<%= department.Description %>"><%= department.Name %></a>
                &nbsp;&laquo;
            </td>
        </tr>
        <% } %>
    </tbody>
    <tfoot>
        <tr>
            <td class="DepartmentListContent">
                &nbsp;&raquo; <a href="AmazonProducts.aspx" class="<%= Request.AppRelativeCurrentExecutionFilePath == "~/AmazonProducts.aspx" ? "DepartmentSelected" : "DepartmentUnselected" %>">
                    Amazon Balloons </a>&nbsp;&laquo;
            </td>
        </tr>
    </tfoot>
</table>
