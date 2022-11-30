using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemRepository
    {
        public IEnumerable<Item> GetItems();
        public Item GetItem(Guid id);
    }
}