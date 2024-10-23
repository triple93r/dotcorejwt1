using JwtApiPractice1.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtApiPractice1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<usertbl> usertbl { get; set; }
        public DbSet<usrtbl2> usrtbl2 { get; set; }
    }
}
