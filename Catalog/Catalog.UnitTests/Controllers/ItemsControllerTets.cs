using System;
using Moq;
using Catalog.Api.Repositories;
using Catalog.Api.Entities;
using Castle.Core.Logging;
using Catalog.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Catalog.Api.Dtos;
using FluentAssertions;

namespace Catalog.UnitTests.Controllers
{
    //UnitOfWork_StateUnderTest_ExpectedBehaviour
    public class ItemsControllerTets
    {
        private readonly Mock<IItemRepository> repositoryStub=new ();
        private readonly Mock<ILogger<ItemsController>> loggerStub= new ();
        private readonly Random rand= new();
        [Fact]
        public async Task GetItem_WithUnexistingItem_ReturnsNotFound(){
            //Arrange
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Item)null);

            var Controller= new ItemsController(repositoryStub.Object, loggerStub.Object);
            //Act
            var result= await Controller.GetItem(Guid.NewGuid());
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task GetItem_WithExistingItem_ReturnsExpectedItem(){
            // Arrange
            Item expectedItem = CreateRandomItem();

            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedItem);

            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetItem(Guid.NewGuid());

            // Assert
            Assert.IsType<ItemDto>(result.Value);
            result.Value.Should().BeEquivalentTo(expectedItem);
        }

        [Fact]
        public async Task GetItems_WithExistingItems_ReturnsAllItems(){
            // Arrange
            var expectedItem = new [] {
                CreateRandomItem(),
                CreateRandomItem(),
                CreateRandomItem(),
                CreateRandomItem()
            };
            repositoryStub.Setup(repo => repo.GetItemsAsync())
                .ReturnsAsync(expectedItem);

            var controller= new ItemsController(repositoryStub.Object, loggerStub.Object);
            // Act
            var actualItems= await controller.GetItems();

            // Assert
            Assert.Equal(expectedItem.Count(), actualItems.Count());
            Assert.IsType<List<ItemDto>>(actualItems);
            actualItems.Should().BeEquivalentTo(
                expectedItem,
                options=>options.ComparingByMembers<Item>()
            );
            
        }



        private Item CreateRandomItem(){
            return new (){
                Id= new Guid(),
                Name=Guid.NewGuid().ToString(),
                Price=rand.Next(1000),
                CreatedDate=DateTimeOffset.UtcNow
            };
        }
    }
}