using Newtonsoft.Json;
using RBSector.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Tools
{
    public static class SendDataType
    {
        public static string ConvertToString(params object[] items)
        {
            if (items.Length <= 0) return string.Empty;
            string result = string.Empty;
            int length = items.Length;
            foreach (object item in items)
            {
                if (!string.IsNullOrEmpty(item.ToString()))
                {
                    if (String.IsNullOrEmpty(result))
                    {
                        result = DefaultType.START;
                    }
                    else
                    {
                        if (length != 0)
                            result += DefaultType.DIFFERENT_PART;
                    }
                    if (item != null)
                    {
                        result += item.ToString();
                        length--;
                    }
                }
                else
                {
                    length--;
                }
            }
            result += DefaultType.END;
            return result;
        }
        public static string JSonRecid(this List<string> recids)
        {
            if (recids == null || recids.Count <= 0) return string.Empty;
            string result = string.Empty;
            for (int i = 0; i < recids.Count; i++)
            {
                string format = recids[i];
                if (!string.IsNullOrEmpty(result)) result += ",";
                result += format;
            }
            return result;
        }
        public static string ComponetsList<T>(this IList<T> list, bool isMain = false)
        {
            if (list == null || list.Count <= 0) return string.Empty;
            bool isFirst = true;
            string result = isMain ? "{" : "";
            if (list.FirstOrDefault() is Tabs)
            {
                result += "\"Tabs\":[";
            }
            else if (list.FirstOrDefault() is Category)
            {
                result += "\"Categories\":[";
            }
            else if (list.FirstOrDefault() is Products)
            {
                result += "\"Products\":[";
            }
            else return string.Empty;
            foreach (var item in list)
            {
                if (!isFirst)
                {
                    result += ",";
                }
                else
                {
                    isFirst = false;
                }
                #region[Check all type]
                if (item is Tabs)
                {
                    result += (item as Tabs).SerializeWithComponents;
                }
                else if (item is Category)
                {
                    result += (item as Category).SerializeWithComponents;
                }
                else if (item is Products)
                {
                    result += (item as Products).SerializeWithComponents;
                }
                #endregion
            }
            result += "]" + (isMain ? "}" : "");
            return result;
        }
        public static string Componets<T>(this T obj, bool isMain = false)
        {
            string result = string.Empty;
            if (obj == null) return string.Empty;
            if (obj is Images)
            {
                result += "\"Image\":";
                var newObj = (obj as Images);
                Images im = new Images();
                im.ImName = newObj.ImName;
                im.ImRecid = newObj.ImRecid;
                im.ImByte = newObj.ImByte;
                im.ImType = newObj.ImType;
                result += JsonConvert.SerializeObject(im);
            }

            return result;
        }
    }
    public struct DefaultType
    {
        public static string START = "{";
        public static string END = "}";
        public static string DIFFERENT_PART = ",";
    }
}
