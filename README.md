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

以下展示基本的使用範例

### 加壓浮水印

以下範例將對 `Input.pdf` PDF 加壓 `PDF的浮水印` 文字，並指定字型 `Noto Sans TC`，顏色為 `rgba(100,100,100,255)`，字體大小 `40px`，樣式 `Regular`， 水平垂直位置為 `(-100px,-100px)` 並旋轉 `45` 度，最後輸出到 `Output.pdf`

#### 👨‍💻 Code

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
#### 💠 浮水印位置與旋轉示意圖

以下說明 `PositionX` , `PositionY` 與 `Rotation` 參數的的效果關係

![wm](http://10.190.173.136/uploads/-/system/temp/956d2afc87e8a6c4ac8a09023a27569d/image.png)

#### ⚠️ 字型設置

請注意 [PDFSharp](http://www.pdfsharp.net/) 中文部分，僅內建支援 `DFKai-SB` (標楷體)，其餘字型需要先在 Server Side 進行[安裝](https://support.microsoft.com/zh-tw/office/%E6%96%B0%E5%A2%9E%E5%AD%97%E5%9E%8B-b7c5f17c-4426-4b53-967f-455339c564c1) 並且只支援 [`otf`](https://zh.wikipedia.org/zh-tw/OpenType) 字型格式

![Font](http://10.190.173.136/uploads/-/system/personal_snippet/86/0f69a9637ea9902cf80df1c88998a3f0/image.png)


**字型名稱**，請確保輸入的是字型檔案打開後顯示的 `字型名稱`，才可被正確指定

![FN](http://10.190.173.136/uploads/-/system/personal_snippet/86/c4bb924e425a241d2ccb18a1f3d8f6e8/image.png)

> 如果需要擴充字型，可自行安裝，例如：[Google Fonts](https://fonts.google.com/)

### PDF 加上密碼

```csharp
// 要加密碼的 PDF (可為絕對路徑)
PDFPassword pp = new PDFPassword("Output.pdf");
// 加上密碼與輸出的路徑 (true 表示成功)
var res = pp.ApplyPassword("123", "Pwd.pdf");
```

![PWD](http://10.190.173.136/uploads/-/system/personal_snippet/86/2887d2cf05aad2eb721f66d2491ad602/image.png)

## 相關文件

[浮水印轉檔交接.txt](https://iqservice.sharepoint.com/:t:/s/DevSupport/ETsZPU30ntlPi0pISbvbDGkB5kVNL89SeQL2-OzMKBGlqQ?e=CzQ6QU)

## License MIT

	Copyright (C) 2021 ZapLin
	Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
	The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


## PDFSharp License

[PDFsharp_License](http://www.pdfsharp.net/PDFsharp_License.ashx)
 
