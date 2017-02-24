using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class UserViewModel : BaseModel
    {
        public int USR_RECID { get; set; }
        public string USR_Login { get; set; }
        public string USR_FName { get; set; }
        public string USR_LName { get; set; }
        public string USR_Email { get; set; }
        public string USR_Role { get; set; }
    }
}
