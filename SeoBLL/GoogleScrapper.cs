using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeoBLL
{
    public static class GoogleScrapper
    {
        public static async Task<List<int>> GoogleResultsScrapper(string query, string url, string countryCodeDomain, string searchsNumer)
        {
            string formatedUrl = string.Format("https://www.google.{0}/search?num={1}&q={2}", countryCodeDomain, searchsNumer, query);

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(formatedUrl);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            //divs that identify the pages cards
            var selectNodes = doc.DocumentNode.SelectNodes("//div[@class='ZINbbc xpd O9g5cc uUPGi']/div[@class='kCrYT']/a[1]");
            List<int> positions = new List<int>();

            for(var i = 0; i < selectNodes.Count; i++)
            {
                //search for the href attributes and comparing to the given url
                if (selectNodes[i].GetAttributeValue("href", string.Empty).Contains(url))
                {
                    positions.Add(i + 1);
                }
            }

            if (positions.Count == 0)
                positions.Add(0);

            return positions;
        }
    }
}
