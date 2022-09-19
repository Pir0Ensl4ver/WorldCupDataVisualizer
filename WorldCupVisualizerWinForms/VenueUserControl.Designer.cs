using System.ComponentModel;

namespace WorldCupVisualizerWinForms
{
    partial class VenueUserControl
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
            this.location = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.locationLabel = new System.Windows.Forms.Label();
            this.attendanceLabel = new System.Windows.Forms.Label();
            this.homeTeamLabel = new System.Windows.Forms.Label();
            this.awayTeamLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // location
            // 
            this.location.Location = new System.Drawing.Point(8, 14);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(72, 17);
            this.location.TabIndex = 0;
            this.location.Text = "Location";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(86, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Attendance";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(164, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Home team";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(242, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Away team";
            // 
            // locationLabel
            // 
            this.locationLabel.Location = new System.Drawing.Point(8, 51);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(72, 17);
            this.locationLabel.TabIndex = 4;
            this.locationLabel.Text = "Location";
            // 
            // attendanceLabel
            // 
            this.attendanceLabel.Location = new System.Drawing.Point(86, 51);
            this.attendanceLabel.Name = "attendanceLabel";
            this.attendanceLabel.Size = new System.Drawing.Size(72, 17);
            this.attendanceLabel.TabIndex = 5;
            this.attendanceLabel.Text = "Attendance";
            // 
            // homeTeamLabel
            // 
            this.homeTeamLabel.Location = new System.Drawing.Point(164, 51);
            this.homeTeamLabel.Name = "homeTeamLabel";
            this.homeTeamLabel.Size = new System.Drawing.Size(72, 17);
            this.homeTeamLabel.TabIndex = 6;
            this.homeTeamLabel.Text = "HomeTeam";
            // 
            // awayTeamLabel
            // 
            this.awayTeamLabel.Location = new System.Drawing.Point(242, 51);
            this.awayTeamLabel.Name = "awayTeamLabel";
            this.awayTeamLabel.Size = new System.Drawing.Size(72, 17);
            this.awayTeamLabel.TabIndex = 7;
            this.awayTeamLabel.Text = "AwayTeam";
            // 
            // VenueUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.awayTeamLabel);
            this.Controls.Add(this.homeTeamLabel);
            this.Controls.Add(this.attendanceLabel);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.location);
            this.Name = "VenueUserControl";
            this.Size = new System.Drawing.Size(318, 76);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label attendanceLabel;
        private System.Windows.Forms.Label homeTeamLabel;
        private System.Windows.Forms.Label awayTeamLabel;
        private System.Windows.Forms.Label location;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}