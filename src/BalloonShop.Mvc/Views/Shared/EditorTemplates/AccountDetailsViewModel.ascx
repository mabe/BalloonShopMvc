﻿<%@ control language="C#" inherits="System.Web.Mvc.ViewUserControl<BalloonShop.Mvc.Models.AccountDetailsViewModel>" %>
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
				<%= Html.EditorFor(x => x.Address1) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.Address2) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.Address2) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.City) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.City) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.Region) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.Region) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.PostalCode) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.PostalCode) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.Country) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.Country) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.ShippingRegion) %>
			</td>
			<td>
				<%= Html.DropDownListFor(x => x.ShippingRegion, (IEnumerable<SelectListItem>) ViewBag.ShippingRegions)%>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.DaytimePhone) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.DaytimePhone) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.EveningPhone) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.EveningPhone) %>
			</td>
		</tr>
		<tr>
			<td>
				<%= Html.LabelFor(x => x.MobilePhone) %>
			</td>
			<td>
				<%= Html.EditorFor(x => x.MobilePhone) %>
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
							<%= Html.TextBoxFor(x => x.CardholderName) %>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.CardType) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.CardType) %>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.CardNumber) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.CardNumber) %>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.IssueDate) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.IssueDate) %>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.ExpiryDate) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.ExpiryDate) %>
						</td>
					</tr>
					<tr>
						<td>
							<%= Html.LabelFor(x => x.IssueNumber) %>
						</td>
						<td>
							<%= Html.TextBoxFor(x => x.IssueNumber) %>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</tbody>
</table>
