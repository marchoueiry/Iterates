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
    public class StocksController : ApiController
    {
        private ManagementSystemContext db;
        #region Constructors
        //Injecting the Context which will help us during the mocking in the unit test
        public StocksController(ManagementSystemContext ms)
        {
            db = ms;
        }
        public StocksController()
        {
            db = new ManagementSystemContext();
        }
        #endregion



        // FR5- A wholesaler can update the remaining quantity of a beer in his stock.
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/Stocks/UpdateQuantityInStock")]
        public IHttpActionResult UpdateQuantityInStock(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            if (string.IsNullOrEmpty(stock.WholesalerID.ToString())|| string.IsNullOrEmpty(stock.BeerID.ToString()))
            {
                return Ok("Missing Input");
            }
            Stock tmpStock = db.Stocks.FirstOrDefault<Stock>(x => x.BeerID == stock.BeerID && x.WholesalerID == stock.WholesalerID);
            if (tmpStock ==null)
            {
                return Ok((String.Format("There is no stock related to the beer {0} and the Wholesaler {1}", stock.BeerID, stock.WholesalerID)));
            }
            try
            {               
                tmpStock.Quantity = stock.Quantity;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(tmpStock.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Dispose(true);
            return Ok("The Stock was updated successfully");
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockExists(int id)
        {
            return db.Stocks.Count(e => e.ID == id) > 0;
        }
    }
}