using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG2781_Group_Project
    {
        public partial class frmLogin : Form
        {
            public frmLogin()
            {
                InitializeComponent();
            }

            private void button1_Click(object sender, EventArgs e)
            {
                FileHandler fh = new FileHandler();
                fh.VerifyUser(txtUsername.Text, txtPassword.Text);

                if (fh.verification == true)
                {
                    frmMain frmMain = new frmMain();
                    Hide();
                    frmMain.ShowDialog();
                    Close();
                }
            }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Register frmRegister = new Register();
            frmRegister.Show();
            this.Hide();
        }
    }
    }
