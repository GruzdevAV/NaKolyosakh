namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Инструктор с рейтингом
    /// </summary>
    public class InstructorRating : Instructor
    {
        public int NumberOfGrades { get; set; }
        public float Grade { get; set; }
    }
}
