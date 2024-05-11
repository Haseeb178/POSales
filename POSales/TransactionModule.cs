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

namespace POSales
{
    public partial class TransactionModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        String transactionId;
        public TransactionModule(String trxId)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            transactionId = trxId;
            LoadTransactionProducts();
        }

        public void LoadTransactionProducts()
        {
            int i = 0;
            dgvTransactionModule.Rows.Clear();
            cm = new SqlCommand("SELECT tp.transactionId, p.pcode, p.pdesc, tp.quantity,   p.price FROM TransactionProduct tp JOIN tbProduct p ON tp.productId = p.PID WHERE tp.transactionId=@trxId;", cn);
            cm.Parameters.AddWithValue("@trxId", transactionId);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvTransactionModule.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            cn.Close();
        }
    }
}
