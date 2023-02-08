using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace WPFLoginBisaYuk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;


        public MainWindow()
        {
            server = "localhost";
            database = "logintut";
            uid = "root";
            password = "";

            string connString;
            connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            conn = new MySqlConnection(connString);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string user = tbUser.Text;
            string pass = tbPass.Text;
            Register(user,pass);

            if (Register(user, pass))
            {
                MessageBox.Show($"User {user} has been created!");
            }
            else
            {
                MessageBox.Show($"User {user} has not been created!");
            }
        }

        public bool Register(string user, string pass)
        {
            string query = $"INSERT INTO users(id,username,password) VALUE ('','{user}','{pass}');";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query,conn);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch(Exception ex)
            {
                conn.Close();
                return false;
            }
        }
    

         public bool IsLogin(string user,string pass)
        {
            string query = $"SELECT * FROM users WHERE username= '{user}' AND password = '{pass}';";

            try
            {
                if(OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query,conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }catch(Exception ex)
            {
                conn.Close();
                return false;
            }
        } 

        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Connection to server failed!");
                        break;
                    case 1045:
                        MessageBox.Show("Server sername or password is incorrect!");
                        break;
                }
                return false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string user = tbUser.Text;
            string pass = tbPass.Text;
            Register(user, pass);

            if (IsLogin(user, pass))
            {
                MessageBox.Show($"Welcome {user}!");
            }
            else
            {
                MessageBox.Show($"{user} does not exist or password is incorrect!");
            }
        }
    }
}

