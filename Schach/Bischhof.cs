using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Bischhof : Figur
    {
        //Konstruktor
        public Bischhof(bool weiss, int zeile, int spalte, bool firstMove, ref Figur[,] feld)
            : base(weiss, zeile, spalte, firstMove, ref feld)
        {
            this.ImageFile = "BishopSmall";
        }
        //kannBetreten ist die Methode in jeder Figur Klasse welche angibt wie sich diese Figur zu verhalten hat(wie sie sich bewegen kann).
        public override bool kannBetreten(int zeile, int spalte)
        {
            for (int i = 1; i < 8; i++)
            {
                if ((this.Zeile + i) == zeile && (this.Spalte + i) == spalte) return istNichtsImWeg(zeile, spalte);
            }
            for (int i = 8; i > 0; i--)
            {
                if ((this.Zeile - i) == zeile && (this.Spalte - i) == spalte) return istNichtsImWeg(zeile, spalte);
            }
            for (int i = 8; i > 0; i--)
            {
                if ((this.Zeile - i) == zeile && (this.Spalte + i) == spalte) return istNichtsImWeg(zeile, spalte);
            }
            for (int i = 8; i > 0; i--)
            {
                if ((this.Zeile + i) == zeile && (this.Spalte - i) == spalte) return istNichtsImWeg(zeile, spalte);
            }
            return false;
        }

        protected override bool istNichtsImWeg(int zeile, int spalte)
        {
            if (this.feld[spalte, zeile] != null &&
                this.feld[spalte, zeile].Weiss == this.feld[this.Spalte, this.Zeile].Weiss)
            {
                return false;
            }

            return this.istWegFrei(zeile, spalte);
        }
    }
}
