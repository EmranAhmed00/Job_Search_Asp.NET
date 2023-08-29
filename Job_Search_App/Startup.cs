using Job_Search_App.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Job_Search_App
{

    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Skapa en dbcontext för att prata med Identity databasen. Hämta connectionstring från  
            //appsettings.json
            var conn = Configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));


            //Lägga till identity som en tjänst som applikationen kan använda
            services.AddIdentity<User, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Lägga till authentication
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "Default",
                template: "{controller=Home}/{action=Index}"
                    );

            });
        }
    }
}
