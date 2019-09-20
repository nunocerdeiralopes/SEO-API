using Microsoft.EntityFrameworkCore;
using SEO_API.Models;

namespace SEO_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }


        public DbSet<RecurringKeyword> RecurringKeyword { get; set; }
        public DbSet<RecurringKeywordPosition> RecurringKeywordPosition { get; set; }

    }
}
