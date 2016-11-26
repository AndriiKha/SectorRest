using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class Category
    {
        public Category() { }
        public virtual int Id { get; set; }
        public virtual Tabs Tabs { get; set; }
        public virtual string Name { get; set; }
    }
}
