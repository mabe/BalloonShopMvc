<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<BalloonShop.Model.Balloon>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">

  <span class="CatalogTitle">Product Search</span><br />
  <span class="CatalogDescription">You searched for <span style="color:red;"><%=ViewData["search"] %></span>.</span><br /><br />
    <%= Html.Paging<BalloonShop.Model.Balloon, SearchController>(Model, page => controller => controller.Show((string)ViewData["search"], (bool)ViewData["allWords"], page))%>
    <% Html.RenderPartial("../Balloon/List"); %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="navigation" runat="server">
    <% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
    <br />
    <% Html.RenderPartial("../Search/SearchForm"); %>
    <br />
    <% Html.RenderAction("Summary", "Cart"); %>
</asp:Content>
