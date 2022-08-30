using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IteratesAssessment.Models
{
    public class Beer
    {
        public int ID { get; set; }
        public int BreweryID { get; set; }
        public string AlcoholContent { get; set; }
        public string BeerName { get; set; }
        public int Price { get; set; }
    }
}