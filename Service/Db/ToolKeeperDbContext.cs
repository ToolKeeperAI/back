using Microsoft.EntityFrameworkCore;
using Service.Model;

namespace Service.Db
{
	public class ToolKeeperDbContext : DbContext
	{
        public DbSet<Tool> Tools { get; set; }

        public DbSet<Inventory> Inventory { get; set; }
        
        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<ToolUsage> ToolUsages { get; set; }

        public ToolKeeperDbContext()
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
