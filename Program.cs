using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Utility;
using SearchScopeAPI.SearchScope.DataAccess.Data;
using SearchScopeAPI.SearchScope.DataAccess.Repositories;
using SearchScopeAPI.SerachScope.API.Logger;
using SearchScopeAPI.SerachScope.API.Middleware;
using System.Reflection;
using System.Text;

namespace SearchScopeAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Bind JwtSettings from configuration(appSettings.json)
            var jwtSettings = new JwtSettings();
            builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);
            builder.Services.AddSingleton(jwtSettings);

            // Configure Authentication and Authorization
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            // Enable Authorization. 
            builder.Services.AddAuthorization();

            // Register CustomLogger.
            builder.Services.AddSingleton<CustomLogger>();

            // Register DbContext
            builder.Services.AddDbContext<SearchScopeDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SearchScopeSqlConnection")));

            builder.Services.AddControllers();

            // Register Mediator
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            // Register Repositories
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
            builder.Services.AddScoped<ISearchResultRepository, SearchResultRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // Configure Swagger for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.MapType<ProductEnum>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(ProductEnum))
                    .Select(name => (IOpenApiAny)new OpenApiString(name)) // Explicit cast added
                    .ToList()
                });

                c.MapType<SearchResultEnum>(() => new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(SearchResultEnum))
                    .Select(name => (IOpenApiAny)new OpenApiString(name)) // Explicit cast added
                    .ToList()
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SearchScope API", Version = "v1" });

                // Add JWT Bearer Authorization in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and your token."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            // Developer exception page for detailed debugging during development
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Global exception handler middleware
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            // Swagger setup
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SearchScope API v1");
            });

            // HTTPS redirection middleware
            app.UseHttpsRedirection();

            // Routing middleware
            app.UseRouting();

            // Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers explicitly (modern approach)
            app.MapControllers();

            // Run the application
            app.Run();

        }
    }
}
