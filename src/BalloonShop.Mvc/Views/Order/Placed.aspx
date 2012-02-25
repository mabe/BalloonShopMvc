<%@ page title="" language="C#" masterpagefile="~/Views/Shared/BalloonShop.master"
	inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:content id="Content1" contentplaceholderid="content" runat="server">
	<span class="InfoText">Thank you for your order, please come again!</span>
</asp:content>
<asp:content id="Content2" contentplaceholderid="navigation" runat="server">
	<% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
	<br />
    <% Html.RenderPartial("../Search/SearchForm"); %>
</asp:content>
<asp:content id="Content3" contentplaceholderid="scripts" runat="server">
</asp:content>
