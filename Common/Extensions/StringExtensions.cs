using System.Linq;

namespace ShoppingApp.WebAPI.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToFirstUpper(this string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}