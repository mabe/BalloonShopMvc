<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BalloonShop.Mvc.Models.AccountDetailsViewModel>" %>

<%= Html.LabelFor(x => x.Address1) %>
<%= Html.EditorFor(x => x.Address1) %><br />
<%= Html.LabelFor(x => x.Address2) %>
<%= Html.EditorFor(x => x.Address2) %><br />
<%= Html.LabelFor(x => x.City) %>
<%= Html.EditorFor(x => x.City) %><br />
<%= Html.LabelFor(x => x.Region) %>
<%= Html.EditorFor(x => x.Region) %><br />
<%= Html.LabelFor(x => x.PostalCode) %>
<%= Html.EditorFor(x => x.PostalCode) %><br />
<%= Html.LabelFor(x => x.Country) %>
<%= Html.EditorFor(x => x.Country) %><br />
<%= Html.LabelFor(x => x.ShippingRegion) %>
<%= Html.EditorFor(x => x.ShippingRegion) %><br />
<%= Html.LabelFor(x => x.DaytimePhone) %>
<%= Html.EditorFor(x => x.DaytimePhone) %><br />
<%= Html.LabelFor(x => x.EveningPhone) %>
<%= Html.EditorFor(x => x.EveningPhone) %><br />
<%= Html.LabelFor(x => x.MobilePhone) %>
<%= Html.EditorFor(x => x.MobilePhone) %><br />