namespace MTGO_dek_FIXER
{
    partial class MainWindow
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
            this.btnPARSE = new System.Windows.Forms.Button();
            this.btnCOLLECTION = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSTATUS = new System.Windows.Forms.Label();
            this.lblFILENAME = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPARSE
            // 
            this.btnPARSE.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPARSE.Location = new System.Drawing.Point(147, 28);
            this.btnPARSE.Name = "btnPARSE";
            this.btnPARSE.Size = new System.Drawing.Size(492, 80);
            this.btnPARSE.TabIndex = 0;
            this.btnPARSE.Text = "IMPORT AND PARSE .DEK FILE";
            this.btnPARSE.UseVisualStyleBackColor = true;
            this.btnPARSE.Click += new System.EventHandler(this.btnPARSE_Click);
            // 
            // btnCOLLECTION
            // 
            this.btnCOLLECTION.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCOLLECTION.Location = new System.Drawing.Point(246, 137);
            this.btnCOLLECTION.Name = "btnCOLLECTION";
            this.btnCOLLECTION.Size = new System.Drawing.Size(285, 80);
            this.btnCOLLECTION.TabIndex = 1;
            this.btnCOLLECTION.Text = "IMPORT COLLECTION .DEK";
            this.btnCOLLECTION.UseVisualStyleBackColor = true;
            this.btnCOLLECTION.Click += new System.EventHandler(this.btnCOLLECTION_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "STATUS:";
            // 
            // lblSTATUS
            // 
            this.lblSTATUS.AutoSize = true;
            this.lblSTATUS.Location = new System.Drawing.Point(12, 271);
            this.lblSTATUS.Name = "lblSTATUS";
            this.lblSTATUS.Size = new System.Drawing.Size(45, 20);
            this.lblSTATUS.TabIndex = 3;
            this.lblSTATUS.Text = "none";
            // 
            // lblFILENAME
            // 
            this.lblFILENAME.AutoSize = true;
            this.lblFILENAME.Location = new System.Drawing.Point(802, 39);
            this.lblFILENAME.Name = "lblFILENAME";
            this.lblFILENAME.Size = new System.Drawing.Size(0, 20);
            this.lblFILENAME.TabIndex = 4;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1678, 502);
            this.Controls.Add(this.lblFILENAME);
            this.Controls.Add(this.lblSTATUS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCOLLECTION);
            this.Controls.Add(this.btnPARSE);
            this.Name = "MainWindow";
            this.Text = "MTGO .dek Cleaner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPARSE;
        private System.Windows.Forms.Button btnCOLLECTION;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSTATUS;
        private System.Windows.Forms.Label lblFILENAME;
    }
}

