using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;

namespace PDFLib
{
    /// <summary>
    /// 自定義字體解析器，實現 IPdfSharp 的 IFontResolver 接口
    /// </summary>
    public class CustomFontResolver : IFontResolver
    {
        // 存儲字體名稱和對應的文件路徑
        private readonly Dictionary<string, string> fontFamilies = new Dictionary<string, string>();
        public string ErrorMessage { get; private set; }
        private readonly string fallbackFontPath;

        /// <summary>
        /// 建構子
        /// </summary>
        public CustomFontResolver()
        {
            string projectFontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "font");
            fallbackFontPath = Path.Combine(projectFontPath, "DFKai-SB Regular.ttf");

            // 檢查並創建字體文件
            EnsureFallbackFontExists();

            LoadFontsFromDirectory(@"C:\Windows\Fonts");
            LoadFontsFromDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\Fonts"));

            // 如果沒有找到任何系統字體，預設加載標楷體
            if (fontFamilies.Count == 0)
            {
                AddFont(fallbackFontPath);
            }
            else if (!fontFamilies.ContainsKey("標楷體"))
            {
                AddFont(fallbackFontPath);
            }
        }

        /// <summary>
        /// 確保標楷體字體文件存在，如果不存在則創建
        /// </summary>
        private void EnsureFallbackFontExists()
        {
            if (!File.Exists(fallbackFontPath))
            {
                try
                {
                    System.Reflection.Assembly assembly = typeof(CustomFontResolver).Assembly;
                    using (Stream stream = assembly.GetManifestResourceStream("PDFLib.font.DFKai-SB Regular.ttf"))
                    {
                        if (stream == null)
                        {
                            ErrorMessage = "Embedded font 'DFKai-SB Regular.ttf' not found.";
                            return;
                        }

                        Directory.CreateDirectory(Path.GetDirectoryName(fallbackFontPath));

                        using (FileStream fileStream = new FileStream(fallbackFontPath, FileMode.Create, FileAccess.Write))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"Failed to create fallback font file. Error: {ex.Message}";
                }
            }
        }

        /// <summary>
        /// 讀取系統字體
        /// </summary>
        /// <param name="fontDirectory">儲存字體的路徑</param>
        private void LoadFontsFromDirectory(string fontDirectory)
        {
            if (Directory.Exists(fontDirectory))
            {
                foreach (string file in Directory.GetFiles(fontDirectory, "*.otf"))
                {
                    AddFont(file);
                }

                foreach (string file in Directory.GetFiles(fontDirectory, "*.ttf"))
                {
                    AddFont(file);
                }
            }
            else
            {
                ErrorMessage = $"Font directory not found: {fontDirectory}";
            }
        }

        /// <summary>
        /// 添加字體到字典中
        /// </summary>
        /// <param name="fontPath">字體文件的路徑</param>
        private void AddFont(string fontPath)
        {
            try
            {
                using (PrivateFontCollection fontCollection = new PrivateFontCollection())
                {
                    fontCollection.AddFontFile(fontPath);

                    string fontFamily = fontCollection.Families[0].Name;

                    if (!fontFamilies.ContainsKey(fontFamily))
                    {
                        fontFamilies.Add(fontFamily, fontPath);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to add font from path: {fontPath}. Error: {ex.Message}";
            }
        }

        /// <summary>
        /// 根據字體名稱獲取字體文件的字節數組
        /// </summary>
        /// <param name="faceName">字體名稱</param>
        /// <returns></returns>
        public byte[] GetFont(string faceName)
        {
            if (fontFamilies.TryGetValue(faceName, out string fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }

            ErrorMessage = $"Font '{faceName}' is not available.";
            return null;
        }

        /// <summary>
        /// 解析特定樣式的字體類型
        /// </summary>
        /// <param name="familyName">字體FamilyName</param>
        /// <param name="isBold">字體粗度</param>
        /// <param name="isItalic">字體斜體</param>
        /// <returns></returns>
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (fontFamilies.ContainsKey(familyName))
            {
                return new FontResolverInfo(familyName);
            }

            ErrorMessage = $"Font family '{familyName}' not found.";
            return null;
        }
    }
}
