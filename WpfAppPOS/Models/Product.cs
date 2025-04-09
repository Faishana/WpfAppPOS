using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPOS.Models
{
    public class Product
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
