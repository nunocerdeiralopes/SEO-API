using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEO_API.Data;
using SEO_API.Jobs;
using SEO_API.Models;

namespace SEO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurringKeywordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecurringKeywordController(ApplicationDbContext context)
        {
            _context = context;
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
        [HttpPost]
        public async Task<ActionResult<RecurringKeyword>> PostRecurringKeyword(RecurringKeyword recurringKeyword)
        {
            _context.RecurringKeyword.Add(recurringKeyword);
            await _context.SaveChangesAsync();

            RecurringJobs scrappingInstance = new RecurringJobs(_context);
            //cron job running every day at 7 am to get the position for the day
            RecurringJob.AddOrUpdate(() => scrappingInstance.GoogleScrappingJob(recurringKeyword.Query,recurringKeyword.Url,recurringKeyword.CountryDomain, recurringKeyword.RecurringKeyworId), "	0 0 7 1/1 * ? *");

            return CreatedAtAction("GetRecurringKeyword", new { id = recurringKeyword.RecurringKeyworId }, recurringKeyword);
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

            _context.RecurringKeyword.Remove(recurringKeyword);
            await _context.SaveChangesAsync();

            return recurringKeyword;
        }

        private bool RecurringKeywordExists(int id)
        {
            return _context.RecurringKeyword.Any(e => e.RecurringKeyworId == id);
        }
    }
}
