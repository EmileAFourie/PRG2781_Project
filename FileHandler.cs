using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace PRG2781_Group_Project
{
    class FileHandler
    {
        frmMain frmMain = new frmMain();
        frmLogin frmLogin = new frmLogin();

        static bool Continue = true;
        public bool verification = false;
        static string line = string.Empty;

        public void RegisterUser(string username, string password)
        {
            try
            {
                FileStream f = new FileStream("../../LoginCredentials.txt", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(f);

                using (sw)
                {
                    sw.WriteLine(username + " " + password);
                }

                sw.Close();
                f.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("User could not be registered: " + ex);
            }
            finally
            {
                MessageBox.Show("User Registered Successfully!");
            }
        }

        public void VerifyUser(string username, string password)
        {
            try
            {
                FileStream f = new FileStream("../../LoginCredentials.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(f);

                while (Continue)
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tempArray = line.Split(' ');

                        if (tempArray[0] == username && tempArray[1] == password)
                        {
                            verification = true;
                            break;
                        }
                    }

                    if (verification.Equals(true))
                    {
                        MessageBox.Show("Successfully Logged In!");
                        Continue = false;
                    }
                    else if (verification.Equals(false))
                    {
                        MessageBox.Show("Incorrect Username or Password!" + "\n" + "Please try again!");
                        break;
                    }

                    sr.BaseStream.Position = 0;
                }

                sr.Close();
                f.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable To Log In: " + ex);
            }
            finally
            {
            }
        }
    }
}
