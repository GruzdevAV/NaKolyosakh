using Google.Apis.Sheets.v4.Data;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    public interface IGoogleSheetsManager
    {
        /// <summary>
        /// Создать новую Google-таблицу
        /// </summary>
        /// <param name="documentName"></param>
        /// <returns></returns>
        Spreadsheet CreateNew(string documentName);
        /// <summary>
        /// Получить существующую Google-таблицу
        /// </summary>
        /// <param name="googleSpreadsheetIdentificator"></param>
        /// <returns></returns>
        Spreadsheet GetSpreadsheet(string googleSpreadsheetIdentificator);
        /// <summary>
        /// Получить значение из существующей Google-таблицы
        /// </summary>
        /// <param name="googleSpreadsheetIdentificator"></param>
        /// <param name="valueRange"></param>
        /// <returns></returns>
        ValueRange GetSingleValue(string googleSpreadsheetIdentificator, string valueRange);
        /// <summary>
        /// Удалить значение из существующей Google-таблицы
        /// </summary>
        /// <param name="googleSpreadsheetIdentificator"></param>
        /// <param name="valueRange"></param>
        /// <returns></returns>
        ClearValuesResponse RemoveSingleValue(string googleSpreadsheetIdentificator, string valueRange);
        BatchGetValuesResponse GetMultipleValues(string googleSpreadsheetIdentificator, string[]ranges);
        BatchClearValuesResponse ClearMultipleValues(string googleSpreadsheetIdentificator, string[] ranges);
        BatchUpdateSpreadsheetResponse UpdateValueAndBackgroundColor(string googleSpreadsheetIdentificator, string valueRange, string value, Color backgroundColor, GridRange gridRange);
    }
}
