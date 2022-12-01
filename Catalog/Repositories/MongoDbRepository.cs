using System.Data;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

using Catalog.Entities;
using Microsoft.Extensions.Options;
using Catalog.Config;

namespace Catalog.Repositories
{
    public class MongoDbRepository : IItemRepository
    {
        private const string _collectionName="items";
        private readonly IMongoCollection<Item> _itemsCollection;
        public MongoDbRepository(IOptions<MongoDbConfig> mongodbConfig){
            var mongoClient= new MongoClient(mongodbConfig.Value.Connectionstring);
            var mongoDatabase= mongoClient.GetDatabase(mongodbConfig.Value.DatabaseName);
            _itemsCollection=mongoDatabase.GetCollection<Item>(_collectionName);
        }
        public void CreateItem(Item item)
        {
            _itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            _itemsCollection.DeleteOne(item=>item.Id==id);
        }

        public Item GetItem(Guid id)
        {
            return _itemsCollection.Find(item=>item.Id==id).FirstOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemsCollection.Find(_=>true).ToList();
        }

        public void UpdateItem(Item item)
        {
            _itemsCollection.FindOneAndReplace(x=>x.Id==item.Id, item);
        }
    }
}