using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class CosmosDBTrigger
    {
        [FunctionName("CosmosDBTrigger")]
        public static void Run([CosmosDBTrigger(
            databaseName: "azure-function-demo",
            collectionName: "demo-data",
            ConnectionStringSetting = "azuredemocosmos_DOCUMENTDB",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists=true)]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                foreach(var document in input)
                {
                    var name = document.GetPropertyValue<string>("name");
                    log.LogInformation($"Document info {document.Id}, {name}");
                }
            }
        }
    }
}
