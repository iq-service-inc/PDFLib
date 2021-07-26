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

namespace PDFLib.Tests
{
    [TestClass()]
    public class WartemarkTests
    {
        [TestMethod()]
        public void MarkTest()
        {
            Wartemark wm = new Wartemark();
            // 輸入 PDF
            wm.InputPath = "Input.pdf";
            // 輸出位置
            wm.OutputPath = "Output.pdf";

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
            wm.FontWeight = XFontStyle.Regular;

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
    }
}