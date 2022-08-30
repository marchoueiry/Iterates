using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IteratesAssessment.DAL;
using IteratesAssessment.Models;

namespace IteratesAssessment.Controllers
{
    public class OrdersController : ApiController
    {
        private ManagementSystemContext db;
        #region Constructors
        //Injecting the Context which will help us during the mocking in the unit test
        public OrdersController(ManagementSystemContext ms)
        {
            db = ms;
        }
        public OrdersController()
        {
            db = new ManagementSystemContext();
        }
        #endregion



        // FR6- A client can request a quote from a wholesaler
        [ResponseType(typeof(Order))]
        [HttpPost]
        [Route("api/Orders/RequestOrder", Name = "RequestOrder")]
        public IHttpActionResult RequestOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(string.IsNullOrEmpty(order.WholesalerID.ToString())|| string.IsNullOrEmpty(order.BeerID.ToString())|| string.IsNullOrEmpty(order.Quantity.ToString()))
            {
                return Ok("Your order is missing information and cannot be processed.");
            }
            Order tmpOrder = Order.GenerateOrder(order,db);
            if (tmpOrder != null)
            {
                db.Orders.Add(tmpOrder);
                Stock tmpStock = db.Stocks.FirstOrDefault<Stock>(x => x.BeerID == tmpOrder.BeerID && x.WholesalerID == tmpOrder.WholesalerID);
                tmpStock.Quantity = tmpStock.Quantity - tmpOrder.Quantity;
                db.SaveChanges();
                return CreatedAtRoute("RequestOrder", new { id = tmpOrder.ID }, tmpOrder);
            }
            else
            {
                return Ok("Please validate your order details and try again.");
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.ID == id) > 0;
        }
    }
}