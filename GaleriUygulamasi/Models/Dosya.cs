using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaleriUygulamasi.Models
{
    public class Dosya
    {
        public int ID { get; set; }
        public byte[] Deger { get; set; }
        public string DosyaAdi { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string DosyaTipi { get; set; }
        public double DosyaBoyutu { get; set; }
        public string BoyutKisaltma { get; set; }
        public string Ikon { get; set; }
        public Nullable<DateTime> KayitTarihi { get; set; }
    }
}