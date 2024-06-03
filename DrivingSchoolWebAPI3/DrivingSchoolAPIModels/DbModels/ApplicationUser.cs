using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class ApplicationUser : IdentityUser, IPerson
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }
        [IgnoreDataMember]
        [NotMapped]
        private Image? _image = null;
        /// <summary>
        /// Поле картинки профиля
        /// </summary>
        public byte[]? ProfilePictureBytes { get; set; }
        [NotMapped]
        [IgnoreDataMember]
        /// <summary>
        /// Картинка профиля
        /// </summary>
        public Image? ProfilePicture
        {
            get
            {
                if (_image == null && ProfilePictureBytes != null)
                {
                    using (MemoryStream ms = new(ProfilePictureBytes))
                    {
                        _image = Image.FromStream(ms);
                    }
                }
                return _image;
            }
            set
            {
                _image = value;
                if (value == null)
                {
                    ProfilePictureBytes = null;
                    return;
                }
                using (MemoryStream ms = new(ProfilePictureBytes))
                {
                    value.Save(ms, value.RawFormat);
                    ProfilePictureBytes = ms.ToArray();
                }
            }
        }
    }
}
