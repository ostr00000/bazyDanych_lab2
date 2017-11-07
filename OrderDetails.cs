using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public int quantity { get; set; }
    }
}
