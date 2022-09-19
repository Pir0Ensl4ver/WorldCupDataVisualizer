using System.ComponentModel;

namespace WorldCupVisualizerWinForms
{
    partial class FavoritePlayersForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoritePlayersForm));
            this.allPlayersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.favoritePlayersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnHighestScore = new System.Windows.Forms.Button();
            this.btnYellowCards = new System.Windows.Forms.Button();
            this.btnAttendance = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // allPlayersPanel
            // 
            resources.ApplyResources(this.allPlayersPanel, "allPlayersPanel");
            this.allPlayersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.allPlayersPanel.Name = "allPlayersPanel";
            // 
            // favoritePlayersPanel
            // 
            resources.ApplyResources(this.favoritePlayersPanel, "favoritePlayersPanel");
            this.favoritePlayersPanel.AllowDrop = true;
            this.favoritePlayersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.favoritePlayersPanel.Name = "favoritePlayersPanel";
            this.favoritePlayersPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.favoritePlayersPanel_DragDrop);
            this.favoritePlayersPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.favoritePlayersPanel_DragEnter);
            // 
            // btnHighestScore
            // 
            resources.ApplyResources(this.btnHighestScore, "btnHighestScore");
            this.btnHighestScore.Name = "btnHighestScore";
            this.btnHighestScore.UseVisualStyleBackColor = true;
            this.btnHighestScore.Click += new System.EventHandler(this.btnHighestScore_Click);
            // 
            // btnYellowCards
            // 
            resources.ApplyResources(this.btnYellowCards, "btnYellowCards");
            this.btnYellowCards.Name = "btnYellowCards";
            this.btnYellowCards.UseVisualStyleBackColor = true;
            this.btnYellowCards.Click += new System.EventHandler(this.btnYellowCards_Click);
            // 
            // btnAttendance
            // 
            resources.ApplyResources(this.btnAttendance, "btnAttendance");
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.UseVisualStyleBackColor = true;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // btnSettings
            // 
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.BackColor = System.Drawing.SystemColors.Info;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // FavoritePlayersForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnAttendance);
            this.Controls.Add(this.btnYellowCards);
            this.Controls.Add(this.btnHighestScore);
            this.Controls.Add(this.favoritePlayersPanel);
            this.Controls.Add(this.allPlayersPanel);
            this.Name = "FavoritePlayersForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FavoritePlayersForm_FormClosing);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnSettings;

        private System.Windows.Forms.Button btnHighestScore;
        private System.Windows.Forms.Button btnYellowCards;
        private System.Windows.Forms.Button btnAttendance;

        private System.Windows.Forms.FlowLayoutPanel favoritePlayersPanel;

        private System.Windows.Forms.FlowLayoutPanel allPlayersPanel;

        #endregion
    }
}