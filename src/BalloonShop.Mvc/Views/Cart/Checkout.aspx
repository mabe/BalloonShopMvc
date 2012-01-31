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
            <% foreach (var obj in (ViewBag.Cart as IEnumerable<BalloonShop.Model.ShoppingCart>).Select((x, i) => new { Item = x, Index = i }).ToList()) { %>
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
    <br />
    <br />
    <%--<uc1:customerdetailsedit id="CustomerDetailsEdit1" runat="server" editable="false" title="User Details" />--%>
    <table border="0" cellpadding="4" cellspacing="0" class="UserDetailsTable">
    <thead>
    <tr><th colspan="2" class="UserDetailsTableHead">User Details</th></tr>
    </thead>
    <tbody>
        <tr><td>Address line 1: </td><td width="350px"><input type="text" name="Address1" style="width:340px;" /></td></tr>
        <tr><td>Address line 2: </td><td><input type="text" name="Address2" style="width:340px;" /></td></tr>
        <tr><td>City: </td><td><input type="text" name="City" style="width:340px;" /></td></tr>
        <tr><td>Region: </td><td><input type="text" name="Region" style="width:340px;" /></td></tr>
        <tr><td>Zip / Postal Code: </td><td><input type="text" name="PostalCode" style="width:340px;" /></td></tr>
        <tr><td>Country: </td><td><input type="text" name="Country" style="width:340px;" /></td></tr>
        <tr><td>Shipping Region: </td><td><%= Html.DropDownList("ShippingRegion", (IEnumerable<SelectListItem>)ViewBag.ShippingRegions, new { style = "width:350px;" })%></td></tr>
        <tr><td>Daytime Phone no: </td><td><input type="text" name="DayPhone" style="width:340px;" /></td></tr>
        <tr><td>Evening Phone no: </td><td><input type="text" name="EvePhone" style="width:340px;" /></td></tr>
        <tr><td>Mobile Phone no: </td><td><input type="text" name="MobPhone" style="width:340px;" /></td></tr>
        <tr><td>Email: </td><td><input type="text" name="Email" style="width:340px;" /></td></tr>
    </tbody>
    </table>
    <br />
    <%--<asp:label id="InfoLabel" runat="server" cssclass="InfoText" />--%>
    <br />
    <br />
    <label class="InfoText">Shipping type:</label>
    <select name="ShippingType" id="ShippingType">
        <option value="0">Please Select</option>
        <% foreach (var item in ViewBag.ShippingTypes) { %>
               <option value="<%= item.Id %>" data-region="<%= item.Region.Id %>"><%= item.Name %></option>
        <% } %>
    </select>
    <br />
    <br />
    <button class="ButtonText">Place order</button>

    <script type="text/javascript">
        $(function () {
            var shipping = $("#ShippingType").prop("disabled", true),
                nothing = shipping.find("option[value=0]").clone(),
                types = shipping.find("option:not([value=0])").clone();

            $("ShippingRegion").on("change", function (e) {
                shipping.empty().append(nothing.clone()).append(types.find("[data-region=" + $(this).val() + "]").clone());
            });
        });
    </script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="navigation" runat="server">
</asp:content>
