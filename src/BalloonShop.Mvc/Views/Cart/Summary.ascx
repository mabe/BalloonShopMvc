<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<BalloonShop.Model.ShoppingCart>>" %>
<table border="0" cellpadding="0" cellspacing="1" width="200">
    <tr>
        <td class="CartSummary">
            <% if (Model.Any())
               { %>
            <strong>Cart summary </strong><a href="<%= Url.Action("Index", "Cart") %>" class="CartLink">
                (view details)</a>
            <ul>
            <% foreach (var item in Model)
               { %>
                   <li><%= item.Quantity %> x <%= item.Balloon.Name %></li>
            <% } %>
            </ul>
            <img src="Images/line.gif" border="0" width="99%" height="1" />
            Total: <span class="ProductPrice">
                <%= ViewBag.Total.ToString("c")%>
            </span>
            <% }
               else
               { %>
            <strong>Your shopping cart is empty.</strong>
            <% } %>
        </td>
    </tr>
</table>
