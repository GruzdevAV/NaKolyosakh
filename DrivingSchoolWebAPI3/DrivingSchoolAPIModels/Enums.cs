namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Оценки от инструкторов ученикам
    /// </summary>
    public enum GradesByInstructorsToStudents : byte
    {
        /// <summary>
        /// Прогул
        /// </summary>
        Прогул = 1,
        Удовлетворительно,
        Хорошо,
        Отлично
    }
    /// <summary>
    /// Оценки от учеников инструкторам
    /// </summary>
    public enum GradesByStudentsToInstructors : byte 
    {
        Ужасно = 1, 
        Плохо, 
        Удовлетворительно, 
        Хорошо, 
        Отлично
    };
    /// <summary>
    /// Состояния занятий
    /// </summary>
    public enum ClassStatus : byte { Предстоит, Завершено, Отменено }
    public enum ClassType : byte { Inner, Outer }
}