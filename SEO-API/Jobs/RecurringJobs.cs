using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Options;
using SEO_API.Data;
using SEO_API.Models;
using SeoBLL;

namespace SEO_API.Jobs
{
    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    public class RecurringJobs
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public RecurringJobs(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task GoogleScrappingJob(string query, string url, string countryDomain, int recurringKeyworId)
        {
            var results = await GoogleScrapper.GoogleResultsScrapper(query, url, countryDomain, _appSettings.NumberOfResults);

            _context.RecurringKeywordPosition.Add(new RecurringKeywordPosition
                {
                    RecurringKeyworId = recurringKeyworId,
                    Date = DateTime.Now,
                    Positions = string.Join(", ", results)
            });
            await _context.SaveChangesAsync();
        }
    }
}