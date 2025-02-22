using Microsoft.EntityFrameworkCore;
namespace CanarySlushie.Core.Persistence;

public class CanarySlushieDbContext(DbContextOptions<CanarySlushieDbContext> options) : DbContext(options)
{
	// public DbSet<Class> ClassNamePlural => Set<Class>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CanarySlushieDbContext).Assembly);
	}
}