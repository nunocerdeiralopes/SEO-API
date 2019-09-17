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
        [HttpGet("{query}/{url}")]
        public async Task<List<int>> Get(string query, string url)
        {
            var results = await GoogleScrapper.GoogleResultsScrapper(query, url, "co.uk", "100");
            return results;
        }
    }
}
