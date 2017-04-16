using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notBilgiSistemi
{
    public class DersNotu
    {
        private int projeNotu;
        private int vizeNotu;
        private int finalNotu;
        private int bütünlemeNotu;
        private Ders ders;

        //CONSTRUCTERS
        public DersNotu(Ders ders)
        {
            this.ders = ders;
            projeNotu = -1;
            vizeNotu = -1;
            finalNotu = -1;
            bütünlemeNotu = -1;
        }

        // GET
        public int getProjeNotu() { return projeNotu; }
        public int getVizeNotu() { return vizeNotu; }
        public int getFinalNotu() { return finalNotu; }
        public int getBütünlemeNotu() { return bütünlemeNotu; }
        public Ders getDers() { return ders; }

        // SET
        public void setProjeNotu (int not) { projeNotu = not; }
        public void setVizeNotu(int not) { vizeNotu = not; }
        public void setFinalNotu(int not) { finalNotu = not; }
        public void setBütünlemeNotu(int not) { bütünlemeNotu = not; }
        public void setDers (Ders ders) { this.ders = ders; }
    }
}
