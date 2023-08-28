using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=LAPTOP-UDFFUK18\\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Enter the username");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Enter the password");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM Logindata WHERE Username = @username AND Password = @password";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                    sda.SelectCommand.Parameters.AddWithValue("@username", textBox1.Text);
                    sda.SelectCommand.Parameters.AddWithValue("@password", textBox2.Text);

                    DataTable dtable = new DataTable();
                    sda.Fill(dtable);

                    if (dtable.Rows.Count > 0)
                    {
                        MessageBox.Show("Login successful");
                    }
                    else
                    {
                        MessageBox.Show("Invalid login details");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
