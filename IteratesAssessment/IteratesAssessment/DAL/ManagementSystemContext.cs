using IteratesAssessment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IteratesAssessment.DAL
{
    public class ManagementSystemContext: DbContext
    {
        public ManagementSystemContext() : base("ManagementSystemContext") { }

        public virtual DbSet<Beer> Beers { get; set; }
        public virtual DbSet<Brewery> Breweries { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Wholesaler> Wholesalers { get; set; }

    }
}