using System;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    public class OuterClassData
    {
        //public DateTime StartTime { get; set; }
        //public TimeSpan TimeOffset { get; set; }
        public string Student { get; set; }
        public TimeOfClass Time { get; set; }
        public DateOfClass Date { get; set; }
        public bool Free { get; set; }
        //public DateTime EndTime
        //{
        //    get => StartTime.Add(TimeOffset);
        //    set => TimeOffset = value - StartTime;
        //}
        public OuterClassData(TimeOfClass time, DateOfClass date, bool free, string student = null)
        {
            Time = time;
            Date = date;
            Free = free;
            Student = student;
        }
        public override string ToString()
        {
            return $"{Date} {Time}: {(Free ? "Available" : (Student == null ? "Not available" : Student))}";
        }
    }
}
