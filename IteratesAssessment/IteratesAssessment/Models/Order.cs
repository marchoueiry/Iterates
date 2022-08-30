using IteratesAssessment.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IteratesAssessment.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int WholesalerID { get; set; }
        public int BeerID { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int OriginalAmout { get; set; }
        public double TotalAmount { get; set; }

        public static Order GenerateOrder(Order order,ManagementSystemContext db)
        {
            //Check if wholesaler exists
            Wholesaler tmpWholesaler = db.Wholesalers.Find(order.WholesalerID);
            if (tmpWholesaler == null)
            {
                return null;
            }
            //Check if same order already exist in Database
            Order tmpOrder = db.Orders.FirstOrDefault<Order>(x => x.WholesalerID == order.WholesalerID && x.BeerID == order.BeerID);
            if(tmpOrder != null)
            {
                return null;
            }
            //check if there is a stock for the wholesaler with the needed beer 
            Stock tmpStock = db.Stocks.FirstOrDefault<Stock>(x => x.BeerID == order.BeerID && x.WholesalerID == order.WholesalerID);
            if (tmpStock == null)
            {
                return null;
            }
            else
            {
                if(order.Quantity > tmpStock.Quantity)
                {
                    return null;
                }
                else
                {
                    //Calculate total amount based on the quantity in the order
                    Beer tempBeer = db.Beers.Find(order.BeerID);
                    order.OriginalAmout = order.Quantity * tempBeer.Price;
                    if (order.Quantity > 10 && order.Quantity < 20)
                    {
                        order.Discount = 10;
                        order.TotalAmount = order.OriginalAmout - (order.OriginalAmout * 0.1);

                    }
                    else if (order.Quantity > 20)
                    {
                        order.Discount = 20;
                        order.TotalAmount = order.OriginalAmout - (order.OriginalAmout * 0.2);
                        return order;
                    }
                    else
                    {
                        order.TotalAmount = order.OriginalAmout;
                        return order;
                    }
                    return order;
                }

            }
        }
    }
}