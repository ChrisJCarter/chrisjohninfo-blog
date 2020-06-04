using System.Data;
using AutoMapper;
using ChrisJohnInfo.Blog.Contracts.Interfaces;
using ChrisJohnInfo.Blog.Core;
using ChrisJohnInfo.Blog.Core.Handlers;
using ChrisJohnInfo.Blog.Core.Services;
using ChrisJohnInfo.Blog.Core.Transformers;
using ChrisJohnInfo.Blog.MvcUI.Data;
using ChrisJohnInfo.Blog.Repositories.Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChrisJohnInfo.Blog.MvcUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddSingleton<MarkdownTransformer>();
            services.AddSingleton<RazorTransformer>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            if (CurrentEnvironment.IsDevelopment())
            {
                services.AddSingleton<IStaticResourceHandler>(new LocalStaticResourceHandler(Configuration["storage-connection-string"]));
            }
            else
            {
                services.AddSingleton<IStaticResourceHandler>(new RemoteStaticResourceHandler(Configuration["storage-connection-string"]));
            }
            //services.AddDbContext<ChrisJohnInfoBlogContext>(options =>
            //    options.UseSqlServer(Configuration["sql-ChrisJohnInfoBlog-001"]));
            services.AddScoped<IDbConnection>(provider => new SqlConnection(Configuration["sql-ChrisJohnInfoBlog-001"]));
            services.AddAutoMapper(typeof(AdminRepository));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["sql-ChrisJohnInfoBlog-001"]));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Posts/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "admin", 
                    areaName: "admin", 
                    pattern: "admin/{controller=posts}/{action=index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Posts}/{action=Index}/{id?}");

                

                endpoints.MapRazorPages();
            });
        }
    }
}
