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
    public partial class Form2CURD : Form
    {
        MySqlConnection conn = new MySqlConnection("server = localhost; uid = root; database = taeyoung1");
        DataTable dataTable = new DataTable();

        public Form2CURD()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Form2CURD_Load_1(object sender, EventArgs e)
        {
            fillData();
        }

        public DataTable getDataTable()
        {
            dataTable.Reset();
            dataTable = new DataTable();

            using (MySqlCommand command = new MySqlCommand("SELECT * FROM detail_transaksi", conn))
            {
                conn.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
            }

            return dataTable;

        }

        public void fillData()
        {
            DataGridPerusahaan.DataSource = getDataTable();

            DataGridPerusahaan.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridPerusahaan.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataGridViewButtonColumn colEdit    = new DataGridViewButtonColumn();
            colEdit.UseColumnTextForButtonValue = true                          ;
            colEdit.Text                        = "Edit"                        ;
            colEdit.Name                        = ""                            ;
            DataGridPerusahaan.Columns.Add(colEdit)                             ;

            DataGridViewButtonColumn colDelete      = new DataGridViewButtonColumn()    ;
            colDelete.UseColumnTextForButtonValue   = true                              ;
            colDelete.Text                          = "Delete"                          ;
            colDelete.Name                          = ""                                ;
            DataGridPerusahaan.Columns.Add(colDelete)                                   ;

            DataGridViewButtonColumn colBarang = new DataGridViewButtonColumn();
            colBarang.UseColumnTextForButtonValue = true;
            colBarang.Text = "Lihat Barang";
            colBarang.Name = "";
            DataGridPerusahaan.Columns.Add(colBarang);

            conn.Close();
        }

        public void ClearTextBox()
        {
            TxtSuratJalan.Text  = "" ;
            TxtPO.Text          = "" ;
            TxtInvoice.Text     = "" ;
            TxtFakturPajak.Text = "" ;
            TxtKontrak.Text     = "" ;
            TxtPackingList.Text = "" ;
            TxtKendaraan.Text   = "" ;
            TxtArticle.Text     = "" ;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;
            conn.Open();

            string No_Surat_Jalan   = this.TxtSuratJalan.Text   ;
            string No_PO            = this.TxtPO.Text           ;
            string No_Invoice       = this.TxtInvoice.Text      ;
            string No_Faktur_Pajak  = this.TxtFakturPajak.Text  ;
            string No_Kontrak       = this.TxtKontrak.Text      ;
            string No_Packing_List  = this.TxtPackingList.Text  ;
            string No_Kendaraan     = this.TxtKendaraan.Text    ;
            string Article          = this.TxtArticle.Text      ;


            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Detail_Transaksi ORDER BY id_Detail_Transaksi DESC LIMIT 1;";

            string LastID = Convert.ToString(cmd.ExecuteScalar());

            int IntId = 0;

            if(LastID == null)
            {
                IntId = 1;
            }
            else
            {
                string subString = LastID.Substring(LastID.Length - 4, 4);
                IntId = Convert.ToInt32(subString) + 1;
               
            }

            string ID_Transaksi_Detail = "DT";

            if(IntId < 10)
            {
                ID_Transaksi_Detail = string.Concat(ID_Transaksi_Detail, "000", IntId.ToString());
            }
            else if (IntId < 100 && IntId >= 10)
            {
                ID_Transaksi_Detail = string.Concat(ID_Transaksi_Detail, "00", IntId.ToString());
            }
            else if (IntId < 1000 && IntId >= 100)
            {
                ID_Transaksi_Detail = string.Concat(ID_Transaksi_Detail, "0", IntId.ToString());
            }
            else
            {
                ID_Transaksi_Detail = string.Concat(ID_Transaksi_Detail, IntId.ToString());
            }

            cmd.CommandText = "INSERT INTO detail_transaksi VALUE (@ID_Transaksi_Detail, @No_Surat_Jalan, @No_PO_PWI, @No_Invoice, @No_Faktur_Pajak, @No_Kontrak, @No_Packing_List, @No_Kendaraan, @Article)";

            cmd.Parameters.AddWithValue("@ID_Transaksi_Detail"  , ID_Transaksi_Detail   );
            cmd.Parameters.AddWithValue("@No_Surat_Jalan"       , No_Surat_Jalan        );
            cmd.Parameters.AddWithValue("@No_PO_PWI"            , No_PO                 );
            cmd.Parameters.AddWithValue("@No_Invoice"           , No_Invoice            );
            cmd.Parameters.AddWithValue("@No_Faktur_Pajak"      , No_Faktur_Pajak       );
            cmd.Parameters.AddWithValue("@No_Kontrak"           , No_Kontrak            );
            cmd.Parameters.AddWithValue("@No_Packing_List"      , No_Packing_List       );
            cmd.Parameters.AddWithValue("@No_Kendaraan"         , No_Kendaraan          );
            cmd.Parameters.AddWithValue("@Article"              , Article               );

            cmd.ExecuteNonQuery ();
            conn.Close          ();

            DataGridPerusahaan.Columns.Clear();

            dataTable.Clear ();
            fillData        ();

            ClearTextBox();
        }
    }
}
