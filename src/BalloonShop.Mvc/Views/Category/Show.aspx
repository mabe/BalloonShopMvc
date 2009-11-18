<%@ Page Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<BalloonShop.Model.Category>" %>

<asp:Content runat="server" ContentPlaceHolderID="navigation">
    <% Html.RenderAction<DepartmentController>(x => x.Navigation(Model.DepartmentId));  %><br />
    <%Html.RenderAction<CategoryController>(x => x.Navigation(Model.DepartmentId, Model.Id)); %>
</asp:Content>
<asp:Content ContentPlaceHolderID="content" runat="server">

<span class="CatalogTitle"><%= Model.Name %></span>
<br/>
<span class="CatalogDescription"><%= Model.Description %></span>
<br />
<%= Html.Paging<BalloonShop.Model.Balloon, CategoryController>(Model.Balloons, x => y => y.Show(Model.Id, x)) %>
<% Html.RenderPartial("../Balloon/List", Model.Balloons); %>
</asp:Content>