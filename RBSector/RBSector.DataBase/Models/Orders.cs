using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class Orders
    {
        public Orders() { }
        public virtual int Id { get; set; }
        public virtual Usersdata Usersdata { get; set; }
        public virtual DateTime Orderdate { get; set; }
        public virtual decimal Pricecost { get; set; }
        public virtual decimal Getmoney { get; set; }
    }
}
