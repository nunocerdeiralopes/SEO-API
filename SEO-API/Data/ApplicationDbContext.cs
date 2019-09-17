using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using SEO_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SEO_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<RecurringKeyword> RecurringKeyword { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SeoAPIDatabase"].ConnectionString);
    }
}
}
