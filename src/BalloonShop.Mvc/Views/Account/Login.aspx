<%@ page title="" language="C#" masterpagefile="~/Views/Shared/BalloonShop.master"
	inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:content id="Content1" contentplaceholderid="content" runat="server">
	<h2 class="CatalogTitle">
		Who Are You?</h2>
	<form method="post" action="<%= Url.Action("Login") %>">
	<input type="hidden" name="returnUrl" value="<%= (ViewBag.ReturnUrl ?? "") %>" />
	<div class="UserInfoText">
		<label>
			Email:</label>
		<input type="text" name="email" /><br />
		<label>
			Password:</label>
		<input type="password" name="password" /><br />
		<input type="checkbox" name="remeberMe" />
		<label>
			Remember me next time.</label><br />

		<%= Html.ValidationSummary(false) %>

		<input type="submit" value="Log In" />
	</div>
	</form>
	<span class="InfoText">You must be logged in to place an order. If you aren't yet registered
		with the site, click <a href="<%= Url.Action("Register") %>" title="Go to the registration page"
			class="UserInfoLink">here</a>. </span>
</asp:content>
<asp:content id="Content2" contentplaceholderid="navigation" runat="server">
	<% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
	<br />
	<% Html.RenderPartial("../Search/SearchForm"); %>
	<br />
	<% Html.RenderAction("Summary", "Cart"); %>
</asp:content>
<asp:content id="Content3" contentplaceholderid="scripts" runat="server">
</asp:content>
