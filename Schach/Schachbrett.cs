using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;

namespace Schach
{
    public delegate void eventHandler(PaintEventArgs e);
    class Schachbrett
    {
        //Benötigten wariablen
        private Bitmap bg;
        private Graphics gr;
        private int b = 8;
        private int h = 8;
        private int punkte = 0;
        public eventHandler BitmapGeaendert;
        private int beschriftung = 50;
        private Color hintergrundfarbe;
        private int auswahlZeile = -1;
        private int auswahlSpalte = -1;
        public Figur[,] Feld;
        public bool WeissAmZug = true;
        private bool isDebug = false;
        private bool pro = false;

        //Hintergrundfarbe
        public Color HintergrundFarbe
        {
            get { return hintergrundfarbe; }
            set
            {
                hintergrundfarbe = value;
                aktualisiereBitmap();
            }
        }
        public int gibBreite()
        {
            return b;
        }
        public int gibHoehe()
        {
            return h;
        }

        public Schachbrett(int spalten, int zeilen)
        {
            b = spalten;
            h = zeilen;
            hintergrundfarbe = Color.White;
            Feld = new Figur[spalten, zeilen];
            bg = new Bitmap(beschriftung + b * 42 + 4, beschriftung + h * 42 + 4);
            gr = Graphics.FromImage(bg);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            aktualisiereBitmap();
        }

        public void FigurHinzufuegen(Figur e)
        {
            try
            {
                if (e != null)
                {
                    //e.sch = this;
                }
                Feld[e.Spalte, e.Zeile] = e;
                aktualisiereBitmap();
            }
            catch (Exception)
            {

                return;
            }
        }

        public Figur FigurEntfernen(int sp, int zeile)
        {

            try
            {
                Figur tmp = Feld[sp - 1, zeile - 1];
                Feld[sp - 1, zeile - 1] = null;
                aktualisiereBitmap();
                return tmp;
            }
            catch
            {
                return null;
            }
        }

        public void Waehle(int zeile, int spalte)
        {
            Console.WriteLine(zeile + " " + spalte);
            if (this.auswahlSpalte > -1 &&
                this.auswahlZeile > -1 &&
                Feld[this.auswahlSpalte, this.auswahlZeile] != null && 
                (Feld[this.auswahlSpalte, this.auswahlZeile].Weiss == this.WeissAmZug || this.isDebug) && 
                Feld[this.auswahlSpalte, this.auswahlZeile].kannBetreten(zeile, spalte))
            {
                Feld[this.auswahlSpalte, this.auswahlZeile].FirstMove = false;
                Feld[this.auswahlSpalte, this.auswahlZeile].Spalte = spalte;
                Feld[this.auswahlSpalte, this.auswahlZeile].Zeile = zeile;

                Figur oldFigur = Feld[spalte, zeile];
                Feld[spalte, zeile] = Feld[this.auswahlSpalte, this.auswahlZeile];
                Feld[this.auswahlSpalte, this.auswahlZeile] = null;

                this.auswahlSpalte = -1;
                this.auswahlZeile = -1;

                this.WeissAmZug = !this.WeissAmZug;
                this.werdeZurDame();
                
            }
            else if (this.auswahlSpalte == spalte && this.auswahlZeile == zeile)
            {
                this.auswahlSpalte = -1;
                this.auswahlZeile = -1;
            }
            else if ((Feld[spalte, zeile] != null &&
                Feld[spalte, zeile].Weiss == this.WeissAmZug) || this.isDebug)
            {
                this.auswahlSpalte = spalte;
                this.auswahlZeile = zeile;
            }

            aktualisiereBitmap();
        }

        public void werdeZurDame()
        {
            for (int i = 0; i < b; i++)
            {
                try 
                {
                    if (Feld[i, 7] != null && Feld[i, 7].ToString() == "Schach.Bauer") { 
                        FigurHinzufuegen(new Dame(true, 7, i, true, ref Feld));
                    }
                }
                catch(Exception)
                {
                    return;
                }
            }
            for (int i = 0; i < b; i++)
            {
                try
                {
                    if (Feld[i, 0] != null && Feld[i, 0].ToString() == "Schach.Bauer")
                    {
                        FigurHinzufuegen(new Dame(false, 0, i, true, ref Feld));
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
        
        public void figurGeschlagen(string figur)
        {
            switch (figur)
            {
                case "Schach.Bauer": punkte += 1; break;
                case "Schach.Turm": punkte += 5; break;
                case "Schach.Koenig": punkte += 15; this.ende(punkte); break;
                case "Schach.Dame": punkte += 9; break;
                case "Schach.Springer": punkte += 3; break;
                case "Schach.Bischof": punkte += 3; break;
                default: break;
            }
        }

        public void ende(int p)
        {
            Console.WriteLine("ende");
            StreamWriter sw = new StreamWriter("..\\..\\Ergebnis\\Ergebnis.txt");
            sw.WriteLine("Gewinner Punkte: " + p);
            MessageBox.Show("X hat gewonnen");
        }

        public void aktualisiereBitmap()
        {
            gr.Clear(hintergrundfarbe);
            ///Vertikale Linien
            for (int i = 0; i <= b; i++)
            {
                gr.DrawLine(new Pen(new SolidBrush(Color.Black)), beschriftung + 2 + i * 42, beschriftung + 2, beschriftung + 2 + i * 42, beschriftung + 2 + h * 42);
            }
            ///Horizontale Linien
            for (int i = 0; i <= h; i++)
            {
                gr.DrawLine(new Pen(new SolidBrush(Color.Black)), beschriftung + 2, beschriftung + 2 + i * 42, beschriftung + 2 + b * 42, beschriftung + 2 + i * 42);
            }
            ///Buchstaben
            for (int i = 0; i < b; i++)
            {
                gr.DrawString((Convert.ToChar(i + 65)).ToString(), new Font("Calibri", 20, FontStyle.Regular), new SolidBrush(Color.Black), i * 42 + beschriftung + 15, beschriftung / 4);
            }
            ///Zahlen
            for (int j = 0; j < h; j++)
            {
                gr.DrawString((j + 1).ToString(), new Font("Calibri", 20), new SolidBrush(Color.Black), 15, beschriftung + j * 42 + 42 / 4);

            }

            int counter = 1;
            int zeileTmp = beschriftung + 3;
            int spalteTmp = beschriftung + 3;
            for (int i = 0; i < b; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (counter % 2 != 0)
                    {
                        gr.FillRectangle(new SolidBrush(Color.SkyBlue), zeileTmp, spalteTmp, beschriftung - 10, beschriftung - 10);
                        counter++;
                        zeileTmp = zeileTmp + beschriftung - 8 ;
                    }
                    else
                    {
                        gr.FillRectangle(new SolidBrush(Color.White), zeileTmp, spalteTmp, beschriftung - 10, beschriftung - 10);
                        counter++;
                        zeileTmp = zeileTmp + beschriftung - 8 ;
                    }
                }
                counter++;
                zeileTmp = beschriftung + 3;
                spalteTmp = spalteTmp + beschriftung - 8;
            }

            if (pro)
            {
            if (auswahlSpalte >= 0 && auswahlZeile >= 0 && Feld[auswahlSpalte, auswahlZeile] != null)
            {
                for (int i = 0; i < b; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (Feld[auswahlSpalte, auswahlZeile].kannBetreten(j, i))
                        {
                            gr.FillRectangle(new SolidBrush(Color.Yellow), beschriftung + 3 + i * 42, beschriftung + 3 + j * 42, beschriftung - 10, beschriftung - 10);
                        }
                    }
                }
            }
            }

            //Bilder einfuegen
            for (int i = 0; i < b; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (Feld[i, j] != null)
                    {
                        Bitmap bild = Feld[i, j].getBild();
                        bild.MakeTransparent();
                        gr.DrawImage(bild, beschriftung + 3 + i * 42, beschriftung + 3 + j * 42, 42, 42);
                    }
                }
            }
            if (BitmapGeaendert != null) BitmapGeaendert(null);
        }

        public Bitmap gibWeltBild()
        {
            return bg;
        }
        
    }
}
