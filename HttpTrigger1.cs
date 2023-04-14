using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class HttpTrigger1
    {
        private readonly MyService _myService;
        private readonly ILogger<HttpTrigger1> _log;


        public HttpTrigger1(MyService myService, ILogger<HttpTrigger1> log)
        {
            _myService = myService;
            _log = log;
        }

        [FunctionName("HttpTriggerGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            await _myService.Login();

            return new OkObjectResult("OK");
        }

        [FunctionName("HttpTriggerPost")]
        public async Task<IActionResult> RunPost(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _log.LogInformation(requestBody);
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return new OkObjectResult(data);
        }
    }
}
