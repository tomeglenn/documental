﻿using System.Net;
using System.Threading.Tasks;
using Documental.Config;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Documental.Contributors
{
    public class DeleteDatabaseIfExistsContributor : DocumentDbCreationStrategyContributor
    {
        public async override Task OnPreCreate(DocumentClient client, IDocumentDbConfiguration configuration)
        {
            try
            {
                await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(configuration.DatabaseName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }
            }
        }
    }
}
