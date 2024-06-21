using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.IO;
using SharpFont;

namespace PDFLib
{
    public class CustomFontResolver : IFontResolver
    {
        private readonly Dictionary<string, string> fontFamilies = new Dictionary<string, string>();

        public CustomFontResolver()
        {
            LoadFontsFromDirectory(@"C:\Windows\Fonts");
            LoadFontsFromDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\Fonts"));

            if (fontFamilies.Count == 0)
            {
                throw new FileNotFoundException($"No font files found in the system font directories.");
            }
        }

        private void LoadFontsFromDirectory(string fontDirectory)
        {
            if (Directory.Exists(fontDirectory))
            {
                Console.WriteLine($"Font directory found: {fontDirectory}");

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
                Console.WriteLine($"Font directory not found: {fontDirectory}");
            }
        }

        private void AddFont(string fontPath)
        {
            try
            {
                using (Library fontCollection = new Library())
                {
                    Face face = new Face(fontCollection, fontPath);
                    string fontName = face.FamilyName;
                    if (!fontFamilies.ContainsKey(fontName))
                    {
                        fontFamilies.Add(fontName, fontPath);
                        Console.WriteLine($"Loaded font: {fontName} from {fontPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load font from {fontPath}: {ex.Message}");
            }
        }

        public byte[] GetFont(string faceName)
        {
            if (fontFamilies.TryGetValue(faceName, out string fontPath))
            {
                return File.ReadAllBytes(fontPath);
            }
            throw new ArgumentException($"Font '{faceName}' is not available.");
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            Console.WriteLine($"Resolving typeface for: {familyName}, Bold: {isBold}, Italic: {isItalic}");

            if (fontFamilies.ContainsKey(familyName))
            {
                return new FontResolverInfo(familyName);
            }

            Console.WriteLine($"Font family '{familyName}' not found.");
            return null;
        }
    }
}
