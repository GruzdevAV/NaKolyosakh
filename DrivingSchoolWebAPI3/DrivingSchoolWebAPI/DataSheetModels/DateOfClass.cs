namespace DrivingSchoolWebAPI.DataSheetModels
{
    public class DateOfClass
    {
        /// <summary>
        /// Дата занятия
        /// </summary>
        public DateOnly Date {  get; set; }
        public DateOfClass(string date, int year)
        {
            Date = Methods.ParseDateYear(date,year);
        }
        public override string ToString()
        {
            return Date.ToString("dd.MM.yyyy");
        }
    }
}
