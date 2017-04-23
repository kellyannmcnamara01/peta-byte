using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

//  Project: Temiskaming Hospital Website
//  Team Name: PetaByte
//  Class: Mobile Development
//  Professor: Lee Situ
//  Author: Kelly Ann McNamara
//  File Description: This file sets a regex for the pageUrl. It is currently not being used. Please see other comments below for specific details.


namespace PetaByte_KellysFeatures2.Models
{
    public static class UrlFriendly
    {
        public static string GoUrlFriendly(this string url)
        {
            //NOTE (Kelly Ann McNamara): replace spaces with dashes
            url = url.ToLowerInvariant().Replace(" ", "-");
            //NOTE (Kelly Ann McNamara): regex to make sure only letters numbers and dashes are being used in the url
            url = Regex.Replace(url, @"[^0-9a-z-]", string.Empty);

            return url;
        }
    }
}