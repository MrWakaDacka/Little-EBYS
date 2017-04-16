using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notBilgiSistemi
{
    public class Ogrenci
    {
        private int no;
        private string ad;
        private List<DersNotu> notlar;

        //CONSTRUCTERS
        public Ogrenci (int no,string ad)
        {
            this.no = no;
            this.ad = ad;
            notlar = new List<DersNotu>();
        }

        //GET
        public int getNo() { return no; }
        public string getAd () { return ad; }
        public List<DersNotu> getNotlar () { return notlar; }

        //SET
        public void setNo(int no) { this.no = no; }
        public void setAd(string ad) { this.ad = ad; }
        public void setNotListesi (List<DersNotu> notlar) { this.notlar = notlar; }
        
        //ADD \ DELETE
        public void addNote(DersNotu not) { notlar.Add(not); }
        public Boolean removeNote(DersNotu not)
        {
            foreach(DersNotu d in notlar)
            {
                if(not.getDers().Equals(d.getDers()))
                {
                    notlar.Remove(d);
                    return true;
                }
            }
            return false;
        }
    }
}
