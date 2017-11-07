using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class MakeOrderForm : Form
    {
        CategoryForm categoryForm;
        ProdContext context;
        Order order;

        public MakeOrderForm(CategoryForm cat, string customerName)
        {
            InitializeComponent();

            this.order = new Order() { CustomerName = customerName };
            this.context = new ProdContext();
            this.context.Orders.Add(this.order);
            this.context.SaveChanges();

            this.categoryForm = cat;

            this.Load += initialize;

        }

        private void initialize(object sender, EventArgs e)
        {
            var categories = this.categoryForm.context.Categories
                .Include("Products")
                .OrderBy(c => c.Name)
                .Select(c => new { name = c.Name, prod = c.Products });

            foreach (var cat in categories)
            {
                comboBox1.Items.Add(cat.name);
            }

            var query = (from cat in categories
                         from prod in cat.prod
                         select new
                         {
                             ProductID = prod.ProductID,
                             Name = prod.Name,
                             CategoryName = cat.name,
                             Unitprice = prod.Unitprice
                         }).ToList();
            this.productsBindingSource.DataSource = query;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int quantity = 1;
            if (textBoxAdd.Text != "")
            {
                bool succes = Int32.TryParse(textBoxAdd.Text, out quantity);
                if (!succes)
                {
                    MessageBox.Show("invalid number");
                    return;
                }
            }

            int? prodId = (int)productsDataGridView.CurrentRow.Cells[0].Value;
            if (prodId == null)
            {
                MessageBox.Show("select product");
                return;
            }

            OrderDetails orderDetail = new OrderDetails()
            {
                ProductId = (int)prodId,
                quantity = quantity,
                OrderID = this.order.OrederId
            };
            this.context.OrderDetails.Add(orderDetail);
            this.context.SaveChanges();

            var query = this.context.Orders
                .Where(o => o.OrederId == this.order.OrederId)
                .Join(this.context.OrderDetails, o => o.OrederId, od => od.OrderID, (o, od) => od)
                .Select(detail => new
                    {
                        quantity = detail.quantity,

                        productId = detail.ProductId,

                        cost = detail.quantity * (this.context.Products
                            .Where(p => p.ProductID == detail.ProductId)
                            .Select(p => p.Unitprice).FirstOrDefault()),

                        name = this.context.Products
                            .Where(p => p.ProductID == detail.ProductId)
                            .Select(p => p.Name).FirstOrDefault()
                    }
                ).ToList();

            this.orderDetailDataGridView.DataSource = query;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryName = comboBox1.SelectedItem.ToString();
            int? categoryId = (from cat in this.categoryForm.context.Categories
                               where cat.Name == categoryName
                               select cat.CategoryID).FirstOrDefault();

            if (categoryId == null)
            {
                MessageBox.Show("category doesn't exist");
                return;
            }
            using (var context = new ProdContext())
            {
                var query = from prod in context.Products
                            where prod.CategoryID == (int)categoryId
                            select new
                            {
                                Name = prod.Name,
                                CategoryName = categoryName,
                                Unitprice = prod.Unitprice,
                                ProductId = prod.ProductID
                            };

                productsDataGridView.DataSource = query.ToList();
            }

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Hide();
            this.categoryForm.Visible = true;
            DestroyHandle();

        }

    }
}
