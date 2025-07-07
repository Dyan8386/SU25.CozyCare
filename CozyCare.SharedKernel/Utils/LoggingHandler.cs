using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCare.SharedKernel.Utils
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingHandler> _logger;

        public LoggingHandler(ILogger<LoggingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation("Request: {Method} {Uri}", request.Method, request.RequestUri);

            var response = await base.SendAsync(request, cancellationToken);

            sw.Stop();
            _logger.LogInformation("Response: {StatusCode} in {Elapsed}ms", response.StatusCode, sw.ElapsedMilliseconds);
            return response;
        }
    }
}
