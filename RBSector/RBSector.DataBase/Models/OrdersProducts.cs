using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class Ordersproducts
    {
        public virtual int Id { get; set; }
        public virtual Products Products { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual int? Count { get; set; }
    }
}
