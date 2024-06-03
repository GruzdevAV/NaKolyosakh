using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для передачи данных оценки на сервер
    /// </summary>
    public class GradeByInstructorToStudentModel
    {
        [Key]
        public int ClassId { get; set; }
        public GradesByInstructorsToStudents Grade { get; set; }
        public string? Comment { get; set; }
    }
}
