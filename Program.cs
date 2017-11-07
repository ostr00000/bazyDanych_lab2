using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new ProdContext())
            {
                //Console.WriteLine("Get new category");
                //string categoryName = Console.ReadLine();

                //Category newCategory = new Category() { Name = categoryName };

                //ctx.Categories.Add(newCategory);
                //ctx.SaveChanges();
                /*
                var query = from c in ctx.Categories
                            select c;

                Console.WriteLine("categories");
                foreach (var name in query)
                {
                    Console.WriteLine(name.Name);
                } 
                Console.WriteLine("end");*/

                CategoryForm catForm = new CategoryForm();
                catForm.ShowDialog();
                
                Console.ReadKey();
            }

        }
    }
}
