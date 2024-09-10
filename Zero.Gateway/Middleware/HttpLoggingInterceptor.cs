using Microsoft.AspNetCore.HttpLogging;

namespace Zero.Gateway.Middleware
{
    public class HttpLoggingInterceptor : IHttpLoggingInterceptor
    {
        public ValueTask OnRequestAsync(HttpLoggingInterceptorContext logContext)
        {

            if (logContext.HttpContext.Request.Path.StartsWithSegments("/swagger"))
                logContext.LoggingFields = HttpLoggingFields.None;


            return ValueTask.CompletedTask;
        }

        public ValueTask OnResponseAsync(HttpLoggingInterceptorContext logContext)
        {
            throw new NotImplementedException();
        }
    }
}
