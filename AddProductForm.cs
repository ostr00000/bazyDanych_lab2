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
    public partial class AddProductForm : Form
    {
        internal CategoryForm categoryForm;

        public AddProductForm(CategoryForm cat)
        {
            InitializeComponent();
            this.Load += initializeComboBoxCategory;
            this.categoryForm = cat;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
            DestroyHandle();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textName.Text == "")
            {
                MessageBox.Show("Get product name");
                return;
            }

            int unitInStock = 0;
            if (textUnitInStock.Text == "" ||
                !Int32.TryParse(textUnitInStock.Text, out unitInStock))
            {
                MessageBox.Show("Get number of unit in stock");
                return;   
            }

            decimal unitPrice;
            if(textUnitprice.Text == "" ||  
               !Decimal.TryParse(textUnitprice.Text, out unitPrice))
            {
                MessageBox.Show("Get unit price");
                return;
            }

            if(comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Select category");
                return;
            }

            using (var context = new ProdContext())
            {

                int? categoryId = (from cat in this.categoryForm.context.Categories
                                   where cat.Name == comboBox1.SelectedItem.ToString()
                                   select cat.CategoryID).FirstOrDefault();

                if (categoryId == null)
                {
                    MessageBox.Show("You need to chose category");
                    return;
                }

                Product product = new Product()
                {
                    Name = textName.Text,
                    Unitprice = unitPrice,
                    UnitsInStock = unitInStock,
                    CategoryID = (int)categoryId,
                };

                context.Products.Add(product);
                context.SaveChanges();
            }

            this.categoryForm.reload();
                
             Hide();
             DestroyHandle();

        }

        private void initializeComboBoxCategory(object sender, EventArgs e)
        {
            var categories =
                from cat in this.categoryForm.context.Categories
                orderby cat.Name
                select cat.Name;
            
            foreach (var cat in categories)
            {
                comboBox1.Items.Add(cat);
            }
        }

   

  


    }
}
