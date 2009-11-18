<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<BalloonShop.Model.Category>>" %>

<table style="width: 200px; border-collapse: collapse;" border="0" cellspacing="0"
    class="CategoryListContent">
    <thead>
        <tr>
            <th class="CategoryListHead">
                Choose a Category
            </th>
        </tr>
    </thead>
    <tbody>
        <% foreach (var category in Model)
           { %>
        <tr>
            <td>
                &nbsp;&raquo;
                    <a href="<%= Url.Action("Show", "Category", new { Id = category.Id }) %>" class="<%= category.Id == (int)ViewData["CategoryId"] ? "CategorySelected" : "CategoryUnselected" %>" title="<%= category.Description %>"><%= category.Name %></a>
                &nbsp;&laquo;
            </td>
        </tr>
        <% } %>
    </tbody>
    <%--<tfoot>
        <tr>
            <td class="DepartmentListContent">
                &nbsp;&raquo; <a href="AmazonProducts.aspx" class="<%//= Request.AppRelativeCurrentExecutionFilePath == "~/AmazonProducts.aspx" ? "DepartmentSelected" : "DepartmentUnselected" %>">
                    Amazon Balloons </a>&nbsp;&laquo;
            </td>
        </tr>
    </tfoot>--%>
</table>

<%--<asp:DataList ID="list" runat="server" CssClass="CategoryListContent" Width="200px">
  <ItemTemplate>
    &nbsp;&raquo;
    <asp:HyperLink 
      ID="HyperLink1" 
      Runat="server" 
      NavigateUrl='<%# "../Catalog.aspx?DepartmentID=" + Request.QueryString["DepartmentID"] + "&CategoryID=" + Eval("CategoryID")  %>'
      Text='<%# Eval("Name") %>' 
      ToolTip='<%# Eval("Description") %>' 
      CssClass='<%# Eval("CategoryID").ToString() == Request.QueryString["CategoryID"] ? "CategorySelected" : "CategoryUnselected" %>'>>
    </asp:HyperLink>
    &nbsp;&laquo;
  </ItemTemplate>
  <HeaderTemplate>
    Choose a Category
  </HeaderTemplate>
  <HeaderStyle CssClass="CategoryListHead" />
</asp:DataList><asp:Label ID="brLabel" runat="server" Text="" />--%>