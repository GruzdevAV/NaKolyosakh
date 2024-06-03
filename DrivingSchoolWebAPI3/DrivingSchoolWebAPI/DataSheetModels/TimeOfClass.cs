
namespace DrivingSchoolWebAPI.DataSheetModels
{
    public class TimeOfClass
    {
        /// <summary>
        /// Время начала занятия
        /// </summary>
        public TimeOnly Start { get; set; }
        /// <summary>
        /// Продолжительность занаятия
        /// </summary>
        public TimeSpan HowLongLasts { get; set; }
        /// <summary>
        /// Время окончания занятия
        /// </summary>
        public TimeSpan End
        {
            get => Start.ToTimeSpan() + HowLongLasts;
            set => HowLongLasts = value - Start.ToTimeSpan();
        }
        public TimeOfClass(string timeInterval)
        {
            var timesOfInterval = Methods.ParseTimes(timeInterval);
            Start = timesOfInterval[0];
            End = timesOfInterval[1].ToTimeSpan();
        }
        public override string ToString()
        {
            var dateStart = new DateTime(Start.Ticks, DateTimeKind.Utc);
            var dateEnd = new DateTime(End.Ticks, DateTimeKind.Utc);
            return $"{dateStart:HH:mm}-{dateEnd:HH:mm}";
        }
    }
}
