using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using CanarySlushie.Core.Behaviors;
using Scrutor;

namespace CanarySlushie.Core.Configurations;

public static class AppServiceCollection
{
	public static IServiceCollection AddAppServices
		(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(typeof(AppServiceCollection).Assembly);

		services.AddMediatR(cfg =>
		{
			// Order matters
			cfg.RegisterServicesFromAssemblies(typeof(AppServiceCollection).Assembly);
			//cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
			cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
		});

		services.Scan(selector => selector.FromAssemblies(typeof(AppServiceCollection).Assembly)
			.AddClasses(false)
			.UsingRegistrationStrategy(RegistrationStrategy.Skip)
			.AsMatchingInterface()
			.WithTransientLifetime());

		return services;
	}
}