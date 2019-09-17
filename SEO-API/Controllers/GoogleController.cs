using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeoBLL;

namespace SEO_API.Controllers
{
    [Route("api/[controller]")]
    public class GoogleController : Controller
    {
        [HttpGet("{query}/{url}/{countryCode}")]
        public async Task<List<int>> Get(string query, string url, string countryCode)
        {
            var results = await GoogleScrapper.GoogleResultsScrapper(query, url, countryCode, "100");
            return results;
        }
    }
}
