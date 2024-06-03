using Google.Apis.Sheets.v4.Data;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    /// <summary>
    /// Класс для помощи с работой с Google-таблицей
    /// </summary>
    public class GoogleSheetHelper
    {
        public Sheet Sheet { get; set; }
        public IList<GridRange> Mergers { get => Sheet.Merges; }
        public GridData Data { get => Sheet.Data[0]; }
        public GoogleSheetHelper(Sheet sheet)
        {
            Sheet = sheet;
        }
        /// <summary>
        /// Если indices указывает на ячейку, входящую в объекдинение, то верутся индексы первой ячейки
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        public Indices GetTrueIndices(Indices indices)
        {
            return GetTrueIndices(indices.Row,indices.Col);
        }
        /// <summary>
        /// Если row и col указывают на ячейку, входящую в объекдинение, то верутся индексы первой ячейки этого объединения
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Indices GetTrueIndices(int row, int col)
        {
            var t = Mergers.Where(x =>
                x.StartRowIndex <= row && x.EndRowIndex > row && x.StartColumnIndex <= col && x.EndColumnIndex > col).FirstOrDefault();
            if (t != null)
            {
                row = t.StartRowIndex ?? -1;
                col = t.StartColumnIndex ?? -1;
            }
            return new Indices() { Row = row, Col = col };
        }
        /// <summary>
        /// Получить ячейку, стоящую в строке row и столбце col
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public CellData this[int row, int col]
        {
            get
            {
                var t = GetTrueIndices(row, col);
                return Data.RowData[t.Row].Values[t.Col];
            }
        }
        /// <summary>
        /// Получить ячейку, стоящую по индексу range (если это диапазон, то вернётся первая ячейка)
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public CellData this[string range]
        {
            get
            {
                var selectedRange = Methods.ExcelIndexToIndices(range.Split(':').First());
                return this[selectedRange.Row, selectedRange.Col];
            }
        }
    }
}
