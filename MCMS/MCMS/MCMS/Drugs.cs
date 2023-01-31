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


namespace MCMS
{
    public partial class Drugs : Form
    {
        public Drugs()
        {
            InitializeComponent();
        }
 SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\OneDrive\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void DisplayDoc()
        {
            Con.Open();
            string Query = "Select * from Drugs";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            dgvDrugs.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string DOB = dTP.Value.ToString("yyyy-MM-dd");

            if (txtName.Text == "" || DOB == "")
            {
                MessageBox.Show("Missing Information ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    string sql = "INSERT INTO Drugs(DrugName, DrugExpD)VALUES('" + txtName.Text + "', '" + DOB + "')";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    Con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Drug Registration Successful ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button2_Click(object sender, EventArgs e)
        {
            string DOB = dTP.Value.ToString("yyyy-MM-dd");

            if (txtName.Text == "" || DOB == "")
            {
                MessageBox.Show("Missing Information ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string sql = "SELECT * FROM Drugs WHERE DrugId=" + Convert.ToInt32(txtID.Text.ToString());
                    string sql1 = "UPDATE Drugs SET DrugName='" + txtName.Text + "', DrugExpD='" + DOB + "' ";
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

        private void button3_Click(object sender, EventArgs e)
        {
            string DOB = dTP.Value.ToString("yyyy-MM-dd");
            try
            {
                DialogResult r = MessageBox.Show("Do you need to delete this record", "Medical Clinic Management System", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (r == DialogResult.Yes)
                {
                    string sql = "DELETE FROM Drugs WHERE DrugId=" + Convert.ToInt32(txtID.Text.ToString());
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

        private void dgvDrugs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDrugs.CurrentRow.Selected = true;
            txtID.Text = dgvDrugs.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            txtName.Text = dgvDrugs.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            dTP.Text = dgvDrugs.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            
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

        private void label14_Click(object sender, EventArgs e)
        {
            Prescription p = new Prescription();
            p.Show();
            this.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void dgvDrugs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
