using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Оценка от ученика инструктору
    /// </summary>
    public class GradeByStudentToInstructor: GradeByStudentToInstructorModel
    {
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
    }
}
