using DrivingSchoolAPIModels;

namespace DrivingSchoolAPIModels
{
    public class InnerScheduleOfInstructorModel
    {
        public List<Class> Classes { get; set; }
        public InnerScheduleOfInstructor Schedule { get; set; }

        public InnerScheduleOfInstructorModel(InnerScheduleOfInstructor schedule, List<Class> classes)
        {
            Schedule = schedule;
            Classes = classes;
        }
    }
}