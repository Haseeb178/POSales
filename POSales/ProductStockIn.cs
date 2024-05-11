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
    public partial class ProductStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        string stitle = "Point Of Sales";
        StockIn stockIn;
        public ProductStockIn(StockIn stk)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadProduct();
            stockIn = stk;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT pcode, pdesc, qty, PID FROM tbProduct" /*WHERE pdesc LIKE '%" + txtSearch.Text + "%'*/, cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(dr[3], dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                if(MessageBox.Show("Add this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    try
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tbStockIn (refno, pcode, sdate, stockinby, supplierid, PID)VALUES (@refno, @pcode, @sdate, @stockinby, @supplierid, @PID)", cn);
                        cm.Parameters.AddWithValue("@refno", stockIn.txtRefNo.Text);
                        cm.Parameters.AddWithValue("@pcode", dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cm.Parameters.AddWithValue("@sdate", stockIn.dtStockIn.Value);
                        cm.Parameters.AddWithValue("@stockinby", UserSession.Name);
                        cm.Parameters.AddWithValue("@supplierid", stockIn.lblId.Text);
                        cm.Parameters.AddWithValue("@PID", dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString());

                        cm.ExecuteNonQuery();
                        cn.Close();
                        stockIn.LoadStockIn();
                        MessageBox.Show("Sucessfully Added", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show(ex.Message, stitle);
                    }
                }
            }
        }
        public void addStockIn(string pcode)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("INSERT INTO tbStockIn (refno, pcode, sdate, stockinby, supplierid)VALUES (@refno, @pcode, @sdate, @stockinby, @supplierid)", cn);
                cm.Parameters.AddWithValue("@refno", stockIn.txtRefNo.Text);
                cm.Parameters.AddWithValue("@pcode", pcode);
                cm.Parameters.AddWithValue("@sdate", stockIn.dtStockIn.Value);
                cm.Parameters.AddWithValue("@stockinby", UserSession.Name);
                cm.Parameters.AddWithValue("@supplierid", stockIn.lblId.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                stockIn.LoadStockIn();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle);
            }
        }

        private void ProductStockIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void ProductStockIn_Load(object sender, EventArgs e)
        {

        }
    }
}
