using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace api
{
    public static class api
    {
        [FunctionName("getSalesOrderDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var sqlClient = await DBConnect.GetConnectionAsync(Environment.GetEnvironmentVariable("DBConnectionString"));
            var result = await sqlClient.QueryAsync<object>("select * from SalesLT.SalesOrderDetail");

            if (result.Any())
            {
                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }
    }
}
