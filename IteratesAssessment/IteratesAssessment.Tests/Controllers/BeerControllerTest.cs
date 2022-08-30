using IteratesAssessment.Controllers;
using IteratesAssessment.DAL;
using IteratesAssessment.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace IteratesAssessment.Tests.Controllers
{
    [TestFixture]
    class BeerControllerTest
    {
        [Test]
        public void Given_UserTriesToRetrieveBeersByBrewery_When_BreweryExists_Then_ReturnAllBeersInBrewery()
        {
            //Arrange
            var userContextMock = ArrangeGetBeers();

            //Act
            var usersService = new BeersController(userContextMock.Object);
            var result = usersService.GetBeerByBrewery(1);

            //Assert
            Assert.AreEqual(2, result.Count<Beer>());
            Assert.AreEqual("Beer1", result.ElementAt<Beer>(0).BeerName);
        }

        [Test]
        public void Given_UserTriesToRetrieveBeersByBrewery_When_BreweryDoesNotExist_Then_NoBeersReturned()
        {
            //Arrange
            var userContextMock = ArrangeGetBeers();

            //Act
            var usersService = new BeersController(userContextMock.Object);
            // There is no brewery with ID 5
            var result = usersService.GetBeerByBrewery(5);

            //Assert
            Assert.AreEqual(0, result.Count<Beer>());
           
        }

        [Test]
        public void Given_UserTriesToAddNewBeer_When_BeerAlreadyExists_Then_BeerNotAdded()
        {
            //Arrange
            var userContextMock = ArrangeGetBeers();

            //Act
            var usersService = new BeersController(userContextMock.Object);
            // There is no brewery with ID 5
            Beer beer = new Beer() { BeerName = "Beer1", AlcoholContent = "5%", BreweryID = 1, Price = 4 };
            var result = usersService.AddBeer(beer);
            

            //Assert
            Assert.AreEqual("Beer Already Exists. Beer will not be added.", ((System.Web.Http.Results.OkNegotiatedContentResult<string>)result).Content);
        }

        [Test]
        public void Given_UserTriesToDeleteBeer_When_BeerDoesNotExist_Then_BeerNotDeleted()
        {
            //Arrange
            var userContextMock = ArrangeGetBeers();

            //Act
            var usersService = new BeersController(userContextMock.Object);
            // There is no beer with ID 100
            var result = usersService.DeleteBeer(100);

            //Assert
            Assert.AreEqual("The beer you are requesting to delete does not exist in the system.", ((System.Web.Http.Results.OkNegotiatedContentResult<string>)result).Content);
        }
        protected Mock<ManagementSystemContext> ArrangeGetBeers()
        {
            var data = new List<Beer>
            {
                new Beer { BeerName="Beer1", AlcoholContent="5%", BreweryID=1, Price=4},
                new Beer { BeerName="Beer2", AlcoholContent="15%", BreweryID=1, Price=7},
                new Beer {  BeerName = "Beer3", AlcoholContent="3%", BreweryID=2, Price=3},
                new Beer {  BeerName = "Beer4", AlcoholContent="8%", BreweryID=3, Price=2}
            }.AsQueryable();

        var usersMock = new Mock<DbSet<Beer>>();
            usersMock.As<IQueryable<Beer>>().Setup(m => m.Provider).Returns(data.Provider);
            usersMock.As<IQueryable<Beer>>().Setup(m => m.Expression).Returns(data.Expression);
            usersMock.As<IQueryable<Beer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            usersMock.As<IQueryable<Beer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var userContextMock = new Mock<ManagementSystemContext>();
            userContextMock.Setup(x => x.Beers).Returns(usersMock.Object);

            return userContextMock;
        }
    }
}
