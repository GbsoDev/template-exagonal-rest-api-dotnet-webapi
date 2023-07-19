using AutoMapper;
using GbsoDevExagonalTemplate.Dtos.MapperConfigurations;
using GbsoDevExagonalTemplate.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GbsoDevExagonalTemplate.Infrastructure.providers
{
	internal static class WebApiPrivider
	{
		internal static IServiceCollection AddWebApiTokenAuthentication(this IServiceCollection services, AppSettings appSettings)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
			{
				o.Audience = appSettings.AuthOptions.Audience;
				o.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = appSettings.AuthOptions.Issuer,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.AuthOptions.SigningKey))
				};
			});
			return services;
		}

		internal static IServiceCollection AddMapperConfigurations(this IServiceCollection services)
		{
			return services.AddSingleton(
				new MapperConfiguration(mc => mc.AddProfile(typeof(UserMapperConfiguration))).CreateMapper()
			);
		}

		internal static IServiceCollection AddWebApiCorsPolicies(this IServiceCollection services, AppSettings appSettings)
		{
			return services.AddCors(options =>
			{
				foreach (var corPolicy in appSettings.AllowCors)
				{
					options.AddPolicy(corPolicy.Origin, builder =>
					{
						var policy = builder.WithOrigins(corPolicy.Origin)
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials();

						if (corPolicy.Methods?.Any() ?? false)
						{
							policy.WithMethods(corPolicy.Methods);
						}
					});
				}
			});
		}

		internal static IApplicationBuilder AddPosBuildWebApiCorsPolicies(this IApplicationBuilder app, AppSettings appSettings)
		{
			app.UseCors(builder =>
			{
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
			});

			foreach (var corPolicy in appSettings.AllowCors)
			{
				app.UseCors(corPolicy.Name);
			}
			return app;
		}
	}
}
