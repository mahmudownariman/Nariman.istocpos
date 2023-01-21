using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace istoCPos
{
    internal class islemler
    {
        public static double DoubleYap(String deger)
        {
            double sonuc;
            double.TryParse(deger, NumberStyles.Currency, CultureInfo.CurrentUICulture.NumberFormat, out sonuc);
            return sonuc;

        }

        public static void StokAzalt(string barkod, int miktar) {
            using (var db = new istocDBEntities())
            {
                var urunbilgi = db.Urunler.SingleOrDefault(x => x.UrunBarkod == barkod);
                urunbilgi.StokMiktari -= miktar;
                db.SaveChanges();


            }
        
        }

        public static void StokArttir(string barkod, int miktar)
        {
            using (var db = new istocDBEntities())
            {
                var urunbilgi = db.Urunler.SingleOrDefault(x => x.UrunBarkod == barkod);
                urunbilgi.StokMiktari += miktar;
                db.SaveChanges();


            }

        }
    }
}
