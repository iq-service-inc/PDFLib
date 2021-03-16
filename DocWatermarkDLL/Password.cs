using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocWatermarkDLL
{
    public class Password
    {
        /// <summary>
        /// 文件加密
        /// </summary>
        /// <param name="password">欲設密碼</param>
        /// <param name="filename">原始檔案位置，含檔名</param>
        /// <param name="outputName">儲存檔案位置，含檔名</param>
        public static void add(string password, string filename,string outputName)
        {
            //判別檔案是否存在
            if (!File.Exists(filename))
                return;
            //輸出目錄是否存在
            string outputDir = Path.GetDirectoryName(outputName);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            //開啟文件
            var document = PdfReader.Open(outputName);
            //設置密碼
            var securitySettings = document.SecuritySettings;
            // PdfDocumentSecurityLevel.Encrypted128Bit. 
            securitySettings.UserPassword = password;
            //securitySettings.OwnerPassword = "owner";

            // Don't use 40 bit encryption unless needed for compatibility.
            //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

            securitySettings.PermitAccessibilityExtractContent = false;
            securitySettings.PermitAnnotations = false;
            securitySettings.PermitAssembleDocument = false;
            securitySettings.PermitExtractContent = false;
            securitySettings.PermitFormsFill = true;
            securitySettings.PermitFullQualityPrint = false;
            securitySettings.PermitModifyDocument = true;
            securitySettings.PermitPrint = false;

            // 儲存文件
            document.Save(outputName);
        }
    }
}
