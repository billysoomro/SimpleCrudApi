using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleCrudApi.Controllers;
using SimpleCrudApi.Models;

namespace SimpleCrudApi.UnitTests.Controllers
{
    [TestFixture]
    internal class GuitarsControllerTests
    {
        private Mock<IDynamoDBContext> _mockDynamoDbContext;
        private GuitarsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockDynamoDbContext = new Mock<IDynamoDBContext>();
            _controller = new GuitarsController(_mockDynamoDbContext.Object);
        }

        [Test]
        public async Task Get_ShouldReturnOkWithGuitars_WhenGuitarsExist()
        {
            // Arrange
            var guitars = new List<Guitar> { new Guitar { Id = 1, Model = "Stratocaster" }};
            var mockAsyncSearch = new Mock<AsyncSearch<Guitar>>();

            mockAsyncSearch.Setup(x => x.GetRemainingAsync(default)).Returns(Task.FromResult(guitars));

            _mockDynamoDbContext.Setup(x => x.ScanAsync<Guitar>(It.IsAny<List<ScanCondition>>(), null))
            .Returns(mockAsyncSearch.Object);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(guitars, okResult.Value);
        }

        [Test]
        public async Task Get_ShouldReturnNotFound_WhenNoGuitarsExist()
        {
            // Arrange
            var mockAsyncSearch = new Mock<AsyncSearch<Guitar>>();                     

            _mockDynamoDbContext.Setup(x => x.ScanAsync<Guitar>(It.IsAny<List<ScanCondition>>(), null))
            .Returns(mockAsyncSearch.Object);

            // Act
            var result = await _controller.Get();

            // Assert
            var notFoundResult = result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("No Guitars found", notFoundResult.Value);
        }        
    }  
}
