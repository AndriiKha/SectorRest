using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Tools
{
    public class ImageTools
    {
        public static byte[] ImageToByte(string path)
        {
            byte[] image = default(byte[]);
            if (string.IsNullOrEmpty(path)) return image;
            try {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    image = reader.ReadBytes((int)stream.Length);
                    reader.Close();
                    stream.Close();
                }
            }catch(Exception ex) { string error = ex.Message; }
            return image;
        }
    }
}
