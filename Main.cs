using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Drawing.Configuration;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Data.Common;

namespace PRG2781_Group_Project
{
    public partial class frmMain : Form
    {
        DataHandler dataHandler = new DataHandler();
        public frmMain()
        {
            InitializeComponent();
        }

        // Module Code

        private void btnCreateNewModule_Click(object sender, EventArgs e)
        {
            dataHandler.Register(txtbxModuleCode.Text, txtbxModuleName.Text, txtbxModuleDescription.Text, lbxModuleLinks.Text);
            // Clear text
            txtbxModuleCode.Clear();
            txtbxModuleName.Clear();
            txtbxModuleDescription.Clear();
            lbxModuleLinks.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnDisplayModules_Click(object sender, EventArgs e)
        {
            dataHandler.ViewAll();
            dgvModules.DataSource = dataHandler.ViewAll();
        }

        private void btnUpdateModule_Click(object sender, EventArgs e)
        {
            dataHandler.Update(txtbxModuleCode.Text, txtbxModuleName.Text, txtbxModuleDescription.Text, lbxModuleLinks.Text);
        }

        private void btnSearchBy_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            list = dataHandler.SearchModule(txtbxSearchModules.Text);

            txtbxModuleCode.Text = list[0];
            txtbxModuleName.Text = list[1];
            txtbxModuleDescription.Text = list[2];
            lbxModuleLinks.Text = list[3];
        }

        private void btnDeleteModule_Click(object sender, EventArgs e)
        {
            dataHandler.Delete(txtbxModuleCode.Text);
        }

        //-----------------------------------------------------------------------------------------------------------------

        // Student Code

        private void uploadImgButt_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = new Bitmap(ofd.FileName);
            }
        }

        public void DisplayStudentDGV()
        {
            DataSet ds = dataHandler.GetStudents();
            stdDgv.DataSource = ds.Tables["tblStudents"].DefaultView;
            DataGridViewImageColumn imgCol = ((DataGridViewImageColumn)stdDgv.Columns["StudentImage"]);
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void createStdButt_Click(object sender, EventArgs e)
        {
            MemoryStream ms;
            byte[] bytes = null;

            // Get image using memory stream
            if (pictureBox.Image != null)
            {
                //Bitmap image = new Bitmap(pictureBox.Image);
                ms = new MemoryStream();
                pictureBox.Image.Save(ms, ImageFormat.Jpeg);
                bytes = ms.ToArray();

                // Get info
                string stdNum = stdNumTxt.Text;
                string name = stdNameTxt.Text;
                string surname = stdSurnameTxt.Text;
                DateTime dob = stdDobDate.Value.Date;
                string gender = stdGenderTxt.Text;
                string phone = stdPhoneTxt.Text;
                string address = stdAddressTxt.Text;
                string module = stdModuleBox.Text;

                // Add to database
                dataHandler.AddStudent(stdNum, name, surname, bytes, dob, gender, phone, address, module);

                MessageBox.Show("Student saved successfully!");

                // Clear text
                stdNumTxt.Clear();
                stdNameTxt.Clear();
                stdSurnameTxt.Clear();
                stdGenderTxt.Clear();
                stdPhoneTxt.Clear();
                stdAddressTxt.Clear();
                stdModuleBox.Text = "";
                pictureBox.Image = null;
            }
            else
            {
                // Get info
                string stdNum = stdNumTxt.Text;
                string name = stdNameTxt.Text;
                string surname = stdSurnameTxt.Text;
                DateTime dob = stdDobDate.Value.Date;
                string gender = stdGenderTxt.Text;
                string phone = stdPhoneTxt.Text;
                string address = stdAddressTxt.Text;
                string module = stdModuleBox.Text;

                // Add to database
                dataHandler.AddStudent(stdNum, name, surname, bytes, dob, gender, phone, address, module);
                MessageBox.Show("Student saved successfully!");
            }
        }

        private void displayStdButt_Click(object sender, EventArgs e)
        {
            DisplayStudentDGV();
        }

        private void updateStdButt_Click(object sender, EventArgs e)
        {
            MemoryStream ms;
            byte[] bytes = null;

            // Get image using memory stream
            if (pictureBox.Image != null)
            {
                ms = new MemoryStream();
                pictureBox.Image.Save(ms, ImageFormat.Jpeg);
                bytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(bytes, 0, bytes.Length);

                // Get info
                string stdNum = stdNumTxt.Text;
                string name = stdNameTxt.Text;
                string surname = stdSurnameTxt.Text;
                DateTime dob = stdDobDate.Value.Date;
                string gender = stdGenderTxt.Text;
                string phone = stdPhoneTxt.Text;
                string address = stdAddressTxt.Text;
                string module = stdModuleBox.Text;

                dataHandler.UpdateStudent(stdNum, stdNum, name, surname, bytes, dob, gender, phone, address, module);    
            }
            else
            {
                // Get info
                string stdNum = stdNumTxt.Text;
                string name = stdNameTxt.Text;
                string surname = stdSurnameTxt.Text;
                DateTime dob = stdDobDate.Value.Date;
                string gender = stdGenderTxt.Text;
                string phone = stdPhoneTxt.Text;
                string address = stdAddressTxt.Text;
                string module = stdModuleBox.Text;

                dataHandler.UpdateStudent(stdNum, stdNum, name, surname, bytes, dob, gender, phone, address, module);
            }
        }

        private void deleteStdButt_Click(object sender, EventArgs e)
        {
            // Find selected entry/row
            string selectedID = null;

            if (stdDgv.SelectedCells.Count > 0)
            {
                selectedID = stdDgv.SelectedCells[0].Value.ToString();
            }
            //string selectedID = row.Cells[0].Value.ToString();

            // Update database entry
            if (selectedID != null)
            {
                dataHandler.DeleteStudent(selectedID);
            }

            // Update data grid view
            DisplayStudentDGV();
        }

        private void searchStdButt_Click(object sender, EventArgs e)
        {
            DataSet ds = dataHandler.SearchStudent(searchStdTxt.Text);

            DisplayStudentDGV();
        }

        private void stdDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = stdDgv.Rows[e.RowIndex];
                stdNumTxt.Text = row.Cells[0].Value.ToString();
                stdNameTxt.Text = row.Cells[1].Value.ToString();
                stdSurnameTxt.Text = row.Cells[2].Value.ToString();
                Image img = (Bitmap)((new ImageConverter()).ConvertFrom(row.Cells[3].Value));
                pictureBox.Image = img;
                stdDobDate.Text = row.Cells[4].Value.ToString();
                stdGenderTxt.Text = row.Cells[5].Value.ToString();
                stdPhoneTxt.Text = row.Cells[6].Value.ToString();
                stdAddressTxt.Text = row.Cells[7].Value.ToString();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            DataTable dt = dataHandler.FillComboBox();
            stdModuleBox.DataSource = dt;
            stdModuleBox.DisplayMember = "ModuleCode";
            stdModuleBox.ValueMember = "ModuleCode";
        }
    }
}
