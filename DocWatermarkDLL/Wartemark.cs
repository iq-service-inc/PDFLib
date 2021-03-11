using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocWatermarkDLL
{
    public class Wartemark
    {
        /// <summary>
        /// PDf增加浮水印
        /// </summary>
        /// <param name="watermark">浮水印呈現之文字</param>
        /// <param name="filename">原始PDF檔案位置，包含檔名</param>
        /// <param name="outputName">輸出PDF檔案位置，包含檔名</param>
        /// <param name="fontName">字體名稱(測試階段預設文字中只有英數為Times New Roman，有中文則預設標楷體)</param>
        /// <param name="fontSize">浮水印大小，預設150</param>
        /// <param name="pX">浮水印位置X，建議為0，系統自動抓取，大於0值則會設定該位置</param>
        /// <param name="pY">浮水印位置X，建議為0，系統自動抓取，大於0值則會設定該位置</param>
        /// <param name="a">浮水印透明度，0~255，255則無透明，預設128</param>
        /// <param name="r">浮水印顏色R，預設255紅色</param>
        /// <param name="g">浮水印顏色G，預設0</param>
        /// <param name="b">浮水印顏色B，預設0</param>
        /// <param name="rotation">浮水印角度，預設為180(45度)，0為平的</param>
        public static void add(string watermark, string filename, string outputName, string fontName = "Times New Roman", int fontSize = 150, int pX = 0, int pY = 0, int a = 128, int r = 255, int g = 0, int b = 0, int rotation = 0)
        {
            //判別檔案是否存在
            if (!File.Exists(filename))
                return;
            //輸出目錄是否存在
            string outputDir = Path.GetDirectoryName(outputName);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            //將原始檔案複製一份
            File.Copy(filename, outputName, true);

            //無法讀取非ttf之字型，目前中文先只能使用標楷體。後面再試試ttc字型
            //System.Drawing.Text.PrivateFontCollection pfcFonts = new System.Drawing.Text.PrivateFontCollection();
            //string strFontPath = @"C:/Windows/Fonts/kaiu.ttf";
            //pfcFonts.AddFontFile(strFontPath);
            //XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            //XFont font = new XFont(pfcFonts.Families.First(), 150, XFontStyle.Regular, options);

            fontName = isNatural_Number(watermark) ? fontName : "標楷體";

            //設定型、大小、是否粗體
            var font = new XFont(fontName, fontSize, XFontStyle.Bold);

            //開啟文件
            var document = PdfReader.Open(outputName);

            //設定 version to PDF 1.4 (Acrobat 5)，因為有設定透明度.
            if (document.Version < 14)
                document.Version = 14;

            //將浮水印放至每頁
            for (var idx = 0; idx < document.Pages.Count; idx++)
            {
                var page = document.Pages[idx];
                // 將文字當作浮水印塞入
                // Append-每頁的最上面
                var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);
                // 取得浮水印的大小
                var size = gfx.MeasureString(watermark, font);
                // 設定浮水印角度
                XUnit width = page.Width;
                XUnit height = page.Height;
                gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * rotation / Math.PI);
                gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

                var format = new XStringFormat();
                format.LineAlignment = XLineAlignment.Near;
                format.Alignment = XStringAlignment.Near;
                // 設定浮水印顏色、透明度
                XBrush brush = new XSolidBrush(XColor.FromArgb(a, r, g, b));

                // 加壓至PDF頁面
                gfx.DrawString(watermark, font, brush,
                    new XPoint((pX == 0 ? (page.Width - size.Width) / 2 : pX),
                               (pY == 0 ? (page.Height - size.Height) / 2 : pY)), format);
            }
            // 儲存
            document.Save(outputName);
            document.Dispose();
            // 測試時，開啟
            Process.Start(outputName);
        }

        #region 判斷傳入的字符是否是英文字母或數字
        /// <summary>
        /// 判斷傳入的字符是否是英文字母或數字
        /// </summary>
        /// <param name="str">字串</param>
        /// <returns>bool</returns>
        private static bool isNatural_Number(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg1.IsMatch(str);
        }
        #endregion

    }
}
