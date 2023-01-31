using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;


namespace MCMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DisplayDoc();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\OneDrive\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void DisplayDoc()
        {
            Con.Open();
            string Query = "Select * from Patient";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dgvPatient.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string DOB = dtpDOB.Value.ToString("yyyy-MM-dd");
            //assigning the date from the date time picker
            string GenderDo = cmbGender.Text; //assigning the Gender
            if (txtPatientName.Text == "" || txtAddressP.Text == "" || txtPhone.Text == "" || rtbSyntoms.Text == "" || DOB == "" || GenderDo == "")
            {
                MessageBox.Show("Missing Information ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    string sql = "INSERT INTO Patient(PatientName, PatientDOB, PatientGender, PatientAddress, PatientTel, PatientSymptom, PatientAllergies)VALUES('" + txtPatientName.Text + "', '" + DOB + "', '" + GenderDo + "', '" + txtAddressP.Text + "','" + txtPhone.Text + "','" + rtbSyntoms.Text + "','" + rtbAllergies.Text + "')";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pstient Registration Successful ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string DOB = dtpDOB.Value.ToString("yyyy-MM-dd");
            //assigning the date from the date time picker
            string GenderDo = cmbGender.Text; //assigning the Gender
            if (txtPatientName.Text == "" || txtAddressP.Text == "" || txtPhone.Text == "" || rtbSyntoms.Text == "" || DOB == "" || GenderDo == "")
            {
                MessageBox.Show("Missing Information ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string sql = "SELECT * FROM Patient WHERE PatientId=" + Convert.ToInt32(txtID.Text.ToString());
                    string sql1 = "UPDATE Patient SET PatientName='" + txtPatientName.Text + "', PatientDOB='" + DOB + "' , PatientGender ='" + GenderDo + "', PatientAddress= '" + txtAddressP.Text + "', PatientTel= '" + txtPhone.Text + "', PatientSymptom = '" + rtbSyntoms.Text + "', PatientAllergies = '" + rtbAllergies.Text + "'";
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
            string DOB = dtpDOB.Value.ToString("yyyy-MM-dd");
            //assigning the date from the date time picker
            string GenderDo = cmbGender.Text; //assigning the Gender
            try
            {
                DialogResult r = MessageBox.Show("Do you need to delete this record", "Medical Clinic Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (r == DialogResult.Yes)
                {
                    string sql = "DELETE FROM Patient WHERE PatientId=" + Convert.ToInt32(txtID.Text.ToString());
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

        private void dgvPatient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPatient.CurrentRow.Selected = true;
            txtID.Text = dgvPatient.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtPatientName.Text = dgvPatient.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            dtpDOB.Text = dgvPatient.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            cmbGender.SelectedItem = dgvPatient.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            txtAddressP.Text = dgvPatient.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            txtPhone.Text = dgvPatient.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            rtbSyntoms.Text = dgvPatient.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            rtbAllergies.Text = dgvPatient.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Doctor d = new Doctor();
            d.Show();
            this.Visible = false;
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
