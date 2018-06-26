using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach
{
    class Bauer : Figur
    {
        //Konstruktor
        public Bauer(bool weiss, int zeile, int spalte, bool firstMove, ref Figur[,] feld)
            : base(weiss, zeile, spalte, firstMove, ref feld)
        { 
            this.ImageFile = "PawnSmall";
        }
        //kannBetreten ist die Methode in jeder Figur Klasse welche angibt wie sich diese Figur zu verhalten hat(wie sie sich bewegen kann).
        public override bool kannBetreten(int zeile, int spalte)
        {
            if ((this.Zeile + 2) == zeile && (this.Spalte) == spalte && Weiss == true && FirstMove == true) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile + 1) == zeile && (this.Spalte) == spalte && Weiss == true) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile - 2) == zeile && (this.Spalte) == spalte && Weiss == false && FirstMove == true) return istNichtsImWeg(zeile, spalte);
            if ((this.Zeile - 1) == zeile && (this.Spalte) == spalte && Weiss == false) return istNichtsImWeg(zeile, spalte);
            
            if ((this.Zeile + 1) == zeile &&
                ((this.Spalte + 1) == spalte || (this.Spalte - 1) == spalte) && 
                Weiss == true && 
                this.feld[spalte, zeile] != null &&
                this.feld[spalte, zeile].Weiss == false) return true;
            if ((this.Zeile - 1) == zeile &&
                ((this.Spalte + 1) == spalte || (this.Spalte - 1) == spalte) && 
                Weiss == false &&
                this.feld[spalte, zeile] != null &&
                this.feld[spalte, zeile].Weiss == true) return true;
            return false;
        }

        protected override bool istNichtsImWeg(int zeile, int spalte)
        {
            if (this.feld[spalte, zeile] != null)
            {
                return false;
            }

            return this.istWegFrei(zeile, spalte);
        }
    }
}
