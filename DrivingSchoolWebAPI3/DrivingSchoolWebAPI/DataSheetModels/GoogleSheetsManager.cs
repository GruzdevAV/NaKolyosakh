using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    public class GoogleSheetsManager : IGoogleSheetsManager
    {
        public readonly UserCredential _credential;
        public GoogleSheetsManager(UserCredential credential)
        {
            _credential = credential;
        }

        public Spreadsheet CreateNew(string documentName)
        {
            if (string.IsNullOrEmpty(documentName))
            {
                throw new ArgumentNullException(nameof(documentName));
            }
            using (var sheetsService = new SheetsService(new BaseClientService.Initializer()
            { HttpClientInitializer = _credential }))
            {
                var documentCreationRequest = sheetsService.Spreadsheets.Create(new Spreadsheet()
                {
                    Sheets = new List<Sheet>()
                    {
                        new Sheet()
                        {
                            Properties = new SheetProperties()
                            {
                                Title = documentName,
                            }
                        }
                    },
                    Properties = new SpreadsheetProperties()
                    {
                        Title = documentName,
                    }

                });
                return documentCreationRequest.Execute();
            }
        }

        public BatchGetValuesResponse GetMultipleValues(string googleSpreadsheetIdentificator, string[] ranges)
        {
            if (string.IsNullOrEmpty(googleSpreadsheetIdentificator))
                throw new ArgumentNullException(nameof(googleSpreadsheetIdentificator));
            if (ranges == null || ranges.Length == 0)
                throw new ArgumentNullException(nameof(ranges));
            using (var sheetsService = new SheetsService(new BaseClientService.Initializer()
            { HttpClientInitializer = _credential }))
            {

                var getMultipleValuesRequest = sheetsService.Spreadsheets.Values.BatchGet(googleSpreadsheetIdentificator);
                getMultipleValuesRequest.Ranges = ranges;
                return getMultipleValuesRequest.Execute();
            }
        }

        public ValueRange GetSingleValue(string googleSpreadsheetIdentificator, string valueRange)
        {
            if (string.IsNullOrEmpty(googleSpreadsheetIdentificator))
                throw new ArgumentNullException(nameof(googleSpreadsheetIdentificator));
            if (string.IsNullOrEmpty(valueRange))
                throw new ArgumentNullException(nameof(valueRange));
            using (var sheetsService = new SheetsService(new BaseClientService.Initializer()
            { HttpClientInitializer = _credential }))
            {
                var getValueRequest = sheetsService.Spreadsheets.Values.Get(googleSpreadsheetIdentificator, valueRange);
                return getValueRequest.Execute();
            }
        }

        public Spreadsheet GetSpreadsheet(string googleSpreadsheetIdentificator)
        {
            if (string.IsNullOrEmpty(googleSpreadsheetIdentificator))
                throw new ArgumentNullException(nameof(googleSpreadsheetIdentificator));
            using (var sheetsService = new SheetsService(new BaseClientService.Initializer()
            { HttpClientInitializer = _credential }))
            {
                SpreadsheetsResource.GetRequest getSheetRequest = sheetsService.Spreadsheets.Get(googleSpreadsheetIdentificator);
                getSheetRequest.IncludeGridData = true;

                return getSheetRequest.Execute();
            }
        }

        public ClearValuesResponse RemoveSingleValue(string googleSpreadsheetIdentificator, string valueRange)
        {
            if (string.IsNullOrEmpty(googleSpreadsheetIdentificator))
                throw new ArgumentNullException(nameof(googleSpreadsheetIdentificator));
            if (string.IsNullOrEmpty(valueRange))
                throw new ArgumentNullException(nameof(valueRange));
            using (var sheetsService = new SheetsService(new BaseClientService.Initializer()
            { HttpClientInitializer = _credential }))
            {
                var removeValueRequest = sheetsService.Spreadsheets.Values.Clear(new ClearValuesRequest(), googleSpreadsheetIdentificator, valueRange);
               return removeValueRequest.Execute();
            }
        }
        public BatchClearValuesResponse ClearMultipleValues(string googleSpreadsheetIdentificator, string[] ranges)
        {
            if (string.IsNullOrEmpty(googleSpreadsheetIdentificator))
                throw new ArgumentNullException(nameof(googleSpreadsheetIdentificator));
            if (ranges == null || ranges.Length == 0)
                throw new ArgumentNullException(nameof(ranges));
            using (var sheetsService = new SheetsService(new BaseClientService.Initializer()
            { HttpClientInitializer = _credential }))
            {
                var clearMultipleValuesRequest = sheetsService.Spreadsheets.Values.BatchClear(new BatchClearValuesRequest() { Ranges = ranges }, googleSpreadsheetIdentificator);
                return clearMultipleValuesRequest.Execute();
            }
        }
        public BatchUpdateSpreadsheetResponse UpdateValueAndBackgroundColor(string googleSpreadsheetIdentificator, string valueRange, string value, Color backgroundColor, GridRange gridRange)
        {
            if (string.IsNullOrEmpty(googleSpreadsheetIdentificator))
                throw new ArgumentNullException(nameof(googleSpreadsheetIdentificator));
            if (string.IsNullOrEmpty(valueRange))
                throw new ArgumentNullException(nameof(valueRange));
            if (value==null)
                throw new ArgumentNullException(nameof(value));
            if (backgroundColor==null)
                throw new ArgumentNullException(nameof(backgroundColor));
            if (gridRange == null)
                throw new ArgumentNullException(nameof(gridRange));
            using (var sheetsService = new SheetsService(new BaseClientService.Initializer()
            { HttpClientInitializer = _credential }))
            {

                var updateCellsBackgroundRequest = new Request()
                {
                    RepeatCell = new RepeatCellRequest()
                    {
                        Range = gridRange,
                        Cell = new CellData()
                        {
                            UserEnteredFormat = new CellFormat()
                            {
                                BackgroundColor = backgroundColor
                            }
                        },
                        Fields = "UserEnteredFormat(BackgroundColor)"
                    }
                };
                var updateCellsValueRequest = new Request()
                {
                    UpdateCells = new UpdateCellsRequest()
                    {
                        Range = gridRange,
                        Fields = "UserEnteredValue",
                        Rows = new[]
                        {
                            new RowData()
                            {
                                Values = new[]
                                {
                                    new CellData()
                                    {
                                        UserEnteredValue = new ExtendedValue
                                        {
                                            StringValue=value
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
                BatchUpdateSpreadsheetRequest bussr = new BatchUpdateSpreadsheetRequest();
                bussr.Requests = new List<Request>
                {
                    updateCellsBackgroundRequest,
                    updateCellsValueRequest
                };
                var batchUpdateRequest = sheetsService.Spreadsheets.BatchUpdate(bussr, googleSpreadsheetIdentificator);
                return batchUpdateRequest.Execute();
            }

        }
    }
}
