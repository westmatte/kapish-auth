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
        [FunctionName("timerClient")]
        public static async Task RunTimer([TimerTrigger("0 0 5 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            string apiToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://kapish-api.azurewebsites.net");
            Console.WriteLine(apiToken);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            var response = await httpClient.GetAsync(new Uri("https://kapish-api.azurewebsites.net/api/getSalesOrderDetails"));

            response.EnsureSuccessStatusCode();

            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
