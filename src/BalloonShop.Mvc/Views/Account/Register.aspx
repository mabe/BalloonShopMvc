<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">

    <h2 class="CatalogTitle">Register</h2>

	<form method="post" action="<%= Url.Action("Register") %>">
	<div class="UserInfoText">	
		<label>Email:</label>
		<input type="text" name="Email" placeholder="Email" /><br />

		<label>Password:</label>
		<input type="password" name="Password" placeholder="Password" /><br />

		<%= Html.ValidationSummary(false) %>

		<input type="submit" value="Register" />
	</div>
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
