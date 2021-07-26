namespace PDFLib.Model
{
    /// <summary>
    /// 顏色專用資料模型
    /// https://docs.microsoft.com/zh-tw/dotnet/api/system.drawing.color.fromargb?view=net-5.0
    /// </summary>
    public class ModelColor
    {
        /// <summary>紅色比例 0~255 之間的數值</summary>
        public int R { get; set; }
        /// <summary>綠色比例 0~255 之間的數值</summary>
        public int G { get; set; }
        /// <summary>藍色比例 0~255 之間的數值</summary>
        public int B { get; set; }
        /// <summary>透明度 0~255 之間的數值 (0透明~255不透明)</summary>
        public int A { get; set; }
    }
}
