using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int UnitsInStock { get; set; }
        public int CategoryID { get; set; }

        [Column(TypeName="Money")]
        public decimal Unitprice{ get; set; }
    }
}
