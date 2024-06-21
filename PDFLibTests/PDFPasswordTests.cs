using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace PDFLib.Tests
{
    [TestClass()]
    public class PDFPasswordTests
    {
        [TestMethod()]
        public void ApplyPasswordTest()
        {
            // 要加密碼的 PDF
            PDFPassword pp = new PDFPassword(@"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\01-未成年法定代理人同意書1100720.pdf");
            // 加上密碼與輸出的路徑 (true 表示成功)
            bool res = pp.ApplyPassword("123", @"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\Output1.pdf");
            if (!res) Trace.WriteLine(pp.ErrorMessage);
            else Process.Start(@"D:\網智\OneDrive - 網智服務國際股份有限公司\桌面\Output1.pdf");
            Assert.IsTrue(res);
        }
    }
}