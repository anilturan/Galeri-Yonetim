using GaleriUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GaleriUygulamasi.Models
{
    public class GaleriYonetimContext : DbContext
    {
        public GaleriYonetimContext()
            : base("name=MyConnection")
        {

        }

        public DbSet<Dosya> Dosya { get; set; }
    }
}