using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Runtime.Serialization;

namespace DrivingSchoolAPIModels
{
    public class ClassModel
    {
        public int? StudentId { get; set; }
        public string StartTimeJson { get; set; }
        public string DurationJson { get; set; }
        [NotMapped]
        [IgnoreDataMember]
        public TimeSpan StartTime
        {
            get => TimeSpan.ParseExact(StartTimeJson, Consts.TimeSpanFormats, CultureInfo.CurrentCulture);
            set => StartTimeJson = value.ToString(Consts.TimeSpanFormatToString);
        }
        [NotMapped]
        [IgnoreDataMember]
        public TimeSpan Duration
        {
            get => TimeSpan.ParseExact(DurationJson, Consts.TimeSpanFormats, CultureInfo.CurrentCulture);
            set => DurationJson = value.ToString(Consts.TimeSpanFormatToString);
        }
        public int? InstructorId {  get; set; }
        public int? InnerScheduleOfInstructorId { get; set; }
    }
}