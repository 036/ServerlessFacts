using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;

namespace Nulldev.FactGen
{
    public static class GenerateFact
    {
        [FunctionName("GenerateFact")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log, ExecutionContext executionContext)
        {
            var factPath = Path.Combine(executionContext.FunctionDirectory, @"../facts.txt");
            using (StreamReader file = File.OpenText(factPath)) 
            {
                var facts =  file.ReadToEnd().ReplaceLineEndings().Split(Environment.NewLine, StringSplitOptions.None);
                var random = new Random();
                var index = random.Next(facts.Length);

                return new OkObjectResult(facts[index]);
            }
        }
    }
}
