using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    public class SetMeImageModel
    {
        [Required]
        public string Id { get; set; }
        public byte[]? ImageBytes { get; set; }
    }
}
