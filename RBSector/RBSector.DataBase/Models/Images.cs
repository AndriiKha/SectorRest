using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class Images
    {
        public Images() { }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Byte { get; set; }
    }
}
