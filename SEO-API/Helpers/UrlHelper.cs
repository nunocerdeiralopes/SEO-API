using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SEO_API.Helper
{
    public static class UrlHelper
    {
        static string[] countryCodes = { "co.uk", "com.au", "com" };
        public static bool isValidUrl(string url)
        {
            Regex regex = new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$",
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(WebUtility.UrlDecode((url)));
        }

        public static bool isValidCountryCode(string countryDomain)
        {
            return Array.Exists(countryCodes, x => x == countryDomain);
        }
    }
}
