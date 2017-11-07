using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace DataBase
{
    public partial class CategoryForm : Form
    {
        internal ProdContext context;

        public CategoryForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.context = new ProdContext();
            this.context.Categories.Load();

            this.categoryBindingSource.DataSource = 
                this.context.Categories.Local.ToBindingList();
        }

        private void categoryBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            
        }

        private void categoryBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void categoryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();

            this.context.SaveChanges();
            this.categoryDataGridView.Refresh();
            this.productsDataGridView.Refresh();
        }

        public void reload()
        {
            this.context.Categories.Load();
            this.categoryBindingSource.DataSource =
                this.context.Categories.Local.ToBindingList();

            loadProducts();

            this.categoryDataGridView.Refresh();
            this.productsDataGridView.Refresh();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.context.Dispose();
        }

        private void categoryDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            loadProducts();
        }

        private void loadProducts()
        {
            int currentSelectedId = (int)categoryDataGridView.CurrentRow.Cells[0].Value;            

            var query =
                (from prod in this.context.Products
                 where currentSelectedId == prod.CategoryID
                 select new
                 {
                     ProductId = prod.ProductID,
                     Name = prod.Name,
                     Unitprice = prod.Unitprice,
                     UnitsInStock = prod.UnitsInStock
                 }).ToList();

            this.productsDataGridView.DataSource = query;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCategoryForm catForm = new AddCategoryForm(this);
            catForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddProductForm prodForm = new AddProductForm(this);
            prodForm.ShowDialog();
        }

        private void buttonMakeOrder_Click(object sender, EventArgs e)
        {
            string customerName =
                (from cust in context.Customers
                 where cust.CompanyName == textBoxClientName.Text
                 select cust.CompanyName).SingleOrDefault();

            if (customerName == null)
            {
                MessageBox.Show("customer doesn't exist");
                return;
            }
            
            this.Visible = false;
            MakeOrderForm orderForm = new MakeOrderForm(this, customerName);
            orderForm.ShowDialog();
        }


    }
}
