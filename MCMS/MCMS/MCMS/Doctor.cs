using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace MCMS
{
    public partial class Doctor : Form
    {
        
        public Doctor()
        {
            InitializeComponent();
            DisplayDoc();

        }
SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\OneDrive\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void DisplayDoc()
        {
            Con.Open();
            string Query = "Select * from Doctor";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder= new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dGVDoctor.DataSource= ds.Tables[0];
            Con.Close();
        }
        public void Clear()
        {
            txtDocName.Text = "";
            txtAddressDoc.Text = "";
            txtSpecialization.Text = "";
            txtDocPhone.Text = "";
            txtPasswordDoc.Text = "";
            txtExperience.Text = "";
            cBGenDoc.SelectedIndex= 0;
         }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string regdate = dTPReg.Value.ToString("yyyy-MM-dd");
            string DOB = dTPDoc.Value.ToString("yyyy-MM-dd");
            //assigning the date from the date time picker
            string GenderDo = cBGenDoc.Text; //assigning the Gender
            if (txtDocName.Text == "" || txtAddressDoc.Text == "" || txtSpecialization.Text == "" || txtDocPhone.Text == "" || txtPasswordDoc.Text == "" || txtExperience.Text == "" || regdate == "" || DOB == "" || GenderDo == "")
            {
                MessageBox.Show("Missing Information ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    
                    string sql = "INSERT INTO Doctor(DoctorName, DoctorDOB, DoctorGender, DocotorAddress, DoctorSpecization, DoctorPhone, DoctorPass, DoctorRegistationDate ,DoctorExperence)VALUES('"+ txtDocName.Text + "', '" + DOB + "', '" + GenderDo + "', '" + txtAddressDoc.Text + "','" + txtSpecialization.Text + "','" + txtDocPhone.Text + "','" + txtPasswordDoc.Text + "','" + regdate + "','" + txtExperience.Text + "')";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Registration Successful ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Con.Close();
                    DisplayDoc();
                }
            }
        }
        int key = 0;
        private void dGVDoctor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string regdate = dTPReg.Value.ToString("yyyy-MM-dd");
            string DOB = dTPDoc.Value.ToString("yyyy-MM-dd");
            //assigning the date from the date time picker
            string GenderDo = cBGenDoc.Text; //assigning the Gender
            if (txtDocName.Text == "" || txtAddressDoc.Text == "" || txtSpecialization.Text == "" || txtDocPhone.Text == "" || txtPasswordDoc.Text == "" || txtExperience.Text == "" || regdate == "" || DOB == "" || GenderDo == "")
            {
                MessageBox.Show("Missing Information ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string sql = "SELECT * FROM Doctor WHERE DoctorId=" + Convert.ToInt32(txtDocId.Text.ToString());
                    string sql1 = "UPDATE Doctor SET DoctorName='" + txtDocName.Text + "', DoctorDOB='" + DOB + "' , DoctorGender ='" + GenderDo + "', DocotorAddress= '" + txtAddressDoc.Text + "', DoctorSpecization= '" + txtSpecialization.Text + "', DoctorPhone = '" + txtDocPhone.Text + "', DoctorPass = '" + txtDocPhone.Text + "', DoctorRegistationDate = '" + regdate + "' , DoctorExperence='" + txtExperience.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    SqlCommand cmd1 = new SqlCommand(sql1, Con);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Record Successfully updated", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Con.Close();
                    DisplayDoc();

                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string regdate = dTPReg.Value.ToString("yyyy-MM-dd");
            string DOB = dTPDoc.Value.ToString("yyyy-MM-dd");
            //assigning the date from the date time picker
            string GenderDo = cBGenDoc.Text; //assigning the Gender
           
                try
                {
                    DialogResult r = MessageBox.Show("Do you need to delete this record", "Medical Clinic Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (r == DialogResult.Yes)
                    {
                        string sql = "DELETE FROM Doctor WHERE DoctorId=" + Convert.ToInt32(txtDocId.Text.ToString());
                        SqlCommand cmd = new SqlCommand(sql, Con);
                        Con.Open();

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record Successfully deleted", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Con.Close();
                    DisplayDoc();
                }
          
        
            
        }

        private void dGVDoctor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dGVDoctor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) 
            {
                dGVDoctor.CurrentRow.Selected= true;
                txtDocId.Text = dGVDoctor.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                txtDocName.Text = dGVDoctor.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                dTPDoc.Text = dGVDoctor.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                cBGenDoc.SelectedItem = dGVDoctor.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                txtAddressDoc.Text = dGVDoctor.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                txtSpecialization.Text = dGVDoctor.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                txtDocPhone.Text = dGVDoctor.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
                txtPasswordDoc.Text = dGVDoctor.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
                dTPReg.Text = dGVDoctor.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();
                txtExperience.Text = dGVDoctor.Rows[e.RowIndex].Cells[9].FormattedValue.ToString();
                
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Visible = false;
        }

        private void Doctor_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Drugs d1 = new Drugs();
            d1.Show();
            this.Visible = false;
        }

        private void label14_Click(object sender, EventArgs e)
        {

            Prescription p = new Prescription();
            p.Show();
            this.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
