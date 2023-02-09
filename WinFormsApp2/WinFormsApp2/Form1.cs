using MySql.Data.MySqlClient;
namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        static MySqlConnection conn = null;
        public Form1()
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoginValidation();
        }
    }
}