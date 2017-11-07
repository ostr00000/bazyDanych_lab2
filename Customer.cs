using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataBase
{
    public class Customer
    {
        [Key]
        public string CompanyName { get; set; }
        public string Description{ get; set; }
    }
}
