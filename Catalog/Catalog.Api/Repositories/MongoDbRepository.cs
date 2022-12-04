using System.Data;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

using Catalog.Api.Entities;
using Microsoft.Extensions.Options;
using Catalog.Api.Config;

namespace Catalog.Api.Repositories
{
    public class MongoDbRepository : IItemRepository
    {
        private const string _collectionName="items";
        private readonly IMongoCollection<Item> _itemsCollection;
        public MongoDbRepository(IOptions<MongoDbConfig> mongodbConfig){
            var mongoClient= new MongoClient(mongodbConfig.Value.ConnectionString);
            var mongoDatabase= mongoClient.GetDatabase(mongodbConfig.Value.DatabaseName);
            _itemsCollection=mongoDatabase.GetCollection<Item>(_collectionName);
        }
        public void CreateItem(Item item)
        {
            _itemsCollection.InsertOne(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);
        }

        public void DeleteItem(Guid id)
        {
            _itemsCollection.DeleteOne(item=>item.Id==id);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await _itemsCollection.DeleteOneAsync(item=>item.Id==id);
        }

        public Item GetItem(Guid id)
        {
            return _itemsCollection.Find(item=>item.Id==id).FirstOrDefault();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return (await _itemsCollection.
                    FindAsync(item=>item.Id==id)).
                    FirstOrDefault();;
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemsCollection.Find(_=>true).ToList();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return (await _itemsCollection.FindAsync(_=>true)).ToList();
        }

        public void UpdateItem(Item item)
        {
            _itemsCollection.FindOneAndReplace(x=>x.Id==item.Id, item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            await _itemsCollection.
                    FindOneAndReplaceAsync(x=>x.Id==item.Id, 
                                           item);
        }
    }
}