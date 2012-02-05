<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master"
    Inherits="System.Web.Mvc.ViewPage<BalloonShop.Infrastructure.PagedList<BalloonShop.Model.Balloon>>" %>

<asp:Content runat="server" ContentPlaceHolderID="navigation">
    <% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
	<br />
    <% Html.RenderPartial("../Search/SearchForm"); %>
    <br />
    <% Html.RenderAction("Summary", "Cart"); %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <span class="CatalogTitle">Welcome to BalloonShop! </span>
    <br />
    <span class="CatalogDescription">This week we have a special price for these fantastic
        products: </span>
    <br />
    <%= Html.Paging<BalloonShop.Model.Balloon, HomeController>(Model, index => controller => controller.Index(index)) %>
    <% Html.RenderPartial("../Balloon/List"); %>
</asp:Content>
