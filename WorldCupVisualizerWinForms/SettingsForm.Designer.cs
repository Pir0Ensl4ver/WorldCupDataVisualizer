using System.ComponentModel;

namespace WorldCupVisualizerWinForms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.chooseLeagueCB = new System.Windows.Forms.ComboBox();
            this.chooseLanguageCB = new System.Windows.Forms.ComboBox();
            this.chooseLeagueLabel = new System.Windows.Forms.Label();
            this.chooseLanguageLabel = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chooseLeagueCB
            // 
            resources.ApplyResources(this.chooseLeagueCB, "chooseLeagueCB");
            this.chooseLeagueCB.DisplayMember = "Key";
            this.chooseLeagueCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chooseLeagueCB.FormattingEnabled = true;
            this.chooseLeagueCB.Name = "chooseLeagueCB";
            this.chooseLeagueCB.ValueMember = "Value";
            // 
            // chooseLanguageCB
            // 
            resources.ApplyResources(this.chooseLanguageCB, "chooseLanguageCB");
            this.chooseLanguageCB.DisplayMember = "Key";
            this.chooseLanguageCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chooseLanguageCB.FormattingEnabled = true;
            this.chooseLanguageCB.Name = "chooseLanguageCB";
            this.chooseLanguageCB.ValueMember = "Value";
            // 
            // chooseLeagueLabel
            // 
            resources.ApplyResources(this.chooseLeagueLabel, "chooseLeagueLabel");
            this.chooseLeagueLabel.Name = "chooseLeagueLabel";
            // 
            // chooseLanguageLabel
            // 
            resources.ApplyResources(this.chooseLanguageLabel, "chooseLanguageLabel");
            this.chooseLanguageLabel.Name = "chooseLanguageLabel";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chooseLeagueCB);
            this.Controls.Add(this.chooseLanguageCB);
            this.Controls.Add(this.chooseLeagueLabel);
            this.Controls.Add(this.chooseLanguageLabel);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ComboBox chooseLeagueCB;
        private System.Windows.Forms.ComboBox chooseLanguageCB;
        private System.Windows.Forms.Label chooseLeagueLabel;
        private System.Windows.Forms.Label chooseLanguageLabel;
        private System.Windows.Forms.Button btnSave;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}