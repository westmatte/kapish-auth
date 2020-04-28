using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Services.AppAuthentication;
using System.Net.Http;
using System.Net.Http.Headers;

namespace client
{
    public static class client
    {
        [FunctionName("client")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            string apiToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://kapish-api.azurewebsites.net");
            log.LogInformation(apiToken);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            var response = await httpClient.GetAsync(new Uri("https://kapish-api.azurewebsites.net/api/api"));

            response.EnsureSuccessStatusCode();

            return new OkObjectResult(await response.Content.ReadAsStringAsync());
        }
    }
}
