using System.ComponentModel;

namespace WorldCupVisualizerWinForms
{
    partial class FavoriteCountryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoriteCountryForm));
            this.label1 = new System.Windows.Forms.Label();
            this.chooseFavoriteTeamCB = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chooseFavoriteTeamCB
            // 
            this.chooseFavoriteTeamCB.FormattingEnabled = true;
            resources.ApplyResources(this.chooseFavoriteTeamCB, "chooseFavoriteTeamCB");
            this.chooseFavoriteTeamCB.Name = "chooseFavoriteTeamCB";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // FavoriteCountryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.chooseFavoriteTeamCB);
            this.Controls.Add(this.label1);
            this.Name = "FavoriteCountryForm";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnRun;

        private System.Windows.Forms.ProgressBar progressBar;

        private System.Windows.Forms.ComboBox chooseFavoriteTeamCB;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}