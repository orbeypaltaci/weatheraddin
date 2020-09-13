using System;
using System.Windows.Forms;

namespace weatherAddIn
{
    public partial class API_Form : Form
    {
        public API_Form()
        {
            InitializeComponent();

            API_Key_Tbox.Text = Properties.Settings.Default.ApiKey;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ApiKey = API_Key_Tbox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
