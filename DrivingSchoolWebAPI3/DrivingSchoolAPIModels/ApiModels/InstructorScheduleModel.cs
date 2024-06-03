using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для передачи данных расписания инструктора на сервер
    /// </summary>
    public class InstructorScheduleModel
    {
        [Required]
        public int InstructorId { get; set; }
        [Required]
        public string DayOfWorkJson { get; set; }
        [NotMapped]
        [IgnoreDataMember]
        [JsonIgnore]
        public DateOnly DayOfWork
        {
            get => DateOnly.ParseExact(DayOfWorkJson, "dd.MM.yyyy");
            set => DayOfWorkJson = value.ToString("dd.MM.yyyy");
        }
        [Required]
        public ClassModel[] Classes { get; set; }
    }
}
