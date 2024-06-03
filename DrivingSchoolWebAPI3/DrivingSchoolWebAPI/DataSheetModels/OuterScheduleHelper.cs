using DrivingSchoolAPIModels;
using Google.Apis.Sheets.v4.Data;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    public class OuterScheduleHelper
    {
        private OuterSchedule _outerSchedule;
        private GoogleSheetHelper _sheetHelper;
        private Color _notFreeColor;
        private Color _freeColor;
        public OuterScheduleOfInstructor OuterScheduleOfInstructor { get;  set; }
        public GoogleSheetApiClient ApiClient { get; set; }
        public Color NotFreeColor
        {
            get
            {
                if (_notFreeColor == null)
                    try
                    {
                        _notFreeColor = SheetHelper[OuterScheduleOfInstructor.NotFreeClassExampleRange].EffectiveFormat.BackgroundColor;
                    }
                    catch
                    {
                        _notFreeColor = new Color() { Red = 1, Alpha = 1 };
                    }
                return _notFreeColor;
            }
            set => _notFreeColor = value;
        }
        public Color FreeColor
        {
            get
            {
                if (_freeColor == null)
                    try
                    {
                        _freeColor = SheetHelper[OuterScheduleOfInstructor.FreeClassExampleRange].EffectiveFormat.BackgroundColor;
                    }
                    catch
                    {
                        _freeColor = new Color() { Green = 1, Alpha = 1 };
                    }
                return _freeColor;
            }
            set => _freeColor = value;
        }
        public OuterSchedule OuterSchedule
        {
            get
            {
                if (_outerSchedule == null)
                    _outerSchedule = GetAllClassSlots();
                return _outerSchedule;
            }
            set => _outerSchedule = value;
        }
        public GoogleSheetHelper SheetHelper
        {
            get
            {
                if (_sheetHelper == null)
                    _sheetHelper = GetSheetHelper();
                return _sheetHelper;
            } 
            set => _sheetHelper = value;
        }
        public GoogleSheetHelper GetSheetHelper()
        {
            var sheets = ApiClient.Manager.GetSpreadsheet(OuterScheduleOfInstructor.GoogleSheetId);
            return new GoogleSheetHelper(sheets.Sheets.Where(x => x?.Properties.Title == OuterScheduleOfInstructor.GoogleSheetPageName).FirstOrDefault());
        }
        public Dictionary<int, DateOfClass> GetDates()
        {
            var year = Methods.ParseYear(SheetHelper[OuterScheduleOfInstructor.YearRange].FormattedValue);
            var dates = new Dictionary<int, DateOfClass>();
            foreach (var inds in Methods.PythonRange(OuterScheduleOfInstructor.DatesOfClassesRange))
            {
                var dayAndMonth = SheetHelper[inds.Row, inds.Col].FormattedValue;
                //if (dates.ContainsValue())
                dates[inds.Row] = new DateOfClass(dayAndMonth, year);
            }

            return dates;
        }
        public Dictionary<int, TimeOfClass> GetTimes()
        {
            var timesList = new Dictionary<int, TimeOfClass>();
            foreach (var inds in Methods.PythonRange(OuterScheduleOfInstructor.TimesOfClassesRange))
            {
                var timeInterval = SheetHelper[inds.Row, inds.Col].FormattedValue;
                
                timesList[inds.Col] = new TimeOfClass(timeInterval);
            }

            return timesList;
        }
        //public void SetClass(int dateRow, List<int> timeCols, string student)
        public void SetClass(int dateRow, int timeCol, string student)
        {
            var range = $"{Methods.NumberToExcelColumnName(timeCol)}{dateRow+1}";
            
            var grid = new GridRange
            {
                SheetId= SheetHelper.Sheet.Properties.SheetId,
                StartColumnIndex = timeCol,
                EndColumnIndex = timeCol + 1,
                StartRowIndex = dateRow,
                EndRowIndex = dateRow + 1,
            };
            ApiClient.Manager.UpdateValueAndBackgroundColor(OuterScheduleOfInstructor.GoogleSheetId, $"{OuterScheduleOfInstructor.GoogleSheetPageName}!{range}", student, NotFreeColor, grid);
        }
        public void FreeClass(int dateRow, int timeCol)
        {
            var range = $"{Methods.NumberToExcelColumnName(timeCol)}{dateRow + 1}";

            var grid = new GridRange
            {
                SheetId = SheetHelper.Sheet.Properties.SheetId,
                StartColumnIndex = timeCol,
                EndColumnIndex = timeCol + 1,
                StartRowIndex = dateRow,
                EndRowIndex = dateRow + 1,
            };
            ApiClient.Manager.UpdateValueAndBackgroundColor(OuterScheduleOfInstructor.GoogleSheetId, $"{OuterScheduleOfInstructor.GoogleSheetPageName}!{range}", "", FreeColor, grid);
        }
        public OuterSchedule GetAllClassSlots()
        {
            if (SheetHelper.Sheet == null) { return null; }
            Dictionary<int, DateOfClass> dates = GetDates();
            Dictionary<int, TimeOfClass> times = GetTimes();

            OuterSchedule classes = new(dates, times);

            foreach (var inds in Methods.PythonRange(OuterScheduleOfInstructor.ClassesRange))
            {
                //string c = App.NumberToExcelColumnName(inds.Col + 1);
                string value = SheetHelper[inds.Row, inds.Col]!.FormattedValue;
                var clr = SheetHelper[inds.Row, inds.Col]?.EffectiveFormat?.BackgroundColor;
                TimeOfClass time = times[inds.Col];
                DateOfClass date = dates[inds.Row];
                var isFree = Methods.ColorsAreEqual(clr, FreeColor);
                var outerClass = new OuterClassData(time, date, isFree, value);
                classes[inds.Row, inds.Col] = outerClass;
            }
            return classes;
        }
    }
}
