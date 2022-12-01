using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemsRepository:IItemRepository
    {
        private readonly List<Item> items= new(){
            new Item{Id=Guid.NewGuid(), Name="Potion", Price=9, CreatedDate=DateTimeOffset.UtcNow},
            new Item{Id=Guid.NewGuid(), Name="Iron Sword", Price=20, CreatedDate=DateTimeOffset.UtcNow},
            new Item{Id=Guid.NewGuid(), Name="Bronze Shield", Price=18, CreatedDate=DateTimeOffset.UtcNow}
        };   

        public IEnumerable<Item> GetItems(){
            return items;
        }
        public Item GetItem(Guid id){
            if (items is not null){
                var item=items.Where(item=>item.Id==id).SingleOrDefault();
                if (item is not null){
                    return item;
                }else{
                    return null;
                }
            }else{
                return null;
            }
        }

        public void CreateItem(Item item)
        {

            items.Add(item);
            
        }

        public void UpdateItem(Item item)
        {
            var index=items.FindIndex(existingItem=>existingItem.Id==item.Id);
            items[index]=item;
        }

        public void DeleteItem(Guid id)
        {
            var index=items.FindIndex(existingItem=>existingItem.Id==id);
            items.RemoveAt(index);
        }
    }
}