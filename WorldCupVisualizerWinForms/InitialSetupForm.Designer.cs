namespace WorldCupVisualizerWinForms
{
    partial class InitialSetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialSetupForm));
            this.chooseLanguageLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.chooseLeagueLabel = new System.Windows.Forms.Label();
            this.chooseLanguageCB = new System.Windows.Forms.ComboBox();
            this.chooseLeagueCB = new System.Windows.Forms.ComboBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chooseLanguageLabel
            // 
            resources.ApplyResources(this.chooseLanguageLabel, "chooseLanguageLabel");
            this.chooseLanguageLabel.Name = "chooseLanguageLabel";
            // 
            // titleLabel
            // 
            resources.ApplyResources(this.titleLabel, "titleLabel");
            this.titleLabel.Name = "titleLabel";
            // 
            // chooseLeagueLabel
            // 
            resources.ApplyResources(this.chooseLeagueLabel, "chooseLeagueLabel");
            this.chooseLeagueLabel.Name = "chooseLeagueLabel";
            // 
            // chooseLanguageCB
            // 
            resources.ApplyResources(this.chooseLanguageCB, "chooseLanguageCB");
            this.chooseLanguageCB.DisplayMember = "Key";
            this.chooseLanguageCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chooseLanguageCB.FormattingEnabled = true;
            this.chooseLanguageCB.Name = "chooseLanguageCB";
            this.chooseLanguageCB.ValueMember = "Value";
            this.chooseLanguageCB.SelectedIndexChanged += new System.EventHandler(this.chooseLanguageCB_SelectedIndexChanged);
            // 
            // chooseLeagueCB
            // 
            resources.ApplyResources(this.chooseLeagueCB, "chooseLeagueCB");
            this.chooseLeagueCB.DisplayMember = "Key";
            this.chooseLeagueCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chooseLeagueCB.FormattingEnabled = true;
            this.chooseLeagueCB.Name = "chooseLeagueCB";
            this.chooseLeagueCB.ValueMember = "Value";
            this.chooseLeagueCB.SelectedIndexChanged += new System.EventHandler(this.chooseLeagueCB_SelectedIndexChanged);
            // 
            // btnRun
            // 
            resources.ApplyResources(this.btnRun, "btnRun");
            this.btnRun.Name = "btnRun";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // InitialSetupForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.chooseLeagueCB);
            this.Controls.Add(this.chooseLanguageCB);
            this.Controls.Add(this.chooseLeagueLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.chooseLanguageLabel);
            this.Name = "InitialSetupForm";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Button btnRun;

        private System.Windows.Forms.ComboBox chooseLanguageCB;
        private System.Windows.Forms.ComboBox chooseLeagueCB;

        private System.Windows.Forms.Label chooseLeagueLabel;

        private System.Windows.Forms.Label titleLabel;

        private System.Windows.Forms.Label chooseLanguageLabel;

        #endregion
    }
}