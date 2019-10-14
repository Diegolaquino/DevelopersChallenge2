using Microsoft.EntityFrameworkCore;

namespace NiboOFX.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<TransactionT> Transactions { get; set; }
    }
}
