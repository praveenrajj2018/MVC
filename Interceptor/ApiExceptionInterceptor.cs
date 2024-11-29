using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace LoginApp.Interceptor
{
    public class ApiExceptionInterceptor : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            Log.Error(exception, "An unhandled exception occurred.");

            context.ExceptionHandled = true;
            var result = new ViewResult
            {
                ViewName = "/Views/Shared/Error.cshtml"
            };
            context.Result = result;

            await Task.CompletedTask;
        }
    }
}
