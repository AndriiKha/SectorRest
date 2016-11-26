using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class Ingredients
    {
        public Ingredients() { }
        public virtual int Id { get; set; }
        public virtual string Count { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
