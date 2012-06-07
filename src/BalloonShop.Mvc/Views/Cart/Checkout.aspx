<%@ page title="" language="C#" masterpagefile="~/Views/Shared/BalloonShop.master"
	inherits="System.Web.Mvc.ViewPage<BalloonShop.Mvc.Models.CheckoutViewModel>" %>

<asp:content id="Content1" contentplaceholderid="content" runat="server">
	<h2 class="ShoppingCartTitle">
		These are the products in your shopping cart:</h2>
	<br />
	<table cellspacing="0" cellpadding="4" style="border-width: 0px; width: 100%; border-collapse: collapse;">
		<thead>
			<tr class="GridHeader">
				<th>
					Product Name
				</th>
				<th>
					Price
				</th>
				<th>
					Quantity
				</th>
				<th>
					Subtotal
				</th>
			</tr>
		</thead>
		<tbody>
			<% foreach (var obj in (ViewBag.Cart as IEnumerable<BalloonShop.Model.ShoppingCart>).Select((x, i) => new { Item = x, Index = i }).ToList())
	  { %>
			<tr class="<%= obj.Index % 2 == 0 ? "GridRow" : "GridAlternateRow" %>">
				<td>
					<%= obj.Item.Balloon.Name%>
				</td>
				<td>
					<%= obj.Item.Balloon.Price.ToString("c")%>
				</td>
				<td>
					<%= obj.Item.Quantity %>
				</td>
				<td>
					<%= (obj.Item.Balloon.Price * obj.Item.Quantity).ToString("c")%>
				</td>
			</tr>
			<% } %>
		</tbody>
		<tfoot>
			<tr>
				<td colspan="5">
					<span class="ProductDescription">Total amount: </span><span class="ProductPrice">
						<%= ViewBag.Total.ToString("c") %></span>
				</td>
			</tr>
		</tfoot>
	</table>
	<form method="post" action="<%= Url.Action("Checkout") %>">
	<br />
	<br />
	<%--<uc1:customerdetailsedit id="CustomerDetailsEdit1" runat="server" editable="false" title="User Details" />--%>
	<%= Html.DisplayFor(x => x.AccountDetails) %>
	<br />
	<%--<asp:label id="InfoLabel" runat="server" cssclass="InfoText" />--%>
	<span class="InfoText">Please confirm that the above details are correct before proceeding.</span>
	<br />
	<br />
	<label class="InfoText">
		Shipping type:</label>
	<select name="ShippingType" id="ShippingType">
		<option value="0">Please Select</option>
		<% foreach (var item in ViewBag.ShippingTypes)
	 { %>
		<option value="<%= item.Id %>" data-region="<%= item.Region.Id %>">
			<%= item.Name %></option>
		<% } %>
	</select>
	<br />
	<%= Html.ValidationSummary(false) %>
	<br />
	<input type="submit" class="ButtonText" value="Place order" />
	</form>
</asp:content>
<asp:content id="Content2" contentplaceholderid="navigation" runat="server">
	<% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
	<br />
	<% Html.RenderPartial("../Search/SearchForm"); %>
	<br />
	<% Html.RenderAction("Summary", "Cart"); %>
</asp:content>
<asp:content contentplaceholderid="scripts" runat="server">
	<script type="text/javascript">
		$(function () {
			var shipping = $('#ShippingType').prop('disabled', true),
                nothing = shipping.find('option[value=0]').clone(),
                types = shipping.find('option:not([value=0])').clone();

			$('#AccountDetails_ShippingRegion').on('change', function (e) {
				shipping.empty().append(nothing.clone()).append(types.filter('[data-region=' + $(this).val() + ']').clone()).prop('disabled', false);
			}).trigger('change');
		});
	</script>
</asp:content>
