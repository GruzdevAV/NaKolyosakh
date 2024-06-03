using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPIModels
{
    public class OuterScheduleOfInstructor : OuterScheduleOfInstructorModel
    {
        /// <summary>
        /// Id внешнего расписания
        /// </summary>
        [Key]
        public int OuterScheduleId { get; set; }

        /// <summary>
        /// Id инструктора, которому принадлежит это внешнее расписание
        /// </summary>
        public int InstructorId { get; set; }
        /// <summary>
        /// Инструктор, которому принадлежит это внешнее расписание
        /// </summary>
        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }

        //public OuterScheduleOfInstructor(OuterScheduleOfInstructorModel model)
        //{
        //    InstructorId = model.InstructorId;
        //    GoogleSheetId = model.GoogleSheetId;
        //    GoogleSheetPageName = model.GoogleSheetPageName;
        //    TimesOfClassesRange = model.TimesOfClassesRange;
        //    DatesOfClassesRange = model.DatesOfClassesRange;
        //    ClassesRange = model.ClassesRange;
        //    FreeClassExampleRange = model.FreeClassExampleRange;
        //    NotFreeClassExampleRange = model.NotFreeClassExampleRange;
        //    YearRange = model.YearRange;
        //}
        public static OuterScheduleOfInstructor Constructor(OuterScheduleOfInstructorModel model)
        {
            return new OuterScheduleOfInstructor
            {
                GoogleSheetId = model.GoogleSheetId,
                GoogleSheetPageName = model.GoogleSheetPageName,
                TimesOfClassesRange = model.TimesOfClassesRange,
                DatesOfClassesRange = model.DatesOfClassesRange,
                ClassesRange = model.ClassesRange,
                FreeClassExampleRange = model.FreeClassExampleRange,
                NotFreeClassExampleRange = model.NotFreeClassExampleRange,
                YearRange = model.YearRange
            };
        }
        // TODO: добавить все методы по получению данных, добавлению и проверке внутренних расписаний сюда
    }
}
