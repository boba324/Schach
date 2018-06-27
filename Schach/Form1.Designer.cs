namespace Schach
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNeuesSpiel = new System.Windows.Forms.Button();
            this.btnSpielSpeichern = new System.Windows.Forms.Button();
            this.btnSpielLaden = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(543, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Weiss";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 479);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // btnNeuesSpiel
            // 
            this.btnNeuesSpiel.Location = new System.Drawing.Point(549, 119);
            this.btnNeuesSpiel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNeuesSpiel.Name = "btnNeuesSpiel";
            this.btnNeuesSpiel.Size = new System.Drawing.Size(88, 48);
            this.btnNeuesSpiel.TabIndex = 2;
            this.btnNeuesSpiel.Text = "Neues Spiel";
            this.btnNeuesSpiel.UseVisualStyleBackColor = true;
            this.btnNeuesSpiel.Visible = false;
            this.btnNeuesSpiel.Click += new System.EventHandler(this.btnNeuesSpiel_Click);
            // 
            // btnSpielSpeichern
            // 
            this.btnSpielSpeichern.Location = new System.Drawing.Point(549, 410);
            this.btnSpielSpeichern.Margin = new System.Windows.Forms.Padding(4);
            this.btnSpielSpeichern.Name = "btnSpielSpeichern";
            this.btnSpielSpeichern.Size = new System.Drawing.Size(88, 30);
            this.btnSpielSpeichern.TabIndex = 3;
            this.btnSpielSpeichern.Text = "Speichern";
            this.btnSpielSpeichern.UseVisualStyleBackColor = true;
            this.btnSpielSpeichern.Click += new System.EventHandler(this.btnSpielSpeichern_Click);
            // 
            // btnSpielLaden
            // 
            this.btnSpielLaden.Location = new System.Drawing.Point(549, 447);
            this.btnSpielLaden.Margin = new System.Windows.Forms.Padding(4);
            this.btnSpielLaden.Name = "btnSpielLaden";
            this.btnSpielLaden.Size = new System.Drawing.Size(88, 28);
            this.btnSpielLaden.TabIndex = 4;
            this.btnSpielLaden.Text = "Laden";
            this.btnSpielLaden.UseVisualStyleBackColor = true;
            this.btnSpielLaden.Click += new System.EventHandler(this.btnSpielLaden_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(655, 523);
            this.Controls.Add(this.btnSpielLaden);
            this.Controls.Add(this.btnSpielSpeichern);
            this.Controls.Add(this.btnNeuesSpiel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Schach made by Kadir, Andreas, Bastian";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNeuesSpiel;
        private System.Windows.Forms.Button btnSpielSpeichern;
        private System.Windows.Forms.Button btnSpielLaden;
    }
}

