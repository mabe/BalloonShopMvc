<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master"
    Inherits="System.Web.Mvc.ViewPage<BalloonShop.Infrastructure.PagedList<BalloonShop.Model.Balloon>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <span class="CatalogTitle">Welcome to BalloonShop! </span>
    <br />
    <span class="CatalogDescription">This week we have a special price for these fantastic
        products: </span>
    <br />
    <% Html.RenderPartial("~/Views/Balloon/PagedList.ascx"); %>
</asp:Content>
