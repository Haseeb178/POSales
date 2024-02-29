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
    public partial class Qty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        string stitle = "Point Of Sales";
        private string pcode;
        private double price;
        private String trasnno;
        private int qty;
        Cashier cashier;
        public Qty(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cashier = cash;
        }
        public void ProductDetails(string pcode, double price, string trasnno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.trasnno = trasnno;
            this.qty = qty;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (txtQty.Text != string.Empty))
            {
                try
                {
                    string id = "";
                    int cart_qty = 0;
                    bool found = false;
                    cn.Open();
                    cm = new SqlCommand("Select * from tbCart Where trasnno = @trasnno and pcode = @pcode", cn);
                    cm.Parameters.AddWithValue("@trasnno", trasnno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        id = dr["id"].ToString();
                        cart_qty = int.Parse(dr["qty"].ToString());
                        found = true;
                    }
                    else found = false;
                    dr.Close();
                    cn.Close();

                    if (found)
                    {
                        if (qty < (int.Parse(txtQty.Text) + cart_qty))
                        {
                            MessageBox.Show("Unable to procced. Remaining qty on hand is" + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cn.Open();
                        cm = new SqlCommand("Update tbCart set qty = (qty + " + int.Parse(txtQty.Text) + ")Where id= '" + id + "'", cn);
                        cm.ExecuteReader();
                        cn.Close();
                        cashier.txtBarcode.Clear();
                        cashier.txtBarcode.Focus();
                        cashier.LoadCart();
                        this.Dispose();
                    }
                    else
                    {
                        if (qty < (int.Parse(txtQty.Text) + cart_qty))
                        {
                            MessageBox.Show("Unable to procced. Remaining qty on hand is" + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tbCart(trasnno, pcode, price, qty, sdate, cashir)VALUES(@trasnno, @pcode, @price, @qty, @sdate, @cashir)", cn);
                        cm.Parameters.AddWithValue("@trasnno", trasnno);
                        cm.Parameters.AddWithValue("@pcode", pcode);
                        cm.Parameters.AddWithValue("@price", price);
                        cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                        cm.Parameters.AddWithValue("@cashir", cashier.lblUsername.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        cashier.txtBarcode.Clear();
                        cashier.txtBarcode.Focus();
                        cashier.LoadCart();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, stitle);
                }
            }
        }

        private void Qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
