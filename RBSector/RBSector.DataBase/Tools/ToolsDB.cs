using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Tools
{
    public static class ToolsDB
    {
        public static string ConvertToString(this byte[] bytes)
        {
            string result = string.Empty;
            if(bytes==null || bytes.Count()<1) return string.Empty;
            foreach(var bt in bytes)
            {
                result += bt.ToString() + "\n";
            }
            return result;
        }
    }
}
