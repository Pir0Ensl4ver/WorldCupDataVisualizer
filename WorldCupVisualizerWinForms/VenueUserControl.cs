using System;
using System.Windows.Forms;

namespace WorldCupVisualizerWinForms
{
    public partial class VenueUserControl : UserControl
    {
        
        public new string Location
        { 
            get => locationLabel.Text; 
            set => locationLabel.Text = value; 
        }
        public string Attendance
        {
            get => attendanceLabel.Text;
            set => attendanceLabel.Text = value;
        }
        public string HomeTeam
        {
            get => homeTeamLabel.Text;
            set => homeTeamLabel.Text = value;
        }
        public string AwayTeam
        {
            get => awayTeamLabel.Text;
            set => awayTeamLabel.Text = value;
        }
        
        public VenueUserControl()
        {
            InitializeComponent();
        }
    }
}