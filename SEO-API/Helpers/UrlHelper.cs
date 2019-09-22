using System;
using System.Text.RegularExpressions;

namespace SEO_API.Helper
{
    public static class UrlHelper
    {
        static string[] countryCodes = { "co.uk", "com.au", "com" };
        public static bool isValidUrl(string url)
        {
            Regex regex = new Regex(@"^(https?:\/\/)?(www\.)?([a-zA-Z0-9]+(-?[a-zA-Z0-9])*\.)+[\w]{2,}(\/\S*)?$",
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(url);
        }

        public static bool isValidCountryCode(string countryDomain)
        {
            return Array.Exists(countryCodes, x => x == countryDomain);
        }
    }
}
