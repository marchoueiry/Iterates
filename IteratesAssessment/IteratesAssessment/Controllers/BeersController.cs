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
    public class BeersController : ApiController
    {
        private ManagementSystemContext db;
        #region Constructors
        //Injecting the Context which will help us during the mocking in the unit test
        public BeersController(ManagementSystemContext ms)
        {
            db = ms;
        }
        public BeersController()
        {
            db = new ManagementSystemContext();
        }
        #endregion

        // FR1: List all the beers by brewery 
        [HttpGet]
        [Route("api/Beers/GetBeerByBrewery/{id}")]
        public IQueryable<Beer> GetBeerByBrewery(int id)
        {            
            return db.Beers.Where(s => s.BreweryID == id);
        }


        // FR2: A brewer can add new beer
        [ResponseType(typeof(Beer))]
        [HttpPost]
        [Route("api/Beers/AddBeer",Name ="AddBeer")]
        public IHttpActionResult AddBeer(Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(string.IsNullOrEmpty(beer.BreweryID.ToString()))
            {
                return Ok("A beer should be always linked to a brewer");
            }
            Beer tmpBeer = db.Beers.FirstOrDefault<Beer>(x => x.BeerName == beer.BeerName);
            if (tmpBeer != null)
            {
                return Ok("Beer Already Exists. Beer will not be added.");
            }
            else
            {
                db.Beers.Add(beer);
                db.SaveChanges();

                return CreatedAtRoute("AddBeer", new { id = beer.ID }, beer);
            }
        }

        // FR3- A brewer can delete a beer.
        [HttpDelete]
        [Route("api/Beers/DeleteBeer/{id}", Name = "DeleteBeer")]
        [ResponseType(typeof(Beer))]
        public IHttpActionResult DeleteBeer(int id)
        {
            Beer beer = db.Beers.Find(id);
            if (beer == null)
            {
                return Ok("The beer you are requesting to delete does not exist in the system.");
            }

            db.Beers.Remove(beer);
            db.SaveChanges();

            return Ok("Beer Deleted Successfully");
        }


        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BeerExists(int id)
        {
            return db.Beers.Count(e => e.ID == id) > 0;
        }
    }
}