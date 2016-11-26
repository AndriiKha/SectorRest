using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class Products
    {
        public Products() { }
        public virtual int Id { get; set; }
        public virtual Images Images { get; set; }
        public virtual Category Category { get; set; }
        public virtual Tabs Tabs { get; set; }
        public virtual Ingredients Ingredients { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
    }
}
