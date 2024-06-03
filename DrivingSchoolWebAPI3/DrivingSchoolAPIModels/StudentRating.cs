namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Студент с рейтингом
    /// </summary>
    public class StudentRating : Student
    {
        public int NumberOfGrades { get; set; }
        public float Grade { get; set; }
        public static StudentRating FromStudent(Student student, int count=0, float avg=0.0f)
        {
            return new StudentRating
            {
                StudentId = student.StudentId,
                User = student.User,
                UserId = student.UserId,
                InstructorId = student.InstructorId,
                Instructor = student.Instructor,
                Grade = avg,
                NumberOfGrades = count
            };
        }
    }
}
