<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<BalloonShop.Model.Department>" %>

<asp:Content runat="server" ContentPlaceHolderID="navigation">
    <% Html.RenderAction<DepartmentController>(x => x.Navigation(Model.Id));  %><br />
    <%Html.RenderAction<CategoryController>(x => x.Navigation(Model.Id, null)); %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">

<span class="CatalogTitle"><%= Model.Name %></span>
<br/>
<span class="CatalogDescription"><%= Model.Description %></span>
<br />
<%= Html.Paging<BalloonShop.Model.Balloon, DepartmentController>((object)ViewBag.PromotedBalloons, x => y => y.Show(Model.Id, x)) %>
<% Html.RenderPartial("../Balloon/List", (object)ViewBag.PromotedBalloons); %>
</asp:Content>
