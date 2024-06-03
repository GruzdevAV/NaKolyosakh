using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;

namespace DrivingSchoolWebAPI.DataSheetModels
{
    /// <summary>
    /// Класс, осуществляющий авторизацию клиента Api Google-таблиц
    /// </summary>
    public static class GoogleAuthentication
    {
        public static UserCredential Login(string googleClientId, string googleClientSecret, string[] scopes)
        {
            return LoginAsync(googleClientId, googleClientSecret, scopes).Result;
        }

        public static async Task<UserCredential> LoginAsync(string googleClientId, string googleClientSecret, string[] scopes)
        {
            ClientSecrets secrets = new()
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret
            };
            var path = Path.Combine(Environment.CurrentDirectory, "App_Data", "OAuth");
            Directory.CreateDirectory(path);
            var dataStore = new FileDataStore(path, true);
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user", CancellationToken.None, dataStore: dataStore);
        }
    }
}
