using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    class Order
    {
        [Key]
        public int OrederId { get; set; }

        public string CustomerName { get; set; }
        public List<OrderDetails> OrderDetail { get; set; }
    }
}
