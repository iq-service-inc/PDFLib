using Microsoft.VisualStudio.TestTools.UnitTesting;
using PDFLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFLib.Tests
{
    [TestClass()]
    public class PDFPasswordTests
    {
        [TestMethod()]
        public void ApplyTest()
        {
            // 要加密碼的 PDF
            PDFPassword pp = new PDFPassword("Output.pdf");
            // 加上密碼與輸出的路徑 (true 表示成功)
            var res = pp.ApplyPassword("123", "Pwd.pdf");
            if (!res) Trace.WriteLine(pp.ErrorMessage);
            else Process.Start("Pwd.pdf");
            Assert.IsTrue(res);
            
        }
    }
}