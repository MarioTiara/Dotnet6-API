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
        Item GetItem(Guid id);

        void CreateItem(Item item); 
        void UpdateItem(Item item);
    }
}