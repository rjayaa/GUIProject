﻿using System;
using System.Collections.Generic;
using System.Configuration.Provider;
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
using Project00;

namespace wepeefHAPHAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MySqlConnection conn = null;
        public MainWindow()
        {
            InitializeComponent();
            Connection();
        }

       
        public static void Connection()
        {
            string connStr = "server=localhost;port=3306;database=tut;user=root;password=Enyoi321!;";

            try
            {
                conn= new MySqlConnection(connStr);
                conn.Open();
                MessageBox.Show("Connection Open");
            }
            catch (Exception ex) { 
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
                while(Reader.Read())
                {
                    if(user == Reader.GetString("Username") && pass == Reader.GetString("password"))
                    {
                        MessageBox.Show("Authorized User");
                        MenuTable mt1 = new MenuTable();
                        this.Content=mt1;
                        conn.Close();
                        return;
                    }
                }
            }
            catch(Exception ex)
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginValidation();
        }
    }
}
