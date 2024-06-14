using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DrivingSchoolAPIModels
{
    // Занятие
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        /// <summary>
        /// Тип занятия (внешнее или внутреннее)
        /// </summary>
        public bool IsOuterClass => InnerScheduleOfInstructor?.IsOuterSchedule == true;
        // 
        public int InnerScheduleOfInstructorId { get; set; }
        [ForeignKey(nameof(InnerScheduleOfInstructorId))]
        public InnerScheduleOfInstructor InnerScheduleOfInstructor { get; set; }
        // Если занятие получено из внешнего источника
        public int? OuterScheduleId => InnerScheduleOfInstructor?.OuterScheduleId;
        public OuterScheduleOfInstructor? OuterScheduleOfInstructor => InnerScheduleOfInstructor?.OuterScheduleOfInstructor;
        // Сам занимающийся ученик
        public int? StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }
        // Будет хранить только StartTime, Duration и Status
        public TimeOnly StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        /// <summary>
        /// Проведено ли занятие
        /// </summary>
        [DefaultValue(ClassStatus.Предстоит)]
        public ClassStatus Status { get; set; }
        // Остальные значения будет вычислять в процессе
        [NotMapped]
        public TimeOnly EndTime
        {
            get => TimeOnly.FromTimeSpan(StartTime.ToTimeSpan() + Duration);
            set => Duration = value - StartTime;
        }
        public DateTime StartDateTime => InnerScheduleOfInstructor.DayOfWork.ToDateTime(StartTime);
        public DateTime EndDateTime => InnerScheduleOfInstructor.DayOfWork.ToDateTime(EndTime);
        public DateOnly Date => InnerScheduleOfInstructor.DayOfWork;
        // Инструктор
        public int InstructorId => Instructor.InstructorId;
        public Instructor Instructor => InnerScheduleOfInstructor.Instructor;
        [NotMapped]
        public string StartTimeJson
        {
            get => StartTime.ToString(Consts.TimeOnlyFormatToString);
            set => StartTime = TimeOnly.ParseExact(value, Consts.TimeSpanFormats);
        }
        [NotMapped]
        public string DurationJson
        {
            get => Duration.ToString(Consts.TimeSpanFormatToString);
            set => Duration = TimeSpan.ParseExact(value, Consts.TimeSpanFormats, CultureInfo.CurrentCulture);
        }
    }
}