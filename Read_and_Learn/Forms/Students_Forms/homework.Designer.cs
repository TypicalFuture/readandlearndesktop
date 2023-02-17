namespace Read_and_Learn.Forms.Students_Forms
{
    partial class homework
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(homework));
            this.panel2 = new Bunifu.UI.WinForms.BunifuGradientPanel();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderRadius = 1;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.GradientBottomLeft = System.Drawing.Color.White;
            this.panel2.GradientBottomRight = System.Drawing.Color.White;
            this.panel2.GradientTopLeft = System.Drawing.Color.White;
            this.panel2.GradientTopRight = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Quality = 10;
            this.panel2.Size = new System.Drawing.Size(934, 554);
            this.panel2.TabIndex = 6;
            // 
            // homework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 554);
            this.Controls.Add(this.panel2);
            this.Name = "homework";
            this.Text = "homework";
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.UI.WinForms.BunifuGradientPanel panel2;
    }
}