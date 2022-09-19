using System;
using System.Windows.Forms;

namespace WorldCupVisualizerWinForms
{
    public partial class PlayerUserControl : UserControl
    {
        
        public string Name
        { 
            get => nameLabel.Text; 
            set => nameLabel.Text = value; 
        }
        public string Number
        {
            get => numberLabel.Text;
            set => numberLabel.Text = value;
        }
        public string Position
        {
            get => positionLabel.Text;
            set => positionLabel.Text = value;
        }
        public string Captain
        {
            get => captainLabel.Text;
            set => captainLabel.Text = value;
        }
        
        public PlayerUserControl()
        {
            InitializeComponent();
        }
    }
}