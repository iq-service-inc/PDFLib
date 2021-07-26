# PDFLib

此專案基於 [DocWatermarkPDF](http://10.190.173.136/DevelopmentSupport/DocWatermarkPDF) 修改，專門用於處理 PDF 相關功能的函式庫，支援浮水印與密碼加密功能


## Installation

**Package Manager (v1.0.0)**

```
PM> Install-Package PDFLib -Version 1.0.0
```

## System Requirement

* .NET Framework 4.5 以上

## Usage

以下展示基本的使用範例，進階使用範例請參考文件

### 加壓浮水印

```csharp
Wartemark wm = new Wartemark();
// 輸入 PDF (可為絕對路徑)
wm.InputPath = "Input.pdf";
// 輸出位置 (可為絕對路徑)
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
```
### PDF 加上密碼

```csharp
// 要加密碼的 PDF (可為絕對路徑)
PDFPassword pp = new PDFPassword("Output.pdf");
// 加上密碼與輸出的路徑 (true 表示成功)
var res = pp.ApplyPassword("123", "Pwd.pdf");
```


## 相關文件

[浮水印轉檔交接.txt](https://iqservice.sharepoint.com/:t:/s/DevSupport/ETsZPU30ntlPi0pISbvbDGkB5kVNL89SeQL2-OzMKBGlqQ?e=CzQ6QU)

## License MIT

	Copyright (C) 2021 ZapLin
	Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
	The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


## PDFSharp License

[PDFsharp_License](http://www.pdfsharp.net/PDFsharp_License.ashx)
 
