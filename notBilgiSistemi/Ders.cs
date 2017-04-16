using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace notBilgiSistemi
{
    public class Ders
    {
        private string ad;
        private int projeNotuYüzdesi;
        private int vizeNotuYüzdesi;
        private int finalNotuYüzdesi;
        private decimal geçmeSınırı;
        private List<Ogrenci> ogrenciler;

        //CONSTRUCTERS
        public Ders (string ad)
        {
            this.ad = ad;
            ogrenciler = new List<Ogrenci>();
            projeNotuYüzdesi = 0;
            vizeNotuYüzdesi = 40;
            finalNotuYüzdesi = 60;
            geçmeSınırı = -1;
        }

        //GET
        public string getAd(){ return ad; }
        public int getProjeNotuYüzdesi() { return projeNotuYüzdesi; }
        public int getVizeNotuYüzdesi() { return vizeNotuYüzdesi; }
        public int getFinalNotuYüzdesi() { return finalNotuYüzdesi; }
        public decimal getGeçmeSınırı() { return geçmeSınırı; }
        public List<Ogrenci> getOgrenciList() { return ogrenciler; }

        //SET
        public void setAd (string ad) { this.ad = ad; }
        public void setProjeNotuYüzdesi(int yüzde) { projeNotuYüzdesi = yüzde; }
        public void setVizeNotuYüzdesi(int yüzde) { vizeNotuYüzdesi = yüzde; }
        public void setFinalNotuYüzdesi(int yüzde) { finalNotuYüzdesi = yüzde; }
        public void setGeçmeSınırı(decimal sınır) { geçmeSınırı = sınır; }
        public void setOgrenciList(List<Ogrenci> ogrList) { ogrenciler = ogrList; }

        //ADD \ DELETE
        public void addOgrenci(Ogrenci ogr )
        {
            ogrenciler.Add(ogr);
        }
        public Boolean deleteOgrenci(Ogrenci ogr)
        {
            foreach (Ogrenci o in ogrenciler)
            {
                if (ogr.getNo() == o.getNo())
                {
                    ogrenciler.Remove(o);
                    return true;
                }
            }
            return false;
        }
        public Boolean deleteOgrenci(int no)
        {
            foreach (Ogrenci o in ogrenciler)
            {
                if (o.getNo() == no)
                {
                    ogrenciler.Remove(o);
                    return true;
                }
            }
            return false;
        }
    }
}
