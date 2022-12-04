using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController:ControllerBase
    {
        private readonly IItemRepository _repository;

        public ItemsController(IItemRepository repository){
            _repository=repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems(){
            var items= await _repository.GetItemsAsync();
            return items.Select(item=>item.AsDto());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id){
            var item= await _repository.GetItemAsync(id);
            if (item is null){
                return NotFound();
            }
            return Ok(item.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CretaeItem(CreateItmDto itemDto){
            Item item= new (){
                Id=  Guid.NewGuid(),
                Name= itemDto.Name,
                Price=itemDto.Price,
                CreatedDate= DateTimeOffset.UtcNow
            };
            await _repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new{id=item.Id}, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdatedItemDto itemDto){
            var existingItem=  await _repository.GetItemAsync(id);
            if (existingItem is null){
                return NotFound();
            }
            Item UpdatedItem= existingItem with{
                Name =itemDto.Name,
                Price= itemDto.Price
            };
            await _repository.UpdateItemAsync(UpdatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletItem(Guid id){
            var existingItem= await _repository.GetItemAsync(id);
            if (existingItem is null){
                return NotFound();
            }

            await _repository.DeleteItemAsync(id);
            return NoContent();
        }

    }
}