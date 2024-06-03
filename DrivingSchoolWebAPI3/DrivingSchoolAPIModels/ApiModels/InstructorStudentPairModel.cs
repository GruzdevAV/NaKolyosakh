using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для передачи пары значений id ученика и инструктора на сервер
    /// </summary>
    public class InstructorStudentPairModel
    {
        [Required]
        public int InstructorId { get; set; }
        [Required]
        public int StudentId { get; set;}
    }
}
