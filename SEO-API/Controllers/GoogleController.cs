using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SEO_API.Helper;
using SEO_API.Models;
using SeoBLL;

namespace SEO_API.Controllers
{
    [Route("api/[controller]")]
    public class GoogleController : Controller
    {
        private readonly AppSettings _appSettings;

        public GoogleController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet("{query}/{url}/{countryCode}")]
        public async Task<ActionResult<List<int>>> Get(string query, string url, string countryCode)
        {
            if (!UrlHelper.isValidUrl(url) || !UrlHelper.isValidCountryCode(countryCode))
                return BadRequest();

            var results = await GoogleScrapper.GoogleResultsScrapper(query, url, countryCode, _appSettings.NumberOfResults);
            return results;
        }
    }
}
