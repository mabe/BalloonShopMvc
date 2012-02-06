<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<BalloonShop.Mvc.Models.AccountDetailsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">

    <h2 class="CatalogTitle">Details</h2>

	<form method="post" action="">

	<%= Html.EditorForModel() %>

	<%= Html.ValidationSummary(false) %>

	<input type="submit" value="Update" />

	</form>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="navigation" runat="server">
	<% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
	<br />
	<% Html.RenderPartial("../Search/SearchForm"); %>
	<br />
	<% Html.RenderAction("Summary", "Cart"); %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="server">
</asp:Content>
