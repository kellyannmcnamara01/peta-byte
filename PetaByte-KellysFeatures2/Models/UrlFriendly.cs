using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace PetaByte_KellysFeatures2.Models
{
    public static class UrlFriendly
    {
        public static string GoUrlFriendly(this string url)
        {
            //replace spaces with dashes
            url = url.ToLowerInvariant().Replace(" ", "-");
            //regex to make sure only letters numbers and dashes are being used in the url
            url = Regex.Replace(url, @"[^0-9a-z-]", string.Empty);

            return url;
        }
    }
}