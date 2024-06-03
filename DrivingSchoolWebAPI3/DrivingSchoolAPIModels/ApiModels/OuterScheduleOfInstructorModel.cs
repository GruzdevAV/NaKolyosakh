using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    public class OuterScheduleOfInstructorModel
    {
        /// <summary>
        /// Id google-таблицы
        /// то есть это [id] в https://docs.google.com/spreadsheets/d/[id]/edit#gid=[gid]
        /// </summary>
        [Required]
        public string GoogleSheetId { get; set; }
        /// <summary>
        /// Имя страницы google-таблицы
        /// </summary>
        [Required]
        public string GoogleSheetPageName { get; set; }
        /// <summary>
        /// Диапазон ячеек времён рассписания занятий. Должны быть на одной строке
        /// </summary>
        [Required]
        public string TimesOfClassesRange { get; set; }
        /// <summary>
        /// Диапазон ячеек дат (день+месяц) рассписания занятий. Должны быть в одном столбце
        /// </summary>
        [Required]
        public string DatesOfClassesRange { get; set; }
        /// <summary>
        /// Диапазон ячеек доступных занятий
        /// </summary>
        [Required]
        public string ClassesRange { get; set; }
        /// <summary>
        /// Ячейка с примером форматирования свободного занятия
        /// </summary>
        [Required]
        public string FreeClassExampleRange { get; set; }
        /// <summary>
        /// Ячейка с примером форматирования несвободного занятия. Может отсутствовать
        /// </summary>
        public string? NotFreeClassExampleRange { get; set; }
        /// <summary>
        /// Ячейка, содержащая год занятий
        /// </summary>
        [Required]
        public string YearRange { get; set; }

    }
}