using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CanarySlushie.Core.Persistence;
namespace CanarySlushie.Core.Configurations;

public static class PersistenceServiceCollection
{
	public static IServiceCollection AddPersistenceServices
		(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("CanarySlushieConnectionString");
		// var serverVersion = new MySqlServerVersion(new Version(10, 6, 18));

		// services.AddDbContext<CanarySlushieDbContext>(options =>
		// 	options.UseMySql(connectionString, serverVersion));

		// services.AddSingleton<IAppCache, AppCache>();

		return services;
	}
}