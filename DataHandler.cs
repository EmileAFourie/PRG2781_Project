using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.IO;
using System.Data.Common;
using System.Reflection;

namespace PRG2781_Group_Project
{
    internal class DataHandler
    {
        // details for connection string
        static string host = "TAMMYLAPTOP\\SQLEXPRESS";
        static string database = "BCdatabase";
        public static string connString = @"Data Source = " + host + "; Initial Catalog=" + database + ";Integrated Security=SSPI";

        // variables for connection and commands
        SqlConnection conn;
        SqlCommand cmd;
        SqlCommand cmd2;
        SqlCommand cmd3;
        SqlDataReader reader;

        public DataHandler()
        {
            // Constructor
        }

        // Methods for Modules
        public void Register(string code, string Name, string description, string link)
        {
            string insert = @"INSERT INTO tblModule VALUES ('" + code + "', '" + Name + "', '" + description + "', '" + link + "')";

            conn = new SqlConnection(connString);

            conn.Open();

            cmd = new SqlCommand(insert, conn);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("The Module has been added!");
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error when trying to add the Module " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        public List<Module> ViewAll()
        {
            List<Module> module = new List<Module>();

            string ViewAll = @"SELECT * FROM tblModule";

            conn = new SqlConnection(connString);

            conn.Open();

            cmd = new SqlCommand(ViewAll, conn);

            Module objmodule = new Module();

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    objmodule.ModuleCode1 = reader[0].ToString();
                    objmodule.ModuleName1 = reader[1].ToString();
                    objmodule.ModuleDescription1 = reader[2].ToString();
                    objmodule.YouTubeLink1 = reader[3].ToString();


                    module.Add(new Module(objmodule.ModuleCode1, objmodule.ModuleName1, objmodule.ModuleDescription1, objmodule.YouTubeLink1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return module;

        }

        public void Update(string moduleCode, string Name, string Description, string YouTubeLink)
        {
            string UpdateStudent = @"Update tblModule SET ModuleCode = ('" + moduleCode + "'), ModuleName = ('" + Name + "'),ModuleDescription = ('" + Description + "'), Resources = ('" + YouTubeLink + "') where ModuleCode = ('" + moduleCode + "')";
            conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand updatecmd = new SqlCommand(UpdateStudent, conn);


            try
            {
                updatecmd.ExecuteNonQuery();
                MessageBox.Show("Module Details Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Details Not Updated" + ex);

            }
            finally
            {
                conn.Close();
            }
        }

        public List<string> SearchModule(string id)
        {
            string sqlCmd = @"SELECT * FROM tblModule WHERE ModuleCode = '" + id + "'";
            conn = new SqlConnection(connString);

            try
            {
                List<string> list = new List<string>();
                conn.Open();
                cmd = new SqlCommand(sqlCmd, conn);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();

                    list.Add(dr["ModuleCode"].ToString());
                    list.Add(dr["ModuleName"].ToString());
                    list.Add(dr["ModuleDescription"].ToString());
                    list.Add(dr["Resources"].ToString());
                }

                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }

            return null;
        }

        public List<Module> Search(string id)
        {
            List<Module> module = new List<Module>();

            string search = @"SELECT * FROM tblModule WHERE ModuleCode = ('" + id + "')";

            conn = new SqlConnection(connString);

            conn.Open();

            cmd = new SqlCommand(search, conn);

            Module objmodule = new Module();

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    objmodule.ModuleCode1 = reader[0].ToString();
                    objmodule.ModuleName1 = reader[1].ToString();
                    objmodule.ModuleDescription1 = reader[2].ToString();
                    objmodule.YouTubeLink1 = reader[3].ToString();


                    module.Add(new Module(objmodule.ModuleCode1, objmodule.ModuleName1, objmodule.ModuleDescription1, objmodule.YouTubeLink1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return module;

        }

        public void Delete(string ID)
        {
            string deleteModule = @"Delete from tblModule where ModuleCode = ('" + ID + "')";

            conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand delcmd = new SqlCommand(deleteModule, conn);


            try
            {
                delcmd.ExecuteNonQuery();
                MessageBox.Show("Module Details Deleted Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Details Not Deleted" + ex);

            }
            finally
            {
                conn.Close();
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------//

        // Methods for Students

        //Get students method
        public DataSet GetStudents()
        {
            string sqlCmd = @"SELECT * FROM tblStudents RIGHT JOIN tblStudent_Module ON tblStudent_Module.StudentNumber = tblStudents.StudentNumber";

            conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "tblStudents");
                return ds;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }

            return null;
        }

        // Add method
        public void AddStudent(string stdNum, string name, string surname, byte[] bytes, DateTime dob, string gender, string phone, string address, string module)
        {
            string sqlCmd = @"INSERT INTO tblStudents VALUES ('" + stdNum + "', '" + name + "', '" + surname + "', @img, '" + dob + "', '" + gender + "', '" + phone + "', '" + address + "')";
            string sqlCmd2 = @"INSERT INTO tblStudent_Module VALUES ('" + stdNum + "', '" + module + "')";

            conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlCmd, conn);
                cmd.Parameters.AddWithValue("@img", bytes);
                cmd.ExecuteNonQuery();

                cmd2 = new SqlCommand(sqlCmd2, conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // Update method
        public void UpdateStudent(string selectedID, string stdNum, string name, string surname, byte[] bytes, DateTime dob, string gender, string phone, string address, string module)
        {
            string sqlCmd = @"UPDATE tblStudents SET StudentNumber = '" + stdNum + "', StudentName = '" + name + "', StudentSurname = '" + surname + "', StudentImage =  @img, DateOfBirth = '" + dob + "', Gender = '" + gender + "', PhoneNumber = '" + phone + "', Address = '" + address + "' WHERE StudentNumber = '" + selectedID + "'";
            string sqlCmd2 = @"INSERT INTO tblStudent_Module Values ('" + stdNum + "', '" + module + "')";
            string slctCmd = @"SELECT ModuleCode FROM tblStudent_Module WHERE StudentNumber = '" + stdNum + "'";

            conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlCmd, conn);
                cmd.Parameters.AddWithValue("@img", bytes);
                cmd.ExecuteNonQuery();

                bool cont = false;
                cmd2 = new SqlCommand(slctCmd, conn);
                SqlDataReader dr = cmd2.ExecuteReader();
                while(dr.Read()) 
                {
                    if (dr["ModuleCode"].ToString() != module)
                    {
                        cont = true;
                        break;
                    }
                }
                dr.Close();
                if(cont)
                {
                    cmd3 = new SqlCommand(sqlCmd2, conn);
                    cmd3.ExecuteNonQuery();
                }
                else
                {
                    string sqlCmd3 = @"UPDATE tblStudent_Module SET StudentNumber = '" + stdNum + "', ModuleCode = '" + module + "'";
                    cmd3 = new SqlCommand(sqlCmd3, conn);
                    cmd3.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        // Delete method
        public void DeleteStudent(string selectedID)
        {
            string sqlCmd = @"DELETE FROM tblStudents WHERE StudentNumber = '" + selectedID + "'";
            string sqlCmd2 = @"DELETE FROM tblStudent_Module WHERE StudentNumber = '" + selectedID + "'";

            conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                cmd = new SqlCommand(sqlCmd, conn);
                cmd.ExecuteNonQuery();

                cmd2 = new SqlCommand(sqlCmd2, conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
        }

        // Search method
        public DataSet SearchStudent(string selectedID)
        {
            string sqlCmd = @"SELECT * FROM tblStudents RIGHT JOIN tblStudent_Module ON tblStudent_Module.StudentNumber = tblStudents.StudentNumber WHERE tblStudent_Module.StudentNumber = '" + selectedID + "'";
            conn = new SqlConnection(connString);

            try
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);
                DataSet ds = new DataSet();

                da.Fill(ds, "tblStudents");
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }

            return null;
        }

        public DataTable FillComboBox()
        {
            conn = new SqlConnection(connString);

            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT ModuleCode FROM tblModule", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            finally
            {
                conn.Close();
            }
            return null;
        }
    }
}
