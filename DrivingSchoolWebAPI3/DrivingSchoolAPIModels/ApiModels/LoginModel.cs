using System.ComponentModel.DataAnnotations;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель для входа в аккаунт
    /// </summary>
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Требуется адрес электронной почты")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Требуется пароль")]
        public string Password { get; set; }
    }
}
