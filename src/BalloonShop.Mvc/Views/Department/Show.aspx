<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<BalloonShop.Model.Department>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">

<span class="CatalogTitle"><%= Model.Name %></span>
<br/>
<span class="CatalogDescription"><%= Model.Description %></span>
<% Html.RenderPartial("~/Views/Balloon/PagedList.ascx", Model.Balloons); %>
</asp:Content>
