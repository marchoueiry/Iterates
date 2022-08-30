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
    class SaleControllerTest
    {
        [Test]
        public void Given_UserTriesToAddNewSale_When_SaleWithSameOrderExists_Then_AddingSaleWillFail()
        {
            //Arrange
            var userContextMock = ArrangeGetSales();

            //Act
            var usersService = new SalesController(userContextMock.Object);
            Sale sale = new Sale() { OrderID = 1, SaleDate = DateTime.Today };
            var result = usersService.AddSale(sale);

            //Assert
            Assert.AreEqual("A Sale is already attached to this order.", ((System.Web.Http.Results.OkNegotiatedContentResult<string>)result).Content);
        }
        [Test]
        public void Given_UserTriesToAddNewSale_When_SaleWithSameOrderDoesntExist_Then_AddingSaleWillSucceed()
        {
            //Arrange
            var userContextMock = ArrangeGetSales();

            //Act
            var usersService = new SalesController(userContextMock.Object);
            Sale sale = new Sale() { OrderID = 3, SaleDate = DateTime.Today };
            var result = usersService.AddSale(sale);

            //Assert
            Assert.AreEqual(sale.OrderID, ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<Sale>)result).Content.OrderID);
        }
        protected Mock<ManagementSystemContext> ArrangeGetSales()
        {
            var data = new List<Sale>
            {
                new Sale {OrderID=1, SaleDate = DateTime.Today},
                new Sale {OrderID=2, SaleDate = DateTime.Today}

            }.AsQueryable();

            var usersMock = new Mock<DbSet<Sale>>();
            usersMock.As<IQueryable<Sale>>().Setup(m => m.Provider).Returns(data.Provider);
            usersMock.As<IQueryable<Sale>>().Setup(m => m.Expression).Returns(data.Expression);
            usersMock.As<IQueryable<Sale>>().Setup(m => m.ElementType).Returns(data.ElementType);
            usersMock.As<IQueryable<Sale>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var userContextMock = new Mock<ManagementSystemContext>();
            userContextMock.Setup(x => x.Sales).Returns(usersMock.Object);

            return userContextMock;
        }
    }
}
