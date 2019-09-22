using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SEO_API.Helper
{
    public static class UrlHelper
    {
        static string[] countryCodes = { "co.uk", "com.au", "com" };

        /// <summary>
        /// Checks if the Url is valid.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool isValidUrl(string url)
        {
            Regex regex = new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$",
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(WebUtility.UrlDecode((url)));
        }

        /// <summary>
        /// Checks if the google domain is valid.
        /// </summary>
        /// <param name="countryDomain"></param>
        /// <returns></returns>
        public static bool isValidCountryCode(string countryDomain)
        {
            return Array.Exists(countryCodes, x => x == countryDomain);
        }
    }
}
