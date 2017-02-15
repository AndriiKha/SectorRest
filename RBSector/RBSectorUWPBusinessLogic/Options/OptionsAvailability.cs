using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Options
{
    public static class OptionsAvailability
    {
        public static bool IS_EDITMODE { get; set; }
        public static bool CAN_ADDCATEGORY { get; set; }
        public static bool CAN_ADDPRODUCTS { get; set; }
    }
}
