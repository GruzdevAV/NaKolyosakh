using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Инструктор
    /// </summary>
    public class Instructor : IPerson
    {
        public int InstructorId { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        [NotMapped]
        public string FirstName { get => User?.FirstName ?? "null"; set => User.FirstName = value; }
        [NotMapped]
        public string LastName { get => User?.LastName ?? "null"; set => User.LastName = value; }
        [NotMapped]
        public string? Patronymic { get => User?.Patronymic ?? "null"; set => User.Patronymic = value; }
        [NotMapped]
        public byte[] ProfilePictureBytes { get => User?.ProfilePictureBytes ?? new byte[0]; set => User.ProfilePictureBytes = value; }
        [NotMapped]
        public Image ProfilePicture { get => User?.ProfilePicture; set => User.ProfilePicture = value; }

        public override string ToString()
        {
            return $"{LastName} {FirstName}{(Patronymic!=null ? ' '+Patronymic:"")}";
        }
    }
}
