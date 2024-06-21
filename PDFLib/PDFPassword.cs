using System;
using System.IO;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;

namespace PDFLib
{
    /// <summary>
    /// PDF 加密專用 class
    /// </summary>
    public class PDFPassword
    {
        private string filename;
        private string output_path = "output.pdf";
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="input_path">PDF 檔案路徑</param>
        public PDFPassword(string input_path)
        {
            filename = input_path;
        }

        /// <summary>
        /// 取得輸出的 PDF 路徑
        /// </summary>
        /// <returns>輸出的 PDF 絕對路徑</returns>
        public string GetOutputPath()
        {
            try
            {
                return Path.GetFullPath(output_path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 文件加密
        /// </summary>
        /// <param name="password">欲設密碼</param>
        /// <param name="output_path">儲存檔案位置，含檔名</param>
        public bool ApplyPassword(string password, string output_path)
        {
            if (!string.IsNullOrWhiteSpace(output_path)) this.output_path = output_path;

            //判別檔案是否存在
            if (!File.Exists(filename)) return false;

            //輸出目錄是否存在
            string outputDir = Path.GetDirectoryName(this.output_path);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            try
            {
                File.Copy(filename, this.output_path, true);

                //開啟文件
                using (PdfSharp.Pdf.PdfDocument document = PdfReader.Open(this.output_path, PdfDocumentOpenMode.Modify))
                {
                    //設置密碼
                    PdfSecuritySettings securitySettings = document.SecuritySettings;
                    //PdfDocumentSecurityLevel.Encrypted128Bit. 
                    securitySettings.UserPassword = password;
                    //securitySettings.OwnerPassword = "owner";

                    // Don't use 40 bit encryption unless needed for compatibility.
                    securitySettings.PermitAnnotations = false;
                    securitySettings.PermitAssembleDocument = false;
                    securitySettings.PermitExtractContent = false;
                    securitySettings.PermitFormsFill = true;
                    securitySettings.PermitFullQualityPrint = false;
                    securitySettings.PermitModifyDocument = true;
                    securitySettings.PermitPrint = false;

                    // 儲存文件
                    document.Save(this.output_path);
                }

                return true;
            }
            catch (Exception e)
            {

                ErrorMessage = e.ToString();
                return false;
            }
        }
    }
}

