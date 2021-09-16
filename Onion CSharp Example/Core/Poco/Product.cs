using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Poco
{
    public class Product
    {
        public int Id { get; set; }//4
        public string Name { get; set; }//50 * 2 => 103 
        public string Description { get; set; }//200 * 2 => 403
        public string Brand { get; set; }//50 => 103
        public string Model { get; set; }//50 => 103
        public decimal Price { get; set; }//16 
        public int Stock { get; set; }//4
        public string ImageURL { get; set; }//1024 => 2051
    }
}
