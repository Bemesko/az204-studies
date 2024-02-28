using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace func
{
    public class Echo
    {
        private readonly ILogger _logger;

        public Echo(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Echo>();
        }

        [Function("Echo")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Função C# Echo Disparada");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            StreamReader streamReader = new StreamReader(req.Body);
            string requestBody = streamReader.ReadToEnd();
            response.WriteString(requestBody);

            return response;
        }
    }
}
