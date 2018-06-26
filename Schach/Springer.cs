using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Springer : Figur
    {
        //Konstruktor
        public Springer(bool weiss, int zeile, int spalte, bool firstMove, ref Figur[,] feld)
            : base(weiss, zeile, spalte, firstMove, ref feld)
        {
            this.ImageFile = "KnightSmall";
        }
        //kannBetreten ist die Methode in jeder Figur Klasse welche angibt wie sich diese Figur zu verhalten hat(wie sie sich bewegen kann).
        public override bool kannBetreten(int zeile, int spalte)
        {
            if ((this.Zeile - 2) == zeile && (this.Spalte - 1) == spalte) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile - 2) == zeile && (this.Spalte + 1) == spalte) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile - 1) == zeile && (this.Spalte - 2) == spalte) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile + 1) == zeile && (this.Spalte - 2) == spalte) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile - 1) == zeile && (this.Spalte + 2) == spalte) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile + 1) == zeile && (this.Spalte + 2) == spalte) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile + 2) == zeile && (this.Spalte - 1) == spalte) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile + 2) == zeile && (this.Spalte + 1) == spalte) return istNichtsImWeg(zeile, spalte);
            return false;
        }

        protected override bool istNichtsImWeg(int zeile, int spalte)
        {
            if (this.feld[spalte, zeile] != null &&
                this.feld[spalte, zeile].Weiss == this.feld[this.Spalte, this.Zeile].Weiss)
            {
                return false;
            }

            return true;
        }
    }
}
