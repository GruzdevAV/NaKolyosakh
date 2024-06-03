namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель ответа при успешном входе в аккаунт
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Токен для аутентификации
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Годность токена
        /// </summary>
        public DateTime Expiration { get; set; }
        public string Role { get; set; }
        public string Id { get; set; }
    }
}
