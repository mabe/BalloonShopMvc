<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/BalloonShop.master" Inherits="System.Web.Mvc.ViewPage<BalloonShop.Model.Balloon>" %>

<asp:Content runat="server" ContentPlaceHolderID="navigation">
    <% Html.RenderAction<DepartmentController>(x => x.Navigation(null));  %>
    <br />
    <% Html.RenderPartial("../Search/SearchForm"); %>
    <br />
    <% Html.RenderAction("Summary", "Cart"); %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">

  <br />
  <h1><%= Model.Name %></h1>
  <br />
  <br />
  <img src="/Content/ProductImages/<%= Model.Image %>" />
  <br />
  <p class="ProductDescription">
    <%= Model.Description %>
  </p>
  <br />
  <br />
  <span class="ProductDescription">Price:</span>
  <span class="ProductPrice"> <%= Model.Price.ToString("c") %></span>
  
  <br />
  <form method="post" action="<%= Url.Action("Add", "Cart") %>">
    <input type="hidden" name="balloonId" value="<%= Model.Id %>" />
    <input type="submit" value="Add to Cart" class="SmallButtonText" />
  </form>
  <%-- <asp:Button ID="addToCartButton" runat="server" Text="Add to Cart" CssClass="SmallButtonText" OnClick="addToCartButton_Click" />
  <asp:Button ID="continueShoppingButton" CssClass="SmallButtonText" runat="server" Text="Continue Shopping" OnClick="continueShoppingButton_Click" />
  --%>
  <br />
  <br />
  <%-- 
  <uc1:ProductRecommendations id="ProductRecommendations1" runat="server">
  </uc1:ProductRecommendations>
  --%>

</asp:Content>
