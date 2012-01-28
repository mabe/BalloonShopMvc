using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalloonShop.Mvc.Helpers
{
    public static class StringHelpers
    {
        public static string ShortenText(this string text) {
            return text.Length > BalloonShopConfiguration.ProductDescriptionLength ? text.Substring(0, BalloonShopConfiguration.ProductDescriptionLength) + "..." : text;
        }
    }
}