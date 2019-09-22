using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEO_API.Data;
using SEO_API.Models;

namespace SEO_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecurringKeywordPositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecurringKeywordPositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RecurringKeywordPosition
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecurringKeywordPosition>>> GetRecurringKeywordPosition()
        {
            return await _context.RecurringKeywordPosition.ToListAsync();
        }

        // GET: api/RecurringKeywordPosition/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GraphData>> GetRecurringKeywordPosition(int id)
        {
            var recurringKeyword = await _context.RecurringKeyword.FindAsync(id);

            if (recurringKeyword == null)
            {
                return NotFound();
            }

            GraphData data = new GraphData
            {
               X =  _context.RecurringKeywordPosition.Where(x => x.RecurringKeyworId == id)
                            .OrderByDescending(x=>x.RecurringKeywordPositionId)
                            .Take(7)
                            .Select(x => x.Date.ToString("dd/MM/yyyy"))
                            .ToList(),
               Y =  _context.RecurringKeywordPosition
                            .Where(x => x.RecurringKeyworId == id)
                            .OrderByDescending(x => x.RecurringKeywordPositionId)
                            .Take(7)
                            .Select(x => Convert.ToInt32(x.Positions.Split(',', StringSplitOptions.None)[0]))
                            .ToList()
            };

            return data;
        }
    }
}
