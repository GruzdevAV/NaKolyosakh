namespace DrivingSchoolWebAPI.DataSheetModels
{
    public struct Indices
    {
        /// <summary>
        /// Индекс строки. На 1 меньше числа в Excel
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// Индекс столбца. В Excel столбцы представляют собой буквы:
        /// A = 0
        /// B = 1
        /// Z = 25
        /// AA = 26
        /// </summary>
        public int Col { get; set; }
    }
}
