using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using SEO_API.Data;
using SEO_API.Models;
using SeoBLL;

namespace SEO_API.Jobs
{
    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    public class RecurringJobs
    {
        private readonly ApplicationDbContext _context;


        public RecurringJobs(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task GoogleScrappingJob(string query, string url, string countryDomain, int recurringKeyworId)
        {
            var results = await GoogleScrapper.GoogleResultsScrapper(query, url, countryDomain, "100");

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