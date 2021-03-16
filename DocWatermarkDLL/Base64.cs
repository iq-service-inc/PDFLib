using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocWatermarkDLL
{
    public class Base64
    {
        public static string output(string filename)
        {
            byte[] data = File.ReadAllBytes(filename);
            return Convert.ToBase64String(data);
        }
    }
}
