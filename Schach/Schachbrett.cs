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
        private int wpunkte = 0;
        private int spunkte = 0;
        public eventHandler BitmapGeaendert;
        private int beschriftung = 50;
        private Color hintergrundfarbe;
        private int auswahlZeile = -1;
        private int auswahlSpalte = -1;
        public Figur[,] Feld;
        public bool WeissAmZug = true;
        private bool isDebug = false;

        public bool Pro { get; set; }

        private bool koenigGeschlagen = false;

        public bool KoenigGeschlagen
        {
            get { return koenigGeschlagen; }
            set { koenigGeschlagen = value; }
        }

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

        public void neuesSpiel()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Feld[i, j] = null;
                }
            }
            
            //Weiße Figuren
            this.FigurHinzufuegen(new Turm(true, 0, 0, true, ref Feld));
            this.FigurHinzufuegen(new Turm(true, 0, 7, true, ref Feld));
            this.FigurHinzufuegen(new Springer(true, 0, 1, true, ref Feld));
            this.FigurHinzufuegen(new Springer(true, 0, 6, true, ref Feld));
            this.FigurHinzufuegen(new Bischhof(true, 0, 2, true, ref Feld));
            this.FigurHinzufuegen(new Bischhof(true, 0, 5, true, ref Feld));
            this.FigurHinzufuegen(new Koenig(true, 0, 4, true, ref Feld));
            this.FigurHinzufuegen(new Dame(true, 0, 3, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 0, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 1, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 2, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 3, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 4, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 5, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 6, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(true, 1, 7, true, ref Feld));
            //Schwarze Figuren
            this.FigurHinzufuegen(new Turm(false, 7, 0, true, ref Feld));
            this.FigurHinzufuegen(new Turm(false, 7, 7, true, ref Feld));
            this.FigurHinzufuegen(new Springer(false, 7, 1, true, ref Feld));
            this.FigurHinzufuegen(new Springer(false, 7, 6, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 7, true, ref Feld));
            this.FigurHinzufuegen(new Bischhof(false, 7, 2, true, ref Feld));
            this.FigurHinzufuegen(new Bischhof(false, 7, 5, true, ref Feld));
            this.FigurHinzufuegen(new Koenig(false, 7, 4, true, ref Feld));
            this.FigurHinzufuegen(new Dame(false, 7, 3, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 0, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 1, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 2, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 3, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 6, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 4, true, ref Feld));
            this.FigurHinzufuegen(new Bauer(false, 6, 5, true, ref Feld));
            wpunkte = 0;
            spunkte = 0;
            WeissAmZug = true;
            KoenigGeschlagen = false;
            aktualisiereBitmap();
        }

        public void spielSpeichern()
        {
            Figur[,] zwSpeicher = new Figur[8,8];
            FileStream fs = new FileStream("..\\..\\Speicherdaten\\Speicher.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            FileStream stat = new FileStream("..\\..\\Speicherdaten\\Stat.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            StreamWriter statw = new StreamWriter(stat);
            statw.WriteLine(wpunkte);
            statw.WriteLine(spunkte);
            statw.WriteLine(WeissAmZug);
            statw.WriteLine(KoenigGeschlagen);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Feld[i, j] != null)
                    {
                        sw.WriteLine(i + "-" + j + "-" + Feld[i, j].ToString() + "|" + Feld[i, j].Weiss);
                    }
                    else
                    {
                        sw.WriteLine(i + "-" + j + "-" + "null" + "|null");
                    }
                }
            }
            sw.Close();
            statw.Close();
        }

        public void spielLaden()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Feld[i, j] = null;
                }
            }
            Figur[,] zwSpeicher = new Figur[8, 8];
            FileStream fs = new FileStream("..\\..\\Speicherdaten\\Speicher.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            FileStream stat = new FileStream("..\\..\\Speicherdaten\\Stat.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs);
            StreamReader statr = new StreamReader(stat);
            string[] tmp1 = new string[4];
            for (int i = 0; i < 4; i++)
            {
                tmp1[i] = statr.ReadLine();
            }
            wpunkte = Convert.ToInt32(tmp1[0]);
            wpunkte = Convert.ToInt32(tmp1[1]);
            WeissAmZug = Convert.ToBoolean(tmp1[2].ToLower());
            KoenigGeschlagen = Convert.ToBoolean(tmp1[3].ToLower());
            string[,] tmp = new string[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tmp[i, j] = sr.ReadLine();
                    switch (tmp[i, j].Substring(4, tmp[i, j].IndexOf("|") - 4))
                    {
                        case "Schach.Bauer": this.FigurHinzufuegen(new Bauer(Convert.ToBoolean(tmp[i, j].Substring(tmp[i, j].IndexOf("|") + 1).ToLower()), Convert.ToInt32(tmp[i, j].Substring(2, 1)), Convert.ToInt32(tmp[i, j].Substring(0, 1)), true, ref Feld)); break;
                        case "Schach.Turm": this.FigurHinzufuegen(new Turm(Convert.ToBoolean(tmp[i, j].Substring(tmp[i, j].IndexOf("|") + 1).ToLower()), Convert.ToInt32(tmp[i, j].Substring(2, 1)), Convert.ToInt32(tmp[i, j].Substring(0, 1)), true, ref Feld)); break;
                        case "Schach.Koenig": this.FigurHinzufuegen(new Koenig(Convert.ToBoolean(tmp[i, j].Substring(tmp[i, j].IndexOf("|") + 1).ToLower()), Convert.ToInt32(tmp[i, j].Substring(2, 1)), Convert.ToInt32(tmp[i, j].Substring(0, 1)), true, ref Feld)); break;
                        case "Schach.Dame": this.FigurHinzufuegen(new Dame(Convert.ToBoolean(tmp[i, j].Substring(tmp[i, j].IndexOf("|") + 1).ToLower()), Convert.ToInt32(tmp[i, j].Substring(2, 1)), Convert.ToInt32(tmp[i, j].Substring(0, 1)), true, ref Feld)); break;
                        case "Schach.Springer": this.FigurHinzufuegen(new Springer(Convert.ToBoolean(tmp[i, j].Substring(tmp[i, j].IndexOf("|") + 1).ToLower()), Convert.ToInt32(tmp[i, j].Substring(2, 1)), Convert.ToInt32(tmp[i, j].Substring(0, 1)), true, ref Feld)); break;
                        case "Schach.Bischhof": this.FigurHinzufuegen(new Bischhof(Convert.ToBoolean(tmp[i, j].Substring(tmp[i, j].IndexOf("|") + 1).ToLower()), Convert.ToInt32(tmp[i, j].Substring(2, 1)), Convert.ToInt32(tmp[i, j].Substring(0, 1)), true, ref Feld)); break;
                        case "null": Feld[i, j] = null; break;
                        default: break;
                    }
                }
            }
            sr.Close();
            statr.Close();
            aktualisiereBitmap();
        }

        public void Waehle(int zeile, int spalte)
        {
            Console.WriteLine(zeile + " " + spalte);
            if (this.auswahlSpalte > -1 &&
                this.auswahlZeile > -1 &&
                Feld[this.auswahlSpalte, this.auswahlZeile] != null && 
                (Feld[this.auswahlSpalte, this.auswahlZeile].Weiss == this.WeissAmZug || this.isDebug) && 
                Feld[this.auswahlSpalte, this.auswahlZeile].kannBetreten(zeile, spalte) && !KoenigGeschlagen)
            {
                Feld[this.auswahlSpalte, this.auswahlZeile].FirstMove = false;
                Feld[this.auswahlSpalte, this.auswahlZeile].Spalte = spalte;
                Feld[this.auswahlSpalte, this.auswahlZeile].Zeile = zeile;

                Figur alteFigur = Feld[spalte, zeile];
                Feld[spalte, zeile] = Feld[this.auswahlSpalte, this.auswahlZeile];
                Feld[this.auswahlSpalte, this.auswahlZeile] = null;

                if (alteFigur != null)
                {
                    this.figurGeschlagen(alteFigur);
                }
                
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
        
        public void figurGeschlagen(Figur f)
        {
            if (!f.Weiss)
            {
                switch (f.ToString())
                {
                    case "Schach.Bauer": wpunkte += 1; break;
                    case "Schach.Turm": wpunkte += 5; break;
                    case "Schach.Koenig": wpunkte += 15; this.ende(!f.Weiss, wpunkte, spunkte); break;
                    case "Schach.Dame": wpunkte += 9; break;
                    case "Schach.Springer": wpunkte += 3; break;
                    case "Schach.Bischhof": wpunkte += 3; break;
                    default: break;
                }
            }
            else
            {
                switch (f.ToString())
                {
                    case "Schach.Bauer": spunkte += 1; break;
                    case "Schach.Turm": spunkte += 5; break;
                    case "Schach.Koenig": spunkte += 15; this.ende(!f.Weiss, wpunkte, spunkte); break;
                    case "Schach.Dame": spunkte += 9; break;
                    case "Schach.Springer": spunkte += 3; break;
                    case "Schach.Bischhof": spunkte += 3; break;
                    default: break;
                }
            }
        }

        public void ende(bool farbe, int wp,int sp)
        {
            KoenigGeschlagen = true;
            StreamWriter sw = new StreamWriter("..\\..\\Ergebnis\\Ergebnis.txt", append: true);
            sw.WriteLine("Spiel:" + "\r\n" + "Weis Punkte: " + wp + "\r\n" + "Schwarz Punkte: " + sp);
            sw.Close();
            MessageBox.Show(((farbe) ? "Weis" : "Schwarz") + " hat Gewonnen");
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

            if (Pro)
            {
            if (auswahlSpalte >= 0 && auswahlZeile >= 0 && Feld[auswahlSpalte, auswahlZeile] != null && !KoenigGeschlagen)
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
