using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Pdf;

namespace RBSectorUWPBusinessLogic.Service
{
    public class СalculationService
    {
        //WebView

        private PdfPage page;
        private string FontFamily = "Geneva";
        private CalculatorService calculator_srv;
        private UserService user_srv;
        public СalculationService()
        {
            user_srv = UserService.Instance();
            calculator_srv = CalculatorService.Instance();
        }
        public StringBuilder CreatePdf(OrderViewModel order)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(StartHtml());
            string element = "<hr>";
            sb.AppendLine(element);
            /* string dateNow = DateTime.Now.Day.ToString() + "." +
                              DateTime.Now.Month.ToString() + "." +
                              DateTime.Now.Year.ToString() + " " +
                              DateTime.Now.Hour.ToString() + ":" +
                              DateTime.Now.Minute.ToString();*/
            if (order.Ord_OrderDate == null || order.Ord_OrderDate == DateTime.MinValue)
                order.Ord_OrderDate = DateTime.Now;
            string dateNow = order.Ord_OrderDate.ToString();
            element = Align(dateNow, AlignEnum.right);
            element = HtmlFormatParagraph("Bill : " + order.Ord_RECID.ToString() + element);
            sb.AppendLine(element);
            sb.Append("<table style=\"width: 100 % \">");
            sb.Append("<tr>");
            element = HtmlTableTitle("Name", "Count", "Price", "Summ");
            sb.AppendLine(element);
            element = "<hr>";
            sb.AppendLine(element);
            foreach (var item in order.Product_ORD)
            {
                sb.AppendLine(HtmlTableRow(item.PR_Name, item.ORD_Count.ToString(), item.Price.ToString(), (item.Price * item.ORD_Count).ToString()));
            }
            sb.Append("</table>");

            sb.AppendLine("<hr>");
            sb.AppendLine(HtmlFormatParagraph(Align("Total" + (new string(' ', 10)) + order.Ord_PriceCost.ToString(), AlignEnum.left)));
            sb.AppendLine("<hr>");
            if (order.Ord_GotMoney == default(decimal))
                order.Ord_GotMoney = JSonTools.JsonT.ConvertStringToDecimal(calculator_srv.Text);
            sb.AppendLine(HtmlFormatParagraph(Align(("Received" + (new string(' ', 10)) + order.Ord_GotMoney), AlignEnum.right)));
            sb.AppendLine(HtmlFormatParagraph("Rest" + (new string(' ', 10)) + (order.Ord_GotMoney - order.Ord_PriceCost)));
            return sb;
        }
        private string HtmlFormatParagraph(string line)
        {
            return "<p style=\"font - family:Geneva; \">" + line + "</p>";
        }
        private string HtmlTableRow(params string[] line)
        {
            StringBuilder html = new StringBuilder();
            string tag = "th";
            html.AppendLine("<tr>");
            foreach (var item in line)
                html.AppendLine("<" + tag + ">" + item + "</" + tag + ">");
            html.AppendLine("</tr>");
            return html.ToString();
        }
        private string HtmlTableTitle(params string[] line)
        {
            StringBuilder html = new StringBuilder();
            string tag = "td";
            html.AppendLine("<tr>");
            foreach (var item in line)
                html.AppendLine("<" + tag + ">" + item + "</" + tag + ">");
            html.AppendLine("</tr>");
            return html.ToString();
        }
        private string StartHtml()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine("hr {");
            sb.AppendLine("border-style : dotted");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            return sb.ToString();
        }
        private string Align(string line, AlignEnum align)
        {
            return "<div align=\"" + align.ToString() + "\">" + line + "<div>";
        }
    }
    public enum AlignEnum { center, right, left }
}
