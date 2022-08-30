using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IteratesAssessment.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public int BeerID { get; set; }
        public int WholesalerID { get; set; }
        public int Quantity { get; set; }
    }
}