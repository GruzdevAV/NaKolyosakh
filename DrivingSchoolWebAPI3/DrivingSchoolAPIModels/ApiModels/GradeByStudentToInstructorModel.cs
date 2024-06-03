using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для передачи данных оценки на сервер
    /// </summary>
    public class GradeByStudentToInstructorModel
    {
        [Key]
        public int ClassId { get; set; }
        public GradesByStudentsToInstructors Grade { get; set; }
        public string? Comment { get; set; }
    }
}
