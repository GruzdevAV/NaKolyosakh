using DrivingSchoolWebAPI.DataSheetModels;

namespace DrivingSchoolAPIModels
{
    /// <summary>
    /// Нужно лишь для удобного хранения данных для создания администратора по умолчанию
    /// </summary>
    public static class DefaultData
    {
        public const string InstructorPassword = "Pass_1";
        public const string AdminPassword = "Admin_Password1";
        public const string AdminName = "Admin";
        public const string AdminEmail = "admin@example.com";
        public static string[] Names = { "Jones", "Louis", "Gill", "Curtis", "Jamie", "Evans", "Jeremy", "Smith", "Eleanor", "Brown", "Timothy", "Stacey", "Amy", "Dorothy", "Crawford", "Mattie", "Washington", "Sara", "Nelson", "Miles", "Saunders", "Rodriguez", "Murphy", "Elaine", "Morgan", "Brooks", "Chambers", "Vivian", "Charlotte", "Mary", "Flores", "Angela", "Douglas", "Myers", "Jackson", "Williams", "Rodney", "Morales", "Ronnie" };
        public static string RandomName => Names[new Random().Next(Names.Length)];
        public static GoogleSheetApiClient ApiClient { get; set; } = new GoogleSheetApiClient();
        //public static List<ApplicationUser> Users
        //{
        //    get
        //    {
        //        var users = new List<ApplicationUser>()
        //        {
        //            new ApplicationUser() {}
        //        };
        //        return users;
        //    }
        //}
    }
}
