using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middleware.Demo
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IPrinter printer;

        public CustomMiddleware(RequestDelegate next, IPrinter printer)
        {
            this.next = next;
            this.printer = printer;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("Inside the new custom middleware");
            await next(httpContext);
            printer.Print();
        }
    }
}