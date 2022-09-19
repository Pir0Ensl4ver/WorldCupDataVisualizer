using System.ComponentModel;

namespace WorldCupVisualizerWinForms
{
    partial class PlayerUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.numberLabel = new System.Windows.Forms.Label();
            this.captainLabel = new System.Windows.Forms.Label();
            this.positionLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 132);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.Location = new System.Drawing.Point(151, 4);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(73, 33);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Name Label";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numberLabel
            // 
            this.numberLabel.Location = new System.Drawing.Point(151, 37);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(73, 33);
            this.numberLabel.TabIndex = 2;
            this.numberLabel.Text = "Number label";
            this.numberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // captainLabel
            // 
            this.captainLabel.Location = new System.Drawing.Point(151, 70);
            this.captainLabel.Name = "captainLabel";
            this.captainLabel.Size = new System.Drawing.Size(73, 33);
            this.captainLabel.TabIndex = 2;
            this.captainLabel.Text = "Captain Label";
            this.captainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // positionLabel
            // 
            this.positionLabel.Location = new System.Drawing.Point(151, 103);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(73, 33);
            this.positionLabel.TabIndex = 2;
            this.positionLabel.Text = "Position Label";
            this.positionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.captainLabel);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PlayerUserControl";
            this.Size = new System.Drawing.Size(327, 139);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.Label captainLabel;
        private System.Windows.Forms.Label positionLabel;

        private System.Windows.Forms.Label nameLabel;

        public System.Windows.Forms.PictureBox pictureBox1;

        #endregion
    }
}