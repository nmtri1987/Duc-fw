using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Core.Models
{
    public class ExcelCellModel
    {
        public ExcelCellModel() {
            this.FontInfo = new FontCellModel();
            this.BackgroundColorInfo = new BackgroundColorModel();
        }
        public int RowInsert { get; set; }
        public int RowInsertNumber { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string CellValue { get; set; }

        public FontCellModel FontInfo { get; set; }

        public BackgroundColorModel BackgroundColorInfo { get; set; }
    }

    public class FontCellModel
    {
        public int FontSize { get; set; }
        public XLColor FontColor { get; set; }
        public bool IsBold{ get; set; }
    }
    public class BackgroundColorModel
    {
        public List<int> ColunmIndex { get; set; }
        public XLColor BackgorundColor { get; set; }
        public XLColor FontColor { get; set; }
    }

}
public enum ExcelCellStyle
{
    Datetime = 1,
    Decimal = 5
}