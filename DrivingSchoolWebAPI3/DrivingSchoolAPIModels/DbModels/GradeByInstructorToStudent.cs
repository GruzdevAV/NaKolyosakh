using System.ComponentModel.DataAnnotations.Schema;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Оценка от инструктора ученику
    /// </summary>
    public class GradeByInstructorToStudent: GradeByInstructorToStudentModel
    {
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
    }
}
