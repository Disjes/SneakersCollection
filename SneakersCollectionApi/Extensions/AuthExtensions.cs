using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SneakersCollection.Api.Models;

namespace SneakersCollection.Api.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuthSettings(this IServiceCollection Services, ConfigurationManager config)
        {
            var swaggerOptions = config.GetSection("Swagger").Get<SwaggerOptions>();
            var jwtOptions = config.GetSection("Jwt").Get<JwtOptions>();

            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerOptions.ApiVersion, new OpenApiInfo { Title = swaggerOptions.ApiTitle, Version = swaggerOptions.ApiVersion });

                var scheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(swaggerOptions.AuthorizationUrl),
                            TokenUrl = new Uri(swaggerOptions.TokenUrl),
                            Scopes = new Dictionary<string, string>
                            {
                                { swaggerOptions.Scope1, swaggerOptions.Scope2 }
                            }
                        }
                    },
                    Type = SecuritySchemeType.OAuth2
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme"
                });

                c.AddSecurityDefinition("OAuth", scheme);
                c.AddSecurityDefinition("AccountsOpenID", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OpenIdConnect,
                    OpenIdConnectUrl = new Uri(swaggerOptions.OpenIdConnectUrl)
                });
            });

            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = jwtOptions.Authority;
                    options.Audience = jwtOptions.Audience;

                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });
        }

        public static void AddSwaggerUISettings(this WebApplication app, ConfigurationManager config)
        {
            var swaggerOptions = config.GetSection("Swagger").Get<SwaggerOptions>();
            var jwtOptions = config.GetSection("Jwt").Get<JwtOptions>();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerOptions.ApiTitle);

                c.OAuthClientId(swaggerOptions.ClientId);
                c.OAuthClientSecret(swaggerOptions.ClientSecret);
                c.OAuthUsePkce();
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
                {
                    { "audience", swaggerOptions.Audience }
                });
                c.OAuth2RedirectUrl(swaggerOptions.RedirectUrl);
                c.OAuthUsePkce();
            });
        }
    }
}
