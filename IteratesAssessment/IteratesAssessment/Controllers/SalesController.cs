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
    public class SalesController : ApiController
    {
        private ManagementSystemContext db;
        #region Constructors
        //Injecting the Context which will help us during the mocking in the unit test
        public SalesController(ManagementSystemContext ms)
        {
            db = ms;
        }
        public SalesController()
        {
            db = new ManagementSystemContext();
        }
        #endregion

        // FR4- Add the sale of an existing beer to an existing wholesaler
        [ResponseType(typeof(Sale))]
        [HttpPost]
        [Route("api/Sales/AddSale", Name = "AddSale")]
        public IHttpActionResult AddSale(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Sale tmpSale = db.Sales.FirstOrDefault<Sale>(x => x.OrderID == sale.OrderID);
            if (tmpSale != null)
            {
                return Ok("A Sale is already attached to this order.");
            }
            else
            {
                db.Sales.Add(sale);
                db.SaveChanges();

                return CreatedAtRoute("AddSale", new { id = sale.ID }, sale);
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


    }
}