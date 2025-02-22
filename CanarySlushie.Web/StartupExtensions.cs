using CanarySlushie.Core.Configurations;
using CanarySlushie.Core.Exceptions;
using Serilog;
namespace CanarySlushie.Web;

public static class StartupExtensions
{
	public static WebApplication ConfigureServices
		(this WebApplicationBuilder builder)
	{

		// var debugMode = builder.Environment.IsDevelopment();
		// if (debugMode)
		// {
		// 	builder.Configuration.AddUserSecrets<Program>();
		// }
		// else
		// {
		// 	builder.Configuration.AddEnvironmentVariables();
		// }
		//
		// var appSettings = builder.Configuration.GetSection("AppSettings");
		// builder.Services.Configure<Settings>(appSettings);

		builder.Services.AddSerilog((services, lc) => lc
			.ReadFrom.Configuration(builder.Configuration)
			.ReadFrom.Services(services)
			.WriteTo.Console());

		builder.Services.AddAppServices();
		// builder.Services.AddInfrastructureServices(builder.Configuration);
		builder.Services.AddPersistenceServices(builder.Configuration);

		builder.Services.AddMemoryCache();

		builder.Services.AddControllers();

		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		builder.Services.AddProblemDetails();

		return builder.Build();
	}

	public static WebApplication ConfigurePipeline
		(this WebApplication app)
	{
		// if (app.Environment.IsDevelopment())
		// {
		// 	app.UseSwagger();
		// 	app.UseSwaggerUI();
		// }

		app.UseStaticFiles();

		app.UseExceptionHandler();

		app.UseRouting();

		// app.UseAuthentication();
		// app.UseAuthorization();

		app.MapControllers();
		app.MapFallbackToFile("index.html");

		return app;
	}

	// public static async Task MigrateDatabase(this WebApplication app)
	// {
	// 	using var scope = app.Services.CreateScope();
	// 	try
	// 	{
	// 		var context = scope.ServiceProvider.GetService<CanarySlushieDbContext>();
	// 		if (context != null)
	// 		{
	// 			await context.Database.MigrateAsync();
	// 		}
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		//add logging here later on
	// 	}
	// }
}