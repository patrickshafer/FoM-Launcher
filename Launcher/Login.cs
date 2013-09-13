using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoM.Launcher
{
    public partial class Login : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Login()
        {
            InitializeComponent();
        }
        public string Username { get { return this.UsernameText.Text; } }
        public string Password { get { return this.PasswordText.Text; } }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (this.UsernameText.Text.Length < 3)
                    {
                        e.Cancel = true;
                        UsernameText.Focus();
                        MessageBox.Show("You must provide a valid Username", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (this.PasswordText.Text.Length < 1)
                    {
                        e.Cancel = true;
                        PasswordText.Focus();
                        MessageBox.Show("You must provide a valid Password", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }
        //forgot password: System.Diagnostics.Process.Start(@"http://www.faceofmankind.com/account/reset");
    }
}
