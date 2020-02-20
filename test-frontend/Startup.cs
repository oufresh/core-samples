using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace test_frontend
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRouting();
            /*services.AddAntiforgery(options => {
                options.HeaderName = "X-XSRF-TOKEN"; }
            );*/
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /*app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });*/

            app.Use(async (context, next) =>
            {
                string path = context.Request.Path.Value;

                if (
                    string.Equals(path, "/", System.StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(path, "/index.html", System.StringComparison.OrdinalIgnoreCase))
                {
                    System.Console.WriteLine("GET index or default");
                    // The request token can be sent as a JavaScript-readable cookie, 
                    // and Angular uses it by default.
                    /* var tokens = antiforgery.GetAndStoreTokens(context);*/
                    context.Response.Cookies.Append("XSRF-TOKEN", "cane",
                      new CookieOptions() { HttpOnly = false });
                    
                    await next.Invoke();
                } 
                else if (path.IndexOf("test") >= 0) {
                    if (context.Request.Headers["X-XSRF-TOKEN"] != "cane") {
                        System.Console.Error.WriteLine("X-XSRF-TOKEN not found");
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Op not permitted");
                    } else
                        await next.Invoke();
                }
                else {
                    await next.Invoke();
                }
                
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/test", async context =>
                    {
                        System.Console.WriteLine("Resp to test");
                        await context.Response.WriteAsync("true");
                    });
                endpoints.MapDefaultControllerRoute();
            }).UseSpa(_ => { });
            //app.UseEndpoints().UseSpa(_ => { }); // extension from 'Microsoft.AspNetCore.SpaServices.Extensions' assembly
            /*app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapFallbackToController("Index", "Home");
                endpoints.MapGet("/test", async context =>
                {
                    await context.Response.WriteAsync("ok");
                });
            });*/
        }
    }
}
