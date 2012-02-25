﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<BalloonShop.Mvc.Models.AccountDetailsViewModel>" %>

<table border="0" cellpadding="4" cellspacing="0" class="UserDetailsTable">
	<thead>
		<tr>
			<th colspan="2" class="UserDetailsTableHead">
				User Details
			</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.Address1) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.Address1, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.Address2) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.Address2, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.City) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.City, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.Region) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.Region, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.PostalCode) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.PostalCode, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.Country) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.Country, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.ShippingRegion) %>
			</td>
			<td>
				<%= Html.DropDownListFor(x => x.ShippingRegion, (IEnumerable<SelectListItem>)ViewBag.ShippingRegions)%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.DaytimePhone) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.DaytimePhone, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.EveningPhone) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.EveningPhone, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.MobilePhone) %>
			</td>
			<td>
				<%= Html.TextBoxFor(x => x.MobilePhone, new { @readonly = "readonly" })%>
			</td>
		</tr>
		<tr>
			<td valign="top">
				Credit Card:
			</td>
			<td>
				<table cellpadding="0" cellspacing="0" border="0">
					<tr>
						<td width="140px">
							<%= Html.LabelFor(x => x.CardholderName) %>
						</td>
						<td width="200px">
							<%= Html.TextBoxFor(x => x.CardholderName, new { @readonly = "readonly" })%>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.CardType) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.CardType, new { @readonly = "readonly" })%>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.CardNumber) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.CardNumber, new { @readonly = "readonly" })%>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.IssueDate) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.IssueDate, new { @readonly = "readonly" })%>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.ExpiryDate) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.ExpiryDate, new { @readonly = "readonly" })%>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.IssueNumber) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.IssueNumber, new { @readonly = "readonly" })%>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</tbody>
</table>