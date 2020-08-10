using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Middleware.Demo
{
    public static class AppMiddlewareExtensions
    {
        public static void UseExtensions(this IApplicationBuilder app)
        {
            app.Use(async (context, next) => {
                await context.Response.WriteAsync("<html><body>Response from First middleware ");
                await next();
                await context.Response.WriteAsync("<br>End of First middleware</body></html>");
            });

            app.UseWhen(c => c.Request.Query.ContainsKey("role"), a => {
                a.Run(async context => {
                    await context.Response.WriteAsync($"<br>Role is {context.Request.Query["role"]}");
                });
            });

            app.Map("/map", a => {
                a.Map("/branch", x => x.Run(async context => await context.Response.WriteAsync($"<br>New child branch")));
                a.Run(async context => {
                    await context.Response.WriteAsync($"<br>New branch map");
                });
            });

            app.MapWhen(c => c.Request.Query.ContainsKey("count"), a => {
                a.Run(async context => {
                    await context.Response.WriteAsync($"<br>Count is {context.Request.Query["count"]}");
                });
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("<br>Response from Second middleware");
            });
        }
    }
}
