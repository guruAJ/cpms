using CPMS_AUTO.Data;
using CPMS_AUTO.Helpers;
using CPMS_AUTO.Repository;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPMS_AUTO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        private IWebHostEnvironment CurrentEnvironment { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.WithOrigins("https://localhost:7000", "https://localhost:44300"));
            //});
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<ComponentRepository, ComponentRepository>();
            services.AddScoped<RepositorySponsor, RepositorySponsor>();
            services.AddScoped<RepositoryStatus, RepositoryStatus>();
            services.AddScoped<RepositoryVetting, RepositoryVetting>();
            services.AddScoped<RepositoryProject, RepositoryProject>();
            services.AddScoped<RepositoryUser, RepositoryUser>();
            services.AddScoped<RepositoryRemarks, RepositoryRemarks>();
            services.AddScoped<RepositoryProjectSupport, RepositoryProjectSupport>();
            services.AddScoped<RepositorySus, RepositorySus>();
            services.AddScoped<RepositoryHrms, RepositoryHrms>();
            services.AddScoped<RepositoryUnitStatus, RepositoryUnitStatus>();
            services.AddTransient<IProfileService, IdentityClaimsProfileService>();

            services.AddTransient<ITokenProvider, TokenProvider>();

            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;


            })


                 .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                  .AddClientStore<ClientStore>()
                .AddAspNetIdentity<ApplicationUser>()
                //.AddSigningCredential(certificate);
                .AddDeveloperSigningCredential()
                .AddProfileService<IdentityClaimsProfileService>();

            services.AddAuthentication()
        .AddOpenIdConnect("oidc", "Login With emIDAM", options =>
        {
            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            options.SignOutScheme = IdentityServerConstants.SignoutScheme;

            options.Authority = "https://152.1.3.8/emIDAM";
            options.ClientId = "5ae3471638584e5705676717fe514cd1671cdf";
            options.ClientSecret = "fe3811934fe148f7b066eadc103eca278313aa6b";

                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    NameClaimType = "name",
                //    RoleClaimType = "role"
                //};
            });
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            //app.UseCors(options => options.WithOrigins("https://localhost:7000", "https://localhost:44300"));
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
      

       
    }
}
