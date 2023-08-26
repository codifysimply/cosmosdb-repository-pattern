using CS.Staff.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Staff.Repositories
{
    public class BaseRepository<TItem> : IBaseRepository<TItem> where TItem : class
    {
        private readonly Container container;
        public BaseRepository(CosmosClient cosmosClient, string databaseId, string containerId)
        {
            container = cosmosClient.GetContainer(databaseId, containerId);
        }
        public async Task<TItem> AddItemAsync(TItem item, string partitionKey)
        {
            return await container.CreateItemAsync<TItem>(item, new PartitionKey(partitionKey)).ConfigureAwait(false); 
        }
        public async Task<TItem> FindItemAsync(string id, string partionKey)
        {
            try
            {
                ItemResponse<TItem> item = await container.ReadItemAsync<TItem>(id, new PartitionKey(partionKey)).ConfigureAwait(false);
                return item;

            } catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
        public async Task<IEnumerable<TItem>> GetItemsAsync(string filter)
        {
            filter = string.IsNullOrEmpty(filter) ? "select * from item" : $"select * from item where {filter}";

            var filteredFeed = container.GetItemQueryIterator<TItem>(new QueryDefinition(filter));

            List<TItem> items = new();

            while (filteredFeed.HasMoreResults)
            {
                var response = await filteredFeed.ReadNextAsync().ConfigureAwait(false);

                items.AddRange(response.ToList());
            }

            return items;

        }
        public async Task<TItem> UpdateItemAsync(TItem item, string id, string etag, string partitionKey)
        {
            try
            {
                return await container.ReplaceItemAsync<TItem>(item, id, new PartitionKey(partitionKey), new ItemRequestOptions { IfMatchEtag = etag }).ConfigureAwait(false);

            }
            catch(CosmosException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task<bool> RemoveItemAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<TItem> item = await container.DeleteItemAsync<TItem>(id, new PartitionKey(partitionKey)).ConfigureAwait(false);
                return true;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }
    }
}
