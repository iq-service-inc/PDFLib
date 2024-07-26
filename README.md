# [PDFLib](https://www.nuget.org/packages/PDFLib)

此專案基於 [PDFsharp](http://www.pdfsharp.net/) 函式庫進行擴充，簡化複雜的呼叫方式，用於處理 PDF 相關功能的函式庫，支援浮水印與密碼加密功能


## Installation

**Package Manager (v2.0.2)**

NuGet：[PDFLib](https://www.nuget.org/packages/PDFLib)

```
PM> Install-Package PDFLib -Version 2.0.2
```

## System Requirement

* .NET Framework 4.7.2 以上

## Usage

以下展示基本的使用範例

### 加壓浮水印

以下範例將對 `Input.pdf` PDF 加壓 `PDF的浮水印` 文字，並指定字型 `Noto Sans TC`，顏色為 `rgba(100,100,100,255)`，字體大小 `40px`，樣式 `Regular`， 水平垂直位置為 `(-100px,-100px)` 並旋轉 `45` 度，最後輸出到 `Output.pdf`

#### 👨‍💻 Code

```csharp
// 輸出位置 (可為絕對路徑)
Watermark wm = new Watermark("Output.pdf");
// 輸入 PDF (可為絕對路徑)
wm.InputPath = "Input.pdf";
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


#### 使用串流輸出加壓浮水印的檔案

將 PDF 檔案加壓浮水印之後，寫入記憶體串流中，進行後續處理

```csharp
// 建立記憶體串流
using (MemoryStream mms = new MemoryStream())
{
    // 將記憶體串流輸入給浮水印
    Watermark wm = new Watermark(mms);
    // 設定 PDF 輸入檔案
    wm.InputPath = "Input.pdf";
    // 設定顯示字串，並加壓浮水印，如果為 true，則加壓好的浮水印就已經寫入在記憶體串流中
    bool res = wm.Mark("PDF Memory Stream 的浮水印");

    if (!res) Trace.WriteLine(wm.ErrorMessage);
    else
    {
        // 將記憶體串流寫出到實體檔案 (非必要)
        string output = "Output.pdf";
        using (FileStream ms = new FileStream(output, FileMode.Create))
        {
            mms.CopyTo(ms);
        }
    }
}
```

#### 💠 浮水印位置與旋轉示意圖

以下說明 `PositionX` , `PositionY` 與 `Rotation` 參數的的效果關係

![Imgur](https://i.imgur.com/XHSHUI6.png)

#### ⚠️ 字型設置

請注意 [PDFSharp](http://www.pdfsharp.net/) 中文部分，僅內建支援 `標楷體`，其餘字型需要先在 Server Side 進行[安裝](https://support.microsoft.com/zh-tw/office/%E6%96%B0%E5%A2%9E%E5%AD%97%E5%9E%8B-b7c5f17c-4426-4b53-967f-455339c564c1) 並且只支援 [`otf`](https://zh.wikipedia.org/zh-tw/OpenType) 字型格式

![Font](https://i.imgur.com/pr52JVQ.png)


**字型名稱**，請確保輸入的是字型檔案打開後顯示的 `字型名稱`，才可被正確指定

![FN](https://i.imgur.com/QNTH6Oa.png)

> 如果需要擴充字型，可自行安裝，例如：[Google Fonts](https://fonts.google.com/)

**如果未指定字體，將預設使用`標楷體`。若使用者指定的字體在系統中未安裝，則會輸出「找不到此字體」的錯誤訊息**
![錯誤訊息](https://imgur.com/Xxq6X9W.png)

### PDF 加上密碼

```csharp
// 要加密碼的 PDF (可為絕對路徑)
PDFPassword pp = new PDFPassword("Output.pdf");
// 加上密碼與輸出的路徑 (true 表示成功)
var res = pp.ApplyPassword("123", "Pwd.pdf");
```

![PWD](https://i.imgur.com/Z90TD4g.png)


## License MIT

	Copyright (C) 2022 ZapLin
	Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
	The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


## PDFSharp License

[PDFsharp_License](http://www.pdfsharp.net/PDFsharp_License.ashx)