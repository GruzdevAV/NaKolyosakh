using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    public class AddOuterScheduleModel : OuterScheduleOfInstructorModel
    {
        [Required]
        public string UserId { get; set; }
    }
}
