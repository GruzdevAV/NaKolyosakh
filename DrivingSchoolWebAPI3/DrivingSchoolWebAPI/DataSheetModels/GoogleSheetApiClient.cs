using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    /// <summary>
    /// Api-клиент для подключения к Google-таблицам
    /// </summary>
    public class GoogleSheetApiClient
    {
        string GoogleClientId;
        string GoogleClientSecret;
        string[] scopes = { SheetsService.Scope.Spreadsheets };
        UserCredential credential;
        public GoogleSheetsManager Manager { get; set; }
        public GoogleSheetApiClient()
        {
            string pathToSecret = Path.Combine(Environment.CurrentDirectory, "App_Data", "client_secret.json");
            using (var stream = new FileStream(pathToSecret, FileMode.Open, FileAccess.Read))
            {
                var secrets = GoogleClientSecrets.FromStream(stream).Secrets;
                GoogleClientId =secrets.ClientId;
                GoogleClientSecret=secrets.ClientSecret;
                credential = GoogleAuthentication.Login(secrets.ClientId, secrets.ClientSecret, scopes);
                Manager = new GoogleSheetsManager(credential);
            }
        }
    }
}
