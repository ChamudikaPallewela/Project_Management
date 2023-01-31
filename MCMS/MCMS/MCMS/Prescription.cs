using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Printing;
using System.Xml.Linq;

namespace MCMS
{
    public partial class Prescription : Form
    {

        public Prescription()
        {
            InitializeComponent();
            DisplayDoc();
            GetPatId();
            GetDocID();
        }
SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\OneDrive\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Prescription_Load(object sender, EventArgs e)
        {

        }
        private void DisplayDoc()
        {
            Con.Open();
            string Query = "Select * from Prescription";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dgvPrescription.DataSource = ds.Tables[0];
            Con.Close();
        }
        public void Clear()
        {
            txtPatient.Text = "";
            txtDoctor.Text = "";
            rtbMed.Text = "";
            txtCost.Text = "";
            cbDoctor.SelectedIndex = 0;
            cbPatientNo.SelectedIndex = 0;
        }
        public void GetDocName()
        {
            Con.Open();
            string Query = "SELECT * FROM Doctor where DoctorId="+ cbDoctor.SelectedValue.ToString() +"";
            SqlCommand cmd= new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sqa = new SqlDataAdapter(cmd);
            sqa.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtDoctor.Text = dr["DoctorName"].ToString();
            }

            Con.Close();
        }
        public void GetPatName()
        {
            Con.Open();
            string Query = "SELECT * FROM Patient where PatientId="+ cbPatientNo.SelectedValue.ToString() +"";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sqa = new SqlDataAdapter(cmd);
            sqa.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtPatient.Text = dr["PatientName"].ToString();
            }
            Con.Close();
        }
        public void GetDocID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select DoctorId from Doctor",Con);
            SqlDataReader sdr;
            sdr= cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DoctorId", typeof(int));
            dt.Load(sdr);
            cbDoctor.ValueMember = "DoctorId";
            cbDoctor.DataSource= dt;
            Con.Close();
        }
        public void GetPatId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PatientId from Patient",Con);
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatientId", typeof(int));
            dt.Load(sdr);
            cbPatientNo.ValueMember = "PatientId";
            cbPatientNo.DataSource = dt;
            Con.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string patId = cbPatientNo.Text;
            string docId = cbDoctor.Text;
            if (txtPatient.Text == "" || txtDoctor.Text == "" || rtbMed.Text == "" )
            {
                MessageBox.Show("Missing Information ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    string sql = "INSERT INTO Prescription(DoctorId, DoctorName, PatientId, PatientName, Medicince, Cost)VALUES('" + docId + "', '" + txtDoctor.Text + "', '" + patId + "', '" + txtPatient.Text + "','" + rtbMed.Text + "','" + txtCost.Text + "')";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prescription details added Successfully ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cbPatientNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetPatName();
        }

        private void cbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvPrescription_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPrescription.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvPrescription.CurrentRow.Selected = true;
                txtID.Text = dgvPrescription.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                cbDoctor.SelectedItem = dgvPrescription.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                txtDoctor.Text = dgvPrescription.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                cbPatientNo.SelectedItem = dgvPrescription.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                txtPatient.Text = dgvPrescription.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                rtbMed.Text = dgvPrescription.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
                txtCost.Text = dgvPrescription.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Visible = false;
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

        private void txtDoctor_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbDoctor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDocName();
        }

        private void dgvPrescription_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPrescription.CurrentRow.Selected = true;
            txtID.Text = dgvPrescription.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM Prescription WHERE PrescriptionId=" + Convert.ToInt32(txtID.Text.ToString());
                string sql1 = "UPDATE Prescription SET Cost='" + txtCost.Text + "'";
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }

