using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IteratesAssessment.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime SaleDate { get; set; }
        public int OrderID { get; set; }
    }
}