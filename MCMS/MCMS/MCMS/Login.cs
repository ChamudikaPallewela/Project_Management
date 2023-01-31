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

namespace MCMS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\OneDrive\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex= 0;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1) 
            {
                MessageBox.Show("Select your position ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(comboBox1.SelectedIndex == 0) 
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Enter both Password or UserName ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string sql = "select * from Doctor where DoctorName = '" + textBox1.Text + "' and DoctorPass = '" + textBox2.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    Con.Open();

                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        Doctor d = new Doctor();
                        d.Show();
                        this.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Wrong username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
            else
            {
                if(textBox1.Text == "Admin" && textBox2.Text == "Admin")
                {
                    Form1 f = new Form1();
                    f.Show();
                    this.Visible = false;

                }
                else if (textBox1.Text == "" || textBox2.Text == "") 
                {
                    MessageBox.Show("Enter both Password or UserName ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Incorrect Password or UserName ", "Medical Clinic Management System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Doctor d = new Doctor();
            d.Show();
            this.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
