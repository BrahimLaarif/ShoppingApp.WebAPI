using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Common.Utilities
{
    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = (ICollection) value;

            if (list != null)
            {
                return list.Count > 0;
            }
            
            return false;
        }
    }
}