using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель данных имени человек
    /// </summary>
    public interface IPerson
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
        /// <summary>
        /// Поле картинки профиля
        /// </summary>
        public byte[]? ProfilePictureBytes { get; set; }
        [NotMapped]
        /// <summary>
        /// Картинка профиля
        /// </summary>
        public Image? ProfilePicture { get; set; }

    }
}