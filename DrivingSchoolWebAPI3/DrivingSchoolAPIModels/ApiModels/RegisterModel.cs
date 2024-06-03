using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для регистрации нового пользователя
    /// </summary>
    public class RegisterModel : IPerson
    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Требуется адрес электронной почты")]
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary
        [Required(ErrorMessage = "Требуется пароль")]
        public string Password { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        [Phone]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "Требуется имя")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "Требуется фамилия")]
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }
        [IgnoreDataMember]
        private Image? _image = null;
        /// <summary>
        /// Поле картинки профиля
        /// </summary>
        public byte[]? ProfilePictureBytes { get; set; }
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
