using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Staff.Repositories.Interfaces
{
    public interface IBaseRepository<TItem> where TItem : class 
    {
        Task<IEnumerable<TItem>> GetItemsAsync(string filter); 
        Task<TItem> FindItemAsync(string id, string partionKey);
        Task<TItem> AddItemAsync(TItem item, string partitionKey);
        Task<TItem> UpdateItemAsync(TItem item, string id, string etag, string partitionKey);
        Task<bool> RemoveItemAsync(string id, string partitionKey);
    }
}
