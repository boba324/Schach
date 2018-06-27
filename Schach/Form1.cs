using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schach
{
    public partial class Form1 : Form
    {
        private Schachbrett sch = new Schachbrett(8,8);
        public Form1()
        {
            InitializeComponent();
            Initialize();
            //Weiße Figuren
            sch.FigurHinzufuegen(new Turm(true, 0, 0, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Turm(true, 0, 7, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Springer(true, 0, 1, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Springer(true, 0, 6, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bischhof(true, 0, 2, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bischhof(true, 0, 5, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Koenig(true, 0, 4, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Dame(true, 0, 3, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 0, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 1, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 2, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 3, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 4, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 5, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 6, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(true, 1, 7, true, ref sch.Feld));
            //Schwarze Figuren
            sch.FigurHinzufuegen(new Turm(false, 7, 0, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Turm(false, 7, 7, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Springer(false, 7, 1, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Springer(false, 7, 6, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bischhof(false, 7, 2, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bischhof(false, 7, 5, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Koenig(false, 7, 4, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Dame(false, 7, 3, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 0, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 1, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 2, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 3, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 4, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 5, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 6, true, ref sch.Feld));
            sch.FigurHinzufuegen(new Bauer(false, 6, 7, true, ref sch.Feld));
            DialogResult dialogResult = MessageBox.Show("Können sie Schach spielen?", "Frage", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                sch.Pro = false;
            }
            else if (dialogResult == DialogResult.No)
            {
                sch.Pro = true;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            panel1.Invalidate();
        }
        private void Initialize()
        {
            this.ClientSize = new Size(sch.gibBreite() * 42 + 50 + 150, sch.gibHoehe() * 42 + 100);
            sch.HintergrundFarbe = this.BackColor;
            sch.BitmapGeaendert += OnPaint;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(sch.gibWeltBild(), Point.Empty);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        { 
            //Auf welches Feld wird geklickt
            sch.Waehle((int)Math.Floor((float)e.Y / 44 -1), (int)Math.Floor((float)e.X / 44 -1));

            //Wer ist am Zug ?
            if (sch.WeissAmZug)
            {
                label1.Text = "Weiss";
            }
            else
            {
                label1.Text = "Schwarz";
            }

            if (sch.KoenigGeschlagen)
            {
                btnNeuesSpiel.Visible = true;
                label1.Visible = false;
            }
        }

        private void btnNeuesSpiel_Click(object sender, EventArgs e)
        {
            sch.neuesSpiel();
            btnNeuesSpiel.Visible = false;
            label1.Visible = true;
            label1.Text = "Weiss";
        }

        private void btnSpielSpeichern_Click(object sender, EventArgs e)
        {
            sch.spielSpeichern();
        }

        private void btnSpielLaden_Click(object sender, EventArgs e)
        {
            sch.spielLaden();
            if (sch.WeissAmZug)
            {
                label1.Text = "Weiss";
            }
            else
            {
                label1.Text = "Schwarz";
            }
        }
    }
}
