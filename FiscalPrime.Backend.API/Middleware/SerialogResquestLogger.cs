using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiscalPrime.Backend.API.Middleware
{
    public class SerialogResquestLogger
    {
        readonly RequestDelegate _next;

        public SerialogResquestLogger(RequestDelegate next)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            string requestBody = "";
            HttpRequestRewindExtensions.EnableBuffering(httpContext.Request);
            Stream body = httpContext.Request.Body;
            byte[] buffer = new byte[Convert.ToInt32(httpContext.Request.ContentLength)];
            await httpContext.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            httpContext.Request.Body = body;

            try
            {
                await _next(httpContext);

                if (httpContext.Response.StatusCode == (int)System.Net.HttpStatusCode.InternalServerError)
                {
                    Log.ForContext("RequestHeaders", httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                        .ForContext("RequestBody", requestBody)
                        .Error("Request information {RequestMethod} {RequestPath} {StatusCode}", httpContext.Request.Method, httpContext.Request.Path, httpContext.Response.StatusCode);
                }
                else
                {
                    Log.ForContext("RequestHeaders", httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                        .ForContext("RequestBody", requestBody)
                        .Debug("Request information {RequestMethod} {RequestPath} {StatusCode}", httpContext.Request.Method, httpContext.Request.Path, httpContext.Response.StatusCode);
                }
            }
            catch (Exception exception)
            {
                Guid errorId = Guid.NewGuid();

                Log.ForContext("Type", "Error")
                    .ForContext("Exception", exception, destructureObjects: true)
                    .Error(exception, exception.Message + ". {@errorId}", errorId);

                var result = JsonConvert.SerializeObject(new { error = "Sorry, an unexpected error has occurred", errorId = errorId });
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
