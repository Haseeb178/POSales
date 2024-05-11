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
    public partial class Transaction : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Transaction()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadTransactions();
        }

        public void LoadTransactions()
        {
            int totalAmount = 0;
            int totalTransactions = 0;
            dgvTransaction.Rows.Clear();
            cm = new SqlCommand("SELECT transactionId, transactionDate, customerName, totalAmount FROM dbo.tbTransaction order by transactionDate desc", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                totalTransactions++;
                totalAmount += Convert.ToInt32(dr["totalAmount"]);
                dgvTransaction.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            lblAmount.Text = totalAmount.ToString();
            lblTrx.Text = totalTransactions.ToString();
            dr.Close();
            cn.Close();
        }

        private void dgvBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvTransaction.Columns[e.ColumnIndex].Name;
            if (colName == "Details")
            { 
                TransactionModule transaction = new TransactionModule(dgvTransaction.Rows[e.RowIndex].Cells[0].Value.ToString());
                
                transaction.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbTransaction WHERE transactionId LIKE '" + dgvTransaction.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Transaction has been successfully deleted.", "Point Of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            LoadTransactions();
        }
    }
}
