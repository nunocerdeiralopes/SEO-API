﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeoBLL
{
    public static class GoogleScrapper
    {
        /// <summary>
        /// Returns the google ranking for the given query, url in the chosen google domain.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="url"></param>
        /// <param name="countryCodeDomain"></param>
        /// <param name="searchsNumer"></param>
        /// <returns></returns>
        public static async Task<List<int>> GoogleResultsScrapper(string query, string url, string countryCodeDomain, string searchsNumer)
        {
            string formatedUrl = string.Format("https://www.google.{0}/search?num={1}&q={2}", countryCodeDomain, searchsNumer, query);

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(formatedUrl);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            //divs that identify the pages cards
            //these values might change
            var selectNodes = doc.DocumentNode.SelectNodes("//div[@class='ZINbbc xpd O9g5cc uUPGi']/div[@class='kCrYT']/a[1]");
            List<int> positions = new List<int>();


            //decode url
            url = WebUtility.UrlDecode(url);

            for (var i = 0; i < selectNodes.Count; i++)
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
