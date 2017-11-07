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
    public partial class AddCategoryForm : Form
    {
        CategoryForm categoryForm;

        public AddCategoryForm(CategoryForm cat)
        {
            InitializeComponent();
            this.categoryForm = cat;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textName.Text == "")
            {
                MessageBox.Show("Get name of category");
                return;
            }

            Category cat = new Category();
            cat.Name = textName.Text;
            cat.Description = textDesc.Text;

            using (var context = new ProdContext())
            {
                context.Categories.Add(cat);
                context.SaveChanges();
            }

            this.categoryForm.reload();

            Hide();
            DestroyHandle();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            DestroyHandle();
        }

    }
}
