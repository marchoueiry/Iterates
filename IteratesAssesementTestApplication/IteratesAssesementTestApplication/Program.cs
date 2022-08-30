using IteratesAssessment.Models;
using Json.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IteratesAssesementTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //FR1- List all the beers by brewery.
            //GetAllBeersByBrewery(2);

            //FR2- A brewer can add new beer.
            //Beer beer = new Beer() {BeerName = "Test1", BreweryID = 1, AlcoholContent = "11%", Price = 6 };
            //  AddBeer(beer);

            //FR3- A brewer can delete a beer.
            //   DeleteBeer(1008);

            //FR4- Add the sale of an existing beer to an existing wholesaler
            //Sale sale = new Sale() { OrderID = 3, SaleDate = DateTime.Today };
            //AddSale(sale);

            //FR5- A wholesaler can update the remaining quantity of a beer in his stock.
             Stock stock = new Stock() { WholesalerID = 1, BeerID = 1, Quantity = 66 };
             UpdateStock(stock);

            //FR6- A client can request a quote from a wholesaler
            //Order order = new Order { WholesalerID = 1, BeerID = 4, Quantity = 22};
            //RequestOrder(order);
        }

        public static void GetAllBeersByBrewery(int breweryId) //Get All Events Records  
        {
            using (var client = new WebClient()) //WebClient  
            {
                client.Headers.Add("Content-Type:application/json"); //Content-Type  
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://localhost:50795/api/Beers/GetBeerByBrewery/"+breweryId); //URI  
                Console.WriteLine(Environment.NewLine + result);
            }
            Console.ReadLine();
        }
        public static void AddBeer(Beer beer)
        {
            using (var client = new WebClient())
            {

                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                string uri = "http://localhost:50795/api/Beers/AddBeer";
                var result = client.UploadString(uri, JsonNet.Serialize(beer));

                Console.WriteLine(result);
                Console.ReadLine();
            }
        }
        public static string DeleteBeer(int beerId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                string uri = "http://localhost:50795/api/Beers/DeleteBeer/" + beerId;
                var result = client.UploadString(uri, "DELETE", "");
                Console.WriteLine(result);
                Console.ReadLine();
                return result;
            }
        }
        public static void AddSale (Sale sale)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                string uri = "http://localhost:50795/api/Sales/AddSale";
                var result = client.UploadString(uri, JsonNet.Serialize(sale));

                Console.WriteLine(result);
                Console.ReadLine();
            }
        }
        public static void UpdateStock(Stock stock)
        {
            using (var client = new WebClient())
            {

                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                string uri = "http://localhost:50795/api/Stocks/UpdateQuantityInStock";
                var result = client.UploadString(uri, JsonNet.Serialize(stock));

                Console.WriteLine(result);
                Console.ReadLine();
            }
        }
        public static void RequestOrder(Order order)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                string uri = "http://localhost:50795/api/Orders/RequestOrder";
                var result = client.UploadString(uri, JsonNet.Serialize(order));

                Console.WriteLine(result);
                Console.ReadLine();
            }
        }




    }
}

