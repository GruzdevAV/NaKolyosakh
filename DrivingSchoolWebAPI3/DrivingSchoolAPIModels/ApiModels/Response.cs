namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Модель ответа на запрос, когда нужно передать сообщение и посылку
    /// </summary>
    public class Response<TPackage> : Response
    {
        public TPackage? Package { get; set; } = default;
    }
    /// <summary>
    /// Модель ответа на запрос, когда нужно передать сообщение
    /// </summary>
    public class Response
    {
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
