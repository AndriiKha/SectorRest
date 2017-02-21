using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
    public class CalculatorService
    {
        private static CalculatorService Calculator;
        private string text;
        public string Text { get { return this.text == null ? string.Empty : this.text; } set { text = value; } }
        private CalculatorService() { }
        public static CalculatorService Instance()
        {
            if (Calculator == null) Calculator = new CalculatorService();
            return Calculator;
        }
    }
}
