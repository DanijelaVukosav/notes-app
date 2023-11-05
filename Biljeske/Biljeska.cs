using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biljeske
{
    enum Tip
    {
        Posao=1,
        Zabava,
        Ostalo
    }
    class Biljeska
    {
        public int idBiljeska;
        public String naslov;
        public String sadrzaj;
        public Tip tip;
        public DateTime vrijeme;
        public bool daLiJeIzbrisano;

        public Biljeska(int idBiljeska, string naslov, string sadrzaj, int tip, DateTime vrijeme, bool daLiJeIzbrisano)
        {
            this.idBiljeska = idBiljeska;
            this.naslov = naslov;
            this.sadrzaj = sadrzaj;
            this.tip = (Tip)tip;
            this.vrijeme = vrijeme;
            this.daLiJeIzbrisano = daLiJeIzbrisano;
        }

        public Biljeska(string naslov, string sadrzaj, int tip)
        {
            this.naslov = naslov;
            this.sadrzaj = sadrzaj;
            this.tip = (Tip)tip;
        }
        public Biljeska(string naslov, string sadrzaj, Tip tip)
        {
            this.naslov = naslov;
            this.sadrzaj = sadrzaj;
            this.tip = (Tip)tip;
        }

        public Biljeska()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Biljeska biljeska &&
                   idBiljeska == biljeska.idBiljeska;
        }

    }
}
