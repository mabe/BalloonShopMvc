<%@ control language="C#" inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<table cellspacing="0" border="0" width="200px" class="UserInfoContent">
	<thead>
		<tr>
			<th class="UserInfoHead">
				User Info
			</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td>
				<span class="UserInfoText">You are logged in as <b><%= Model.Email %></b>.</span>
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;&raquo;
				<a href="<%= Url.Action("Logout", "Account") %>" class="UserInfoLink">Logout</a>
				&nbsp;&laquo;
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;&raquo;
				<a href="<%= Url.Action("Details", "Account") %>" title="Edit your personal details" class="UserInfoLink">Edit Details</a>
				&nbsp;&laquo;
			</td>
		</tr>
	</tbody>
</table>
