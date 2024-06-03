using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.Serialization;

namespace DrivingSchoolAPIModels
{
    public class EditMeModel : IPerson
    {
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? EmailAddress { get; set; }
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string? LastName { get; set; }
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
                if(value == null)
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
