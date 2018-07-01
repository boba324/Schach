using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Schach
{
    abstract class Figur
    {
        #region Variablen
        protected String ImageFile;
        public bool Weiss { get; set; }
        public int Zeile { get; set; }
        public int Spalte { get; set; }
        public bool FirstMove { get; set; }
        protected Figur[,] feld;
        #endregion
        #region Konstruktor
        public Figur(bool weiss, int zeile, int spalte, bool firstMove, ref Figur[,] feld)
        {
            this.Weiss = weiss;
            this.Zeile = zeile;
            this.Spalte = spalte;
            this.FirstMove = firstMove;
            this.feld = feld;
        }
        #endregion
        #region Ist das Feld betretbar
        public abstract bool kannBetreten(int zeile, int spalte);
        #endregion
        #region Ist eine Figur im Weg
        protected abstract bool istNichtsImWeg(int zeile, int spalte);
        #endregion
        #region Überprüfung ob weg Frei ist
        protected bool istWegFrei(int zeile, int spalte)
        {
            int addSpalte = 0;
            int addZeile = 0;
            if (this.Zeile > zeile)
            {
                addZeile = -1;
            }
            else if (this.Zeile < zeile)
            {
                addZeile = 1;
            }
            if (this.Spalte > spalte)
            {
                addSpalte = -1;
            }
            else if (this.Spalte < spalte)
            {
                addSpalte = 1;
            }

            int x = this.Spalte;
            int y = this.Zeile;
            while ((x + addSpalte != spalte ||
                y + addZeile != zeile) &&
                x + addSpalte > -1 &&
                y + addZeile > -1 && 
                x + addSpalte < 8 &&
                y + addZeile < 8)
            {
                if (x + addSpalte != spalte)
                {
                    x += addSpalte;
                }
                if (y + addZeile != zeile)
                {
                    y += addZeile;
                }
                if (this.feld[x, y] != null) 
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
        #region Übernehme Bilder der Figuren
        public Bitmap getBild()
        {
            Bitmap fg = (Bitmap)Image.FromFile("..\\..\\Recources\\image\\" + ((Weiss) ? "White" : "Black") + ImageFile + ".png", true);
            return fg;
        }
        #endregion
    }
}
