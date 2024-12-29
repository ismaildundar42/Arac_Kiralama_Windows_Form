namespace AracKiralamaUygulamasi
{
    partial class Kasa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtToplamAracAdet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtToplamMusteri = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToplamGelir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtToplamAracAdet
            // 
            this.txtToplamAracAdet.BackColor = System.Drawing.Color.LightSlateGray;
            this.txtToplamAracAdet.Enabled = false;
            this.txtToplamAracAdet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtToplamAracAdet.Location = new System.Drawing.Point(304, 31);
            this.txtToplamAracAdet.Name = "txtToplamAracAdet";
            this.txtToplamAracAdet.Size = new System.Drawing.Size(187, 34);
            this.txtToplamAracAdet.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(63, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 28);
            this.label1.TabIndex = 27;
            this.label1.Text = "Toplam Araç Adeti : ";
            // 
            // txtToplamMusteri
            // 
            this.txtToplamMusteri.BackColor = System.Drawing.Color.LightSlateGray;
            this.txtToplamMusteri.Enabled = false;
            this.txtToplamMusteri.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtToplamMusteri.Location = new System.Drawing.Point(304, 86);
            this.txtToplamMusteri.Name = "txtToplamMusteri";
            this.txtToplamMusteri.Size = new System.Drawing.Size(187, 34);
            this.txtToplamMusteri.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(87, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 28);
            this.label2.TabIndex = 29;
            this.label2.Text = "Toplam Müşteri : ";
            // 
            // txtToplamGelir
            // 
            this.txtToplamGelir.BackColor = System.Drawing.Color.LightSlateGray;
            this.txtToplamGelir.Enabled = false;
            this.txtToplamGelir.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtToplamGelir.Location = new System.Drawing.Point(304, 138);
            this.txtToplamGelir.Name = "txtToplamGelir";
            this.txtToplamGelir.Size = new System.Drawing.Size(187, 34);
            this.txtToplamGelir.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(69, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 28);
            this.label3.TabIndex = 31;
            this.label3.Text = "Toplam Kira Geliri : ";
            // 
            // Kasa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(569, 226);
            this.Controls.Add(this.txtToplamGelir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtToplamMusteri);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtToplamAracAdet);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Kasa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kasa";
            this.Load += new System.EventHandler(this.Kasa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtToplamAracAdet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtToplamMusteri;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtToplamGelir;
        private System.Windows.Forms.Label label3;
    }
}