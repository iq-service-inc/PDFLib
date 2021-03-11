using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocWatermarkPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 12)
            {
                string[] data = args;
                string watermark = data[0] + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); 
                string filename = data[1];
                string outputName = data[2];
                string fontName = data[3];
                int fontSize = int.Parse(data[4]);
                int pX = int.Parse(data[5]);
                int pY = int.Parse(data[6]);
                int a = int.Parse(data[7]);
                int r = int.Parse(data[8]);
                int g = int.Parse(data[9]);
                int b = int.Parse(data[10]);
                int rotation = int.Parse(data[11]);

                DocWatermarkDLL.Wartemark.add(watermark, filename, outputName, fontName, fontSize, pX, pY, a, r, g, b, rotation);
            }
            else
            {
                //錯誤：參數不足，請確認
                
                //測試用
                string watermark = "網智服務" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string filename = "doc.pdf";
                string outputName = "out.pdf";
                DocWatermarkDLL.Wartemark.add(watermark, filename, outputName, a: 128, fontSize: 50, rotation: 180, pX: 0, pY: 0);
            }
        }

       
    }
}
