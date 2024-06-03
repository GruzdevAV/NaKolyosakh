using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для передачи пары значений id ученика и занятия на сервер
    /// </summary>
    public class ClassStudentPairModel
    {
        [Required]
        public int ClassId { get; set; }
        [Required]
        public int StudentId { get; set; }
    }
}
