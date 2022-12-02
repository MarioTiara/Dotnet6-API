using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetItems();
        Task<IEnumerable<Item>> GetItemsAsync();
        Item GetItem(Guid id);
        Task<Item> GetItemAsync(Guid id);
        void CreateItem(Item item); 
        Task CreateItemAsync(Item item);
        void UpdateItem(Item item);
        Task UpdateItemAsync (Item item);
        void DeleteItem(Guid id);
        Task DeleteItemAsync(Guid id);
    }
}