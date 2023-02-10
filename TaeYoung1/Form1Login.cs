using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TaeYoung1
{
    public partial class Form1Login : Form
    {
        static MySqlConnection conn = null;
        public Form1Login()
        {
            InitializeComponent();
            Connection();
        }

        public static void Connection()
        {
            string connStr = "server=localhost;port=3306;database=tut;user=root;password=Enyoi321!;";

            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void LoginValidation()
        {
            string user = txtUser.Text;
            string pass = txtPass.Text;

             
            try
            {
                conn.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM login", conn);
                MySqlDataReader Reader = mySqlCommand.ExecuteReader();
                while (Reader.Read())
                {
                    if (user == Reader.GetString("Username") && pass == Reader.GetString("password"))
                    {
                        MessageBox.Show("Authorized User");
                        Form2CURD formNext = new Form2CURD();
                        formNext.Show();
                        conn.Close();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
            MessageBox.Show("Invalid User");
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LoginValidation();
        }
    }
}
