using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POSales
{
    public partial class MainForm : Form
    {
        SqlConnection cn = new SqlConnection(); 
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        public string _pass;
        public MainForm()
        {
            InitializeComponent();
            customizeDesign();
            cn = new SqlConnection(dbcon.myConnection());
            cn.Open();
            MessageBox.Show("Database is Connected");
        }
        #region panelSlide
        private void customizeDesign()
        {
            panelSubProduct.Visible = false;    
            panelSubRecord.Visible = false;
            panelSubStock.Visible = false;
            panelSubSetting.Visible = false;    
        }

        private void hideSubmenu()
        {
            if (panelSubProduct.Visible == true)
                panelSubProduct.Visible = false;
            if (panelSubRecord.Visible == true)
                panelSubRecord.Visible = false;
            if (panelSubSetting.Visible == true)
                panelSubSetting.Visible = false;
            if (panelSubStock.Visible == true)
                panelSubStock.Visible = false;
        }

        private void showSubmenu(Panel submenu)
        {
            if(submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }
        #endregion panelSlide

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            lblTitle.Text = childForm.Text;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnDashboard_Click(object sender, EventArgs e)  //Dashboard
        {

        }
        private void button1_Click(object sender, EventArgs e)  //Product
        {
            showSubmenu(panelSubProduct);
        }
        private void button2_Click(object sender, EventArgs e)   //ProductList
        {
            openChildForm(new Product());
            hideSubmenu();
        }
        private void button3_Click(object sender, EventArgs e)    //Category
        {
            openChildForm(new Category());
            hideSubmenu();
        }
        private void button4_Click(object sender, EventArgs e)    //Brand
        {
            openChildForm(new Brand());
            hideSubmenu();
        }
        private void button5_Click(object sender, EventArgs e)    //Stock
        {
            showSubmenu(panelSubStock);
        }
        private void button6_Click(object sender, EventArgs e)    //StockEntry
        {
            openChildForm(new StockIn());
            hideSubmenu();
        }

        private void button7_Click(object sender, EventArgs e)    //StockAdjustment
        {
            hideSubmenu();
        }

        private void button8_Click(object sender, EventArgs e)    //Supplier
        {
            openChildForm(new Supplier());
            hideSubmenu();
        }

        private void button9_Click(object sender, EventArgs e)    //Record
        {
            showSubmenu(panelSubRecord);
        }
        private void button11_Click(object sender, EventArgs e)    //SalesHistory
        {
            hideSubmenu();
        }
        private void button10_Click(object sender, EventArgs e)    //POSRecord
        {
            hideSubmenu();
        }
        private void button12_Click(object sender, EventArgs e)    //Setting
        {
            showSubmenu(panelSubSetting);
        }
        private void btnUser_Click(object sender, EventArgs e)    //User
        {
            openChildForm(new UserAccount(this));
            hideSubmenu();
        }
        private void btnStore_Click(object sender, EventArgs e)    //Store
        {
            hideSubmenu();
            Store store = new Store();
            store.ShowDialog();
        }
        private void button15_Click(object sender, EventArgs e)    //Logout
        {
            hideSubmenu();
            if (MessageBox.Show("Logout Application?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
            }
        }
        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelTitle_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelSubProduct_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelSubStock_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelSubRecord_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
