using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RBSector.Tools
{
    public class Tools
    {
        public static EditPart EditObj(object obj)
        {
            EditPart editPart = EditPart.NONE;
            Button button = obj as Button;
            if (button == null)
                return editPart;
            switch (button.Name)
            {
                case "btn_AddNewTab":
                    editPart = EditPart.TAB;
                    break;
                case "btn_NewCategory":
                    editPart = EditPart.CATEGORY;
                    break;
                case "btn_NewProducts":
                    editPart = EditPart.PRODUCT;
                    break;
                default:
                    editPart = EditPart.NONE;
                    break;
            }
            return editPart;
        }
    }
    public enum EditPart { TAB, CATEGORY, PRODUCT, NONE};
}
