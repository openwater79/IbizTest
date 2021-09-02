using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using IbizTest.Models;
using IbizTest.Storage;

namespace IbizTest
{
    public static class AddProduct
    {
        [FunctionName("AddProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "products/add")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var product = JsonConvert.DeserializeObject<Product>(requestBody);

                await new ProductRepository().AddAsync(product);
            }
            catch(Exception ex)
            {
                log.LogError(ex, "Add product error");
                return new BadRequestResult();
            }

            string responseMessage = "Product inserted succesfully";

            return new OkObjectResult(responseMessage);
        }

    }
}
