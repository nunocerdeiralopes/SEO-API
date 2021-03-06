﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SEO_API.Data;
using SEO_API.Helper;
using SEO_API.Jobs;
using SEO_API.Models;

namespace SEO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurringKeywordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<AppSettings> options;

        public RecurringKeywordController(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            options = appSettings;
        }

        // GET: api/RecurringKeyword
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RecurringKeyword>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<RecurringKeyword>>> GetRecurringKeyword()
        {
            return await _context.RecurringKeyword.ToListAsync();
        }

        // GET: api/RecurringKeyword/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecurringKeyword), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RecurringKeyword>> GetRecurringKeyword(int id)
        {
            var recurringKeyword = await _context.RecurringKeyword.FindAsync(id);

            if (recurringKeyword == null)
            {
                return NotFound();
            }

            return recurringKeyword;
        }

        // PUT: api/RecurringKeyword/5
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutRecurringKeyword(int id, RecurringKeyword recurringKeyword)
        {
            if (id != recurringKeyword.RecurringKeyworId)
            {
                return BadRequest();
            }

            _context.Entry(recurringKeyword).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecurringKeywordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecurringKeyword
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult<RecurringKeyword>> PostRecurringKeyword(RecurringKeyword recurringKeyword)
        {
            if (!UrlHelper.isValidUrl(recurringKeyword.Url) || !UrlHelper.isValidCountryCode(recurringKeyword.CountryDomain))
                return BadRequest();

            if (!NewRecurringKeywordExistsAlready(recurringKeyword))
            {
                _context.RecurringKeyword.Add(recurringKeyword);
                await _context.SaveChangesAsync();

                RecurringJobs scrappingInstance = new RecurringJobs(_context, options);
                //cron job running every day at 7 am to get the position for the day
                RecurringJob.AddOrUpdate("RecurringKeyword-" + recurringKeyword.RecurringKeyworId, 
                                        () => scrappingInstance.GoogleScrappingJob(recurringKeyword.Query, recurringKeyword.Url, recurringKeyword.CountryDomain, recurringKeyword.RecurringKeyworId), 
                                        Cron.Daily);

                //create fake data do show on graph
                for (int i = 0; i < 7; i++)
                {
                    Random random = new Random();
                    _context.RecurringKeywordPosition.Add(new RecurringKeywordPosition
                    {
                        RecurringKeyworId = recurringKeyword.RecurringKeyworId,
                        Date = DateTime.Today.AddDays(-i-1),
                        Positions = Convert.ToInt32((random.NextDouble() * (10 - 1) + 1)).ToString()
                    });
                }
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRecurringKeyword", new { id = recurringKeyword.RecurringKeyworId }, recurringKeyword);
            }
            else
                return Conflict();
        }

        // DELETE: api/RecurringKeyword/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<RecurringKeyword>> DeleteRecurringKeyword(int id)
        {
            var recurringKeyword = await _context.RecurringKeyword.FindAsync(id);
            if (recurringKeyword == null)
            {
                return NotFound();
            }

            RecurringJob.RemoveIfExists(recurringKeyword.RecurringKeyworId.ToString());
            _context.RecurringKeywordPosition.RemoveRange(_context.RecurringKeywordPosition.Where(x => x.RecurringKeyworId == recurringKeyword.RecurringKeyworId));
            _context.RecurringKeyword.Remove(recurringKeyword);
            await _context.SaveChangesAsync();

            return recurringKeyword;
        }

        private bool RecurringKeywordExists(int id)
        {
            return _context.RecurringKeyword.Any(e => e.RecurringKeyworId == id);
        }

        private bool NewRecurringKeywordExistsAlready(RecurringKeyword recurringKeyword)
        {
            return _context.RecurringKeyword.Any(e => e.Query == recurringKeyword.Query && e.Url == recurringKeyword.Url && e.CountryDomain == recurringKeyword.CountryDomain);
        }
    }
}
