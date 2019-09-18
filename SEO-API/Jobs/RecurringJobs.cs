using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using SeoBLL;

namespace SEO_API.Jobs
{
    [AutomaticRetry(Attempts = 3, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    public static class RecurringJobs
    {
        public static async Task<List<int>> GoogleScrappingJob(string query, string url, string countryCode)
        {
            var results = await GoogleScrapper.GoogleResultsScrapper(query, url, countryCode, "100");
            return results;
        }
    }
}