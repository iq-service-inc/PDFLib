using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PDFLib.Model;
using PdfSharp.Drawing;
using System.IO;

namespace PDFLib.Tests
{
    [TestClass()]
    public class WartemarkTests
    {
        [TestMethod()]
        public void MarkTest()
        {
            Wartemark wm = new Wartemark("Output.pdf");
            // 輸入 PDF
            wm.InputPath = "Input.pdf";


            // 指定浮水印字型
            wm.FontFamily = "Noto Sans TC";

            // 浮水印顏色
            wm.Color = new ModelColor()
            {
                R = 100,
                G = 100,
                B = 200,
                A = 255
            };

            // 浮水印字體大小
            wm.FontSize = 40;

            // 浮水印字體樣式
            wm.FontWeight = XFontStyle.Bold | XFontStyle.Underline | XFontStyle.Italic;

            // 浮水印 水平 位置 (正中間為 0)
            wm.PositionX = -100;
            // 浮水印 垂直 位置 (正中間為 0)
            wm.PositionY = -100;

            // 浮水印旋轉角度
            wm.Rotation = 45;

            // 浮水印文字 true 為加壓成功
            bool res = wm.Mark("PDF的浮水印");

            if (!res) Trace.WriteLine(wm.ErrorMessage);
            else Process.Start(wm.OutputPath);

            Assert.IsTrue(res);

        }

        [TestMethod()]
        public void MarkTest1()
        {
            string output = "Output.pdf";
            FileStream ms = new FileStream(output, FileMode.Create);
            Wartemark wm = new Wartemark(ms);
            wm.InputPath = "Input.pdf";

            // 浮水印文字 true 為加壓成功
            bool res = wm.Mark("PDF Stream的浮水印");


            if (!res) Trace.WriteLine(wm.ErrorMessage);
            else
            {
                Process.Start(output);
                ms.Dispose();
            }
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void MarkTest2()
        {
            using (MemoryStream mms = new MemoryStream())
            {
                Wartemark wm = new Wartemark(mms);
                wm.InputPath = "Input.pdf";
                bool res = wm.Mark("PDF Memory Stream 的浮水印");

                if (!res) Trace.WriteLine(wm.ErrorMessage);
                else
                {
                    string output = "Output3.pdf";
                    using (FileStream ms = new FileStream(output, FileMode.Create))
                    {
                        mms.CopyTo(ms);
                    }
                    Process.Start(output);
                }
                Assert.IsTrue(res);
            }
        }
    }
}