using IteratesAssessment.Controllers;
using IteratesAssessment.DAL;
using IteratesAssessment.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratesAssessment.Tests.Controllers
{
    [TestFixture]
    class StockControllerTest
    {
        [Test]
        public void Given_UserTriesToUpdateQuantityInStock_When_StockDoesNotExist_Then_UpdateStockWillFail()
        {
            //Arrange
            var userContextMock = ArrangeGetStocks();

            //Act
            var usersService = new StocksController(userContextMock.Object);
            Stock stock = new Stock() { WholesalerID = 15, BeerID = 1, Quantity = 22 };
            var result = usersService.UpdateQuantityInStock(stock);

            //Assert
            Assert.AreEqual(String.Format("There is no stock related to the beer {0} and the Wholesaler {1}",stock.BeerID,stock.WholesalerID), ((System.Web.Http.Results.OkNegotiatedContentResult<string>)result).Content);
        }

        [Test]
        public void Given_UserTriesToUpdateQuantityInStock_When_StockExist_Then_UpdateStockWillSucceed()
        {
            //Arrange
            var userContextMock = ArrangeGetStocks();

            //Act
            var usersService = new StocksController(userContextMock.Object);
            Stock stock = new Stock() { WholesalerID = 1, BeerID = 1, Quantity = 25 };
            var result = usersService.UpdateQuantityInStock(stock);

            //Assert
            Assert.AreEqual("The Stock was updated successfully", ((System.Web.Http.Results.OkNegotiatedContentResult<string>)result).Content);
        }
        protected Mock<ManagementSystemContext> ArrangeGetStocks()
        {
            var data = new List<Stock>
            {
                new Stock {WholesalerID=1, BeerID=1, Quantity =20},
                new Stock {WholesalerID=1, BeerID=4, Quantity =30},
                new Stock {WholesalerID=1, BeerID=7, Quantity =60},
                new Stock {WholesalerID=1, BeerID=9, Quantity =70},
                new Stock {WholesalerID=2, BeerID=2, Quantity =50},
                new Stock {WholesalerID=2, BeerID=3, Quantity =40},
                new Stock {WholesalerID=2, BeerID=6, Quantity =10},
                new Stock {WholesalerID=2, BeerID=8, Quantity =60},
                new Stock {WholesalerID=3, BeerID=10, Quantity =30},
                new Stock {WholesalerID=3, BeerID=11, Quantity =80},
                new Stock {WholesalerID=3, BeerID=12, Quantity =40},
                new Stock {WholesalerID=3, BeerID=13, Quantity =70}

            }.AsQueryable();

            var usersMock = new Mock<DbSet<Stock>>();
            usersMock.As<IQueryable<Stock>>().Setup(m => m.Provider).Returns(data.Provider);
            usersMock.As<IQueryable<Stock>>().Setup(m => m.Expression).Returns(data.Expression);
            usersMock.As<IQueryable<Stock>>().Setup(m => m.ElementType).Returns(data.ElementType);
            usersMock.As<IQueryable<Stock>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var userContextMock = new Mock<ManagementSystemContext>();
            userContextMock.Setup(x => x.Stocks).Returns(usersMock.Object);

            return userContextMock;
        }
    }
}
