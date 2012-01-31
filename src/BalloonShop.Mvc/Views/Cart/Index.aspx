<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<BalloonShop.Model.ShoppingCart>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">

    <h2 class="ShoppingCartTitle">Your Shopping Cart</h2>

<form method="post" action="<%= Url.Action("Update", "Cart") %>">
    <table cellspacing="0" cellpadding="4" style="border-width:0px;width:100%;border-collapse:collapse;">
        <thead>
            <tr class="GridHeader">
                <th>Product Name</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Subtotal</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var obj in Model.Select((x, i) => new { Item = x, Index = i }).ToList()) { %>
        <tr class="<%= obj.Index % 2 == 0 ? "GridRow" : "GridAlternateRow" %>">
            <td><%= obj.Item.Balloon.Name%></td>
            <td><%= obj.Item.Balloon.Price.ToString("c")%></td>
            <td>
                <input type="hidden" name="items[<%= obj.Index %>].Key" value="<%= obj.Item.Balloon.Id %>" />
                <input type="text" name="items[<%= obj.Index %>].Value" maxlength="2" style="width:24px;" value="<%= obj.Item.Quantity %>" />
            </td>
            <td><%= (obj.Item.Balloon.Price * obj.Item.Quantity).ToString("c")%></td>
            <td><button value="<%= obj.Item.Balloon.Id %>" name="remove" class="SmallButtonText" formaction="<%= Url.Action("Remove", "Cart") %>">Delete</button></td>
        </tr>       
        <% } %>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="2">
                    <span class="ProductDescription">Total amount: </span>
                    <span class="ProductPrice"><%= ViewBag.Total.ToString("c") %></span>
                </td>
                <td colspan="3" style="text-align:right;">
                    <input type="submit" value="Update Quantities" class="SmallButtonText" />
                    <a href="<%= Url.Action("Checkout", "Cart") %>" class="SmallButtonText">Proceed to Checkout</a>
                </td>
            </tr>
        </tfoot>
    </table>
</form>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="navigation" runat="server">
    <% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
    <br />
    <% Html.RenderPartial("../Search/SearchForm"); %>
</asp:Content>
