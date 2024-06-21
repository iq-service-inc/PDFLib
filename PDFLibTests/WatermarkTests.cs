using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFLib.Model;
using PdfSharp.Drawing;

namespace PDFLib.Tests
{
    [TestClass()]
    public class WatermarkTests
    {
        [TestMethod()]
        public void MarkTest()
        {
            Watermark wm = new Watermark(@"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\Output2.pdf")
            {
                // 輸入 PDF
                InputPath = @"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\01-未成年法定代理人同意書1100720.pdf",

                // 指定浮水印字型
                FontFamily = "Noto Sans TC",

                // 浮水印顏色
                Color = new ModelColor()
                {
                    R = 100,
                    G = 100,
                    B = 200,
                    A = 255
                },

                // 浮水印字體大小
                FontSize = 40,

                // 浮水印字體樣式
                FontWeight = XFontStyleEx.Bold | XFontStyleEx.Underline | XFontStyleEx.Italic,

                // 浮水印 水平 位置 (正中間為 0)
                PositionX = -100,
                // 浮水印 垂直 位置 (正中間為 0)
                PositionY = -100,

                // 浮水印旋轉角度
                Rotation = 45
            };

            // 浮水印文字 true 為加壓成功
            bool res = wm.Mark("PDF的浮水印");

            if (!res) Trace.WriteLine(wm.ErrorMessage);
            else Process.Start(wm.OutputPath);

            Assert.IsTrue(res);

        }

        [TestMethod()]
        public void MarkTest1()
        {
            string output = @"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\Output3.pdf";
            FileStream ms = new FileStream(output, FileMode.Create);
            Watermark wm = new Watermark(ms)
            {
                InputPath = @"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\01-未成年法定代理人同意書1100720.pdf"
            };

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
                Watermark wm = new Watermark(mms)
                {
                    InputPath = @"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\01-未成年法定代理人同意書1100720.pdf"
                };
                bool res = wm.Mark("PDF Memory Stream 的浮水印");

                if (!res) Trace.WriteLine(wm.ErrorMessage);
                else
                {
                    string output = @"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\Output4.pdf";
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