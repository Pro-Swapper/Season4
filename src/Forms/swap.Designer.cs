namespace Pro_Swapper
{
    partial class swap
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
            this.SwapB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.image = new System.Windows.Forms.PictureBox();
            this.log = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // SwapB
            // 
            this.SwapB.BackColor = System.Drawing.Color.White;
            this.SwapB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SwapB.FlatAppearance.BorderSize = 0;
            this.SwapB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SwapB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SwapB.Location = new System.Drawing.Point(253, 144);
            this.SwapB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SwapB.Name = "SwapB";
            this.SwapB.Size = new System.Drawing.Size(162, 41);
            this.SwapB.TabIndex = 0;
            this.SwapB.UseVisualStyleBackColor = false;
            this.SwapB.Click += new System.EventHandler(this.ConvertWork);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(322, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "ON";
            // 
            // image
            // 
            this.image.BackColor = System.Drawing.Color.Transparent;
            this.image.ErrorImage = null;
            this.image.InitialImage = null;
            this.image.Location = new System.Drawing.Point(3, 3);
            this.image.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(244, 230);
            this.image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image.TabIndex = 2;
            this.image.TabStop = false;
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.Color.White;
            this.log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.log.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log.Location = new System.Drawing.Point(0, 240);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.Size = new System.Drawing.Size(427, 35);
            this.log.TabIndex = 11;
            // 
            // swap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(427, 275);
            this.Controls.Add(this.log);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.image);
            this.Controls.Add(this.SwapB);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "swap";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SwapB;
        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox log;
    }
}

