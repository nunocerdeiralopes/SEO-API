using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEO_API.Helper
{
    public static class UrlHelper
    {
        public static bool isValidUrl(string url)
        {
            Regex regex = new Regex(@"^(https?:\/\/)?(www\.)?([a-zA-Z0-9]+(-?[a-zA-Z0-9])*\.)+[\w]{2,}(\/\S*)?$",
                                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(url);
        }
    }
}
