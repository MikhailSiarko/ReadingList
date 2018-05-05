using Api.Authentication.AuthenticationOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSingleton<IJwtOptions, JwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(ConfigureJwtBearer);
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseMvc();
        }

        private static void ConfigureJwtBearer(JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = ConfigureTokenValidationParameters();
        }

        private static TokenValidationParameters ConfigureTokenValidationParameters()
        {
            var jwtOptions = new JwtOptions();
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };
        }
    }
}