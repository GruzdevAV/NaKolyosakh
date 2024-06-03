using DrivingSchoolAPIModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    /// <summary>
    /// 
    /// </summary>
    public static class Methods
    {
        /// <summary>
        /// Регулярное выражение, соответствующее интервалу времени вида "H:mm - H:mm"
        /// </summary>
        public static readonly Regex TimeInterval = new Regex(@"\d{1,2}\s*:\s*\d{2}\s*-\s*\d{1,2}\s*:\s*\d{2}");
        /// <summary>
        /// Регулярное выражение, соответствующее времени вида "H:mm"
        /// </summary>
        public static readonly Regex Time = new Regex(@"\d{1,2}\s*:\s*\d{2}");
        /// <summary>
        /// Регулярное выражение, соответствующее числу из 4 символов
        /// </summary>
        public static readonly Regex Year = new Regex(@"\d{4}");
        /// <summary>
        /// Регулярное выражение, соответствующее дате вида "d.M"
        /// </summary>
        public static readonly Regex Date = new Regex(@"\d{1,2}\.\d{1,2}");
        /// <summary>
        /// Преобразовать строку вида "H:mm" (все значения времени от 0:00 по 23:53) во время типа TimeOnly
        /// </summary>
        /// <param name="time">Строка вида "H:mm"</param>
        /// <returns></returns>
        public static TimeOnly ParseTime(string time)
        {
            return TimeOnly.Parse(time.Replace(" ", ""));
        }
        /// <summary>
        /// Преобразует строку вида "H:mm - H:mm" в 
        /// </summary>
        /// <param name="timeInterval"></param>
        /// <returns></returns>
        public static TimeOnly[] ParseTimes(string timeInterval)
        {
            var times = Time.Matches(timeInterval);
            var arr = (from Match match in times
                        select ParseTime(match.Value));
            return arr.ToArray();
        }
        /// <summary>
        /// Преобразует строку даты вида "d.M" (1.1 - 31.12) в значение DateOnly
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateOnly ParseDate(string date)
        {
            return DateOnly.ParseExact(Date.Match(date).Value, "d.M");
        }
        /// <summary>
        /// Находит в строке год и возвращает его
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int ParseYear(string year)
        {
            return int.Parse(Year.Match(year).Value);
        }
        public static DateOnly ParseDateYear(string date, string year)
        {
            var d = ParseDate(date);
            return new DateOnly(ParseYear(year), d.Month, d.Day);
        }
        public static DateOnly ParseDateYear(string date, int year)
        {
            var d = ParseDate(date);
            return new DateOnly(year, d.Month, d.Day);
        }
        /// <summary>
        /// Преобразует индекс столбца Excel в число 
        /// (A -> 0, B -> 1, Z -> 25 AA -> 26)
        /// https://stackoverflow.com/a/667902
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int ExcelColumnNameToNumber(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));
            columnName = columnName.ToUpperInvariant();
            int sum = 0;
            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }
            return sum;
        }
        /// <summary>
        /// Преобразует число в индекс столбца Excel
        /// (0 -> A, 1 -> B, 25 -> Z, 26 -> AA)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string NumberToExcelColumnName(int number)
        {
            if (number < 1) throw new ArgumentNullException(nameof(number));
            var name = "";
            do
            {
                var num = (--number) % 26;
                char temp = (char)('A' + num);
                name = temp + name;
                number /= 26;
            } while (number > 0);
            return name;
        }
        /// <summary>
        /// Преобразует индекс ячейки Excel в числовые индексы
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Indices ExcelIndexToIndices(string range)
        {
            var nums = new Regex(@"\d+").Match(range).Value;
            var letters = range.Replace(nums, "");
            return new Indices { Row = int.Parse(nums) - 1, Col = ExcelColumnNameToNumber(letters) - 1 };
        }
        /// <summary>
        /// Сравнивает цвета Google.Apis.Sheets.v4.Data.Color
        /// </summary>
        /// <param name="clr1"></param>
        /// <param name="clr2"></param>
        /// <returns></returns>
        public static bool ColorsAreEqual(Google.Apis.Sheets.v4.Data.Color clr1, Google.Apis.Sheets.v4.Data.Color clr2)
        {
            if (clr1 == null || clr2 == null) return clr1 == clr2;
            return (clr1.Alpha?.Equals(clr2.Alpha) != false)
                && (clr1.Red?.Equals(clr2.Red) != false)
                && (clr1.Green?.Equals(clr2.Green) != false)
                && (clr1.Blue?.Equals(clr2.Blue) != false);
        }
        /// <summary>
        /// Цикл по всем индексам, входящим в диапазон
        /// </summary>
        /// <param name="one"></param>
        /// <param name="another"></param>
        /// <returns></returns>
        public static IEnumerable<Indices> PythonRange(Indices one, Indices another)
        {
            // Наибольшее значение индекса строки
            var maxRow = Math.Max(one.Row, another.Row);
            // Наибольшее значение индекса столбца
            var maxCol = Math.Max(one.Col, another.Col);
            for (int row = Math.Min(one.Row, another.Row); row <= maxRow; row++)
            {
                for (int col = Math.Min(one.Col, another.Col); col <= maxCol; col++)
                {
                    yield return new Indices { Row = row, Col = col };
                }
            }
        }
        /// <summary>
        /// Цикл по всем индексам, входящим в диапазон
        /// </summary>
        /// <param name="range">строка вида "J10;J12:L13;J16:L16;J18"</param>
        /// <returns></returns>
        public static IEnumerable<Indices> PythonRange(string range)
        {
            foreach (var temp in range.Split(';'))
            {
                var t = temp.Split(':');
                foreach (var v in PythonRange(ExcelIndexToIndices(t.First()), ExcelIndexToIndices(t.Last())))
                    yield return v;
            }
        }
        public static Dictionary<DateOnly, List<Class>> GetClassesPerDay(OuterScheduleOfInstructor self, GoogleSheetApiClient apiClient)
        {
            var helper = new OuterScheduleHelper
            {
                OuterScheduleOfInstructor = self,
                ApiClient = apiClient,
            };
            var oClasses = helper.OuterSchedule;
            var classesPerDay = new Dictionary<DateOnly, List<Class>>();
            foreach (var @class in oClasses.OuterClasses)
            {
                if (!@class.Value.Free) continue;
                var day = @class.Value.Date.Date;
                if (!classesPerDay.ContainsKey(day)) classesPerDay[day] = new();
                classesPerDay[day].Add(new Class
                {
                    StartTime = @class.Value.Time.Start,
                    Duration = @class.Value.Time.HowLongLasts
                });
            }
            return classesPerDay;
        }
        public static bool MarkClass(Student? student, DateOnly date, TimeOnly time, OuterScheduleOfInstructor self, GoogleSheetApiClient apiClient)
        {
            try
            {
                var helper = new OuterScheduleHelper
                {
                    OuterScheduleOfInstructor = self,
                    ApiClient = apiClient,
                };
                var oSchedule = helper.OuterSchedule;
                var mDate = oSchedule.Dates
                    .Where(x => x.Value.Date == date)
                    .Select(x => x.Key)
                    .Last();
                var mTime = oSchedule.Times
                    .Where(x => x.Value.Start == time)
                    .Select(x => x.Key)
                    .Last();
                if (student == null)
                    helper.FreeClass(mDate, mTime);
                else
                    helper.SetClass(mDate, mTime, student!.ToString());
            }
            catch { return false; }
            return true;
        }
        public static bool MakeSureOuterMarked(Class @class, GoogleSheetApiClient apiClient)
        {
            return Methods.MarkClass(@class.Student, @class.Date, @class.StartTime, @class.OuterScheduleOfInstructor, apiClient);
        }
    }
}
