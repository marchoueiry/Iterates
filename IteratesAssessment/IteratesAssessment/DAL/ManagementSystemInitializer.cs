using IteratesAssessment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IteratesAssessment.DAL
{
    public class ManagementSystemInitializer: DropCreateDatabaseIfModelChanges<ManagementSystemContext>
    {
        protected override void Seed(ManagementSystemContext context)
        {
            System.Diagnostics.Debugger.Launch();
            // Load Brewery Sample Data to Database
            var breweries = new List<Brewery>()
            {
                new Brewery {BreweryName="Chimay"},
                new Brewery {BreweryName="Alken Maes"},
                new Brewery {BreweryName="Dubuisson"},
                new Brewery {BreweryName="Huyghe"},
                new Brewery {BreweryName="Bosteels"}
            };
            breweries.ForEach(s => context.Breweries.Add(s));
            context.SaveChanges();
            // Load Beer Sample Data to Database
            var beers = new List<Beer>()
            {
                new Beer {BeerName="Chimay Gold", AlcoholContent="4.8%", BreweryID=1, Price=4},
                new Beer {BeerName="Chimay Blue", AlcoholContent="9%", BreweryID=1, Price=7},
                new Beer {BeerName="Chimay Red", AlcoholContent="7%", BreweryID=1, Price=3},
                new Beer {BeerName="Chimay White", AlcoholContent="8%", BreweryID=1, Price=2},
                new Beer {BeerName="Ciney Blonde", AlcoholContent="7%", BreweryID=2, Price=6},
                new Beer {BeerName="Ciney Brune", AlcoholContent="7%", BreweryID=2, Price=5},
                new Beer {BeerName="Grimbergen", AlcoholContent="6.7%", BreweryID=2, Price=5},
                new Beer {BeerName="Cuvée des Trolls", AlcoholContent="7%", BreweryID=3, Price=4},
                new Beer {BeerName="Delirium de Noel", AlcoholContent="10%", BreweryID=4, Price=6},
                new Beer {BeerName="Delirium Nocturnum", AlcoholContent="8.5%", BreweryID=4, Price=8},
                new Beer {BeerName="Delirium Tremens", AlcoholContent="9%", BreweryID=4, Price=4},
                new Beer {BeerName="Pauwel Kwak", AlcoholContent="8.4%", BreweryID=5, Price=7},
                new Beer {BeerName="Tripel Karmeliet", AlcoholContent="8.4%", BreweryID=5, Price=7}
            };
            beers.ForEach(s => context.Beers.Add(s));
            context.SaveChanges();
            // Load Wholesaler sample data to Database
            var wholesalers = new List<Wholesaler>()
            {
                new Wholesaler {WholesalerName="Belgian Beer Export"},
                new Wholesaler {WholesalerName="Belgian Beer Box"},
                new Wholesaler {WholesalerName="Beer of Belgium"}
                
            };
            wholesalers.ForEach(s => context.Wholesalers.Add(s));
            context.SaveChanges();
            // Load Stock sample data to Database
            var stock = new List<Stock>()
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

            };
            stock.ForEach(s => context.Stocks.Add(s));
            context.SaveChanges();
            // Load Order sample data to Database
            var orders = new List<Order>()
            {
                new Order {WholesalerID = 1,BeerID=1, Quantity=4, OriginalAmout=16, Discount=0,TotalAmount=16 },
                new Order {WholesalerID = 2,BeerID=2, Quantity=9, OriginalAmout=63, Discount=0,TotalAmount=63 },
                new Order {WholesalerID = 3,BeerID=11, Quantity=5, OriginalAmout=20, Discount=0,TotalAmount=20 }


            };
            orders.ForEach(s => context.Orders.Add(s));
            context.SaveChanges();
            // Load Sale sample data to Database
            var sales = new List<Sale>()
            {
                new Sale {OrderID=1, SaleDate = DateTime.Today},
                new Sale {OrderID=2, SaleDate = DateTime.Today}

            };
            sales.ForEach(s => context.Sales.Add(s));
            context.SaveChanges();
        }
    }
}