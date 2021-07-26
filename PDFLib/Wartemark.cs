using PDFLib.Model;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace PDFLib
{
    /// <summary>
    /// 加壓浮水印功能類別
    /// </summary>
    public class Wartemark
    {
        /// <summary>
        /// 檔案輸入路徑
        /// </summary>
        public string InputPath { get; set; }
        /// <summary>
        /// 檔案輸出路徑
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// 文字字型
        /// </summary>
        public string FontFamily { get; set; } = "Times New Roman";
        /// <summary>
        /// 字體大小
        /// </summary>
        public int FontSize { get; set; } = 20;

        /// <summary>
        /// 字體粗細
        /// </summary>
        public XFontStyle FontWeight { get; set; } = XFontStyle.Regular;

        /// <summary>
        /// 文字 X 位置
        /// </summary>
        public double PositionX { get; set; }
        /// <summary>
        /// 文字 Y 位置
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// 旋轉角度
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// 文字顏色設定
        /// </summary>
        public ModelColor Color { get; set; } = new ModelColor();

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="input_path">檔案輸入路徑</param>
        /// <param name="output_path">檔案輸出路徑</param>
        public Wartemark(string input_path, string output_path)
        {
            InputPath = input_path;
            OutputPath = output_path;
            Color = new ModelColor();
        }

        /// <summary>
        /// 建構子
        /// </summary>
        public Wartemark()
        {
            Color = new ModelColor();
        }


        /// <summary>
        /// 依照浮水印的參數設定加壓浮水印
        /// </summary>
        /// <param name="words">浮水印文字</param>
        /// <returns>是否加壓成功</returns>
        public bool Mark(string words)
        {
            //判別檔案是否存在
            if (!File.Exists(InputPath)) return false;

            try
            {
                //輸出目錄是否存在
                string outputDir = Path.GetDirectoryName(OutputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

                //將原始檔案複製一份
                File.Copy(InputPath, OutputPath, true);

                //無法讀取非ttf之字型，目前中文先只能使用標楷體。後面再試試ttc字型
                //System.Drawing.Text.PrivateFontCollection pfcFonts = new System.Drawing.Text.PrivateFontCollection();
                //string strFontPath = @"C:/Windows/Fonts/kaiu.ttf";
                //pfcFonts.AddFontFile(strFontPath);
                //XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
                //XFont font = new XFont(pfcFonts.Families.First(), 150, XFontStyle.Regular, options);

                //fontName = isNatural_Number(watermark) ? fontName : "標楷體";
                //設定型、大小、是否粗體
                //var options = new XPdfFontOptions(PdfFontEmbedding.Always);
                //XPdfFontOptions op = new XPdfFontOptions(PdfFontEncoding.Unicode,PdfFontEmbedding.Always);
                //System.Drawing.Text.PrivateFontCollection pfcFonts = new System.Drawing.Text.PrivateFontCollection();
                //string strFontPath = @"C:\Users\demon\AppData\Local\Microsoft\Windows\Fonts\NotoSansTC-Regular.otf";
                //pfcFonts.AddFontFile(strFontPath);

                var font = new XFont(FontFamily, FontSize, FontWeight);

                //開啟文件
                using (var document = PdfReader.Open(OutputPath))
                {

                    //設定 version to PDF 1.4 (Acrobat 5)，因為有設定透明度.
                    if (document.Version < 14) document.Version = 14;



                    //將浮水印放至每頁
                    for (var idx = 0; idx < document.Pages.Count; idx++)
                    {
                        var page = document.Pages[idx];
                        // 將文字當作浮水印塞入
                        // Append-每頁的最上面
                        var gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);



                        // 取得浮水印的大小
                        var size = gfx.MeasureString(words, font);
                        // 設定浮水印角度
                        XUnit width = page.Width;
                        XUnit height = page.Height;

                        // 計算加印位置
                        var x = page.Width / 2 - size.Width/2 + PositionX; 
                        var y = page.Height / 2 - size.Height / 2 + PositionY;


                        //gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                        gfx.RotateAtTransform(Rotation, new XPoint(x + size.Width / 2, y + size.Height / 2));
                        //gfx.RotateTransform(-Math.Atan(size.Height / size.Width) * Rotation / Math.PI,);
                        //gfx.TranslateTransform(page.Width/4, -page.Height/4);

                        var format = new XStringFormat();
                        format.LineAlignment = XLineAlignment.Near;
                        format.Alignment = XStringAlignment.Near;
                        // 設定浮水印顏色、透明度
                        XBrush brush = new XSolidBrush(XColor.FromArgb(Color.A, Color.R, Color.G, Color.B));

                        // 加壓至PDF頁面
                        gfx.DrawString(words, font, brush, new XPoint(x, y), format);
                    }
                    // 儲存
                    document.Save(OutputPath);
                    // 測試時，開啟
                    //Process.Start(OutputPath);
                    document.Dispose();
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
                return false;
            }

            return true;

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
