using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<RecurringKeywordPosition>> GetRecurringKeywordPosition(int id)
        {
            var recurringKeywordPosition = await _context.RecurringKeywordPosition.FindAsync(id);

            if (recurringKeywordPosition == null)
            {
                return NotFound();
            }

            return recurringKeywordPosition;
        }
    }
}
