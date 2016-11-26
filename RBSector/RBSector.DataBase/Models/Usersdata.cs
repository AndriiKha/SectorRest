using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class Usersdata
    {
        public Usersdata() { }
        public virtual int Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
        public virtual string Fname { get; set; }
        public virtual string Lname { get; set; }
        public virtual string Email { get; set; }
        public virtual string Role { get; set; }
    }
}
