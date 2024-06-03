#define DELET

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchoolAPIModels
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        ////https://code-maze.com/csharp-map-dateonly-timeonly-types-to-sql/
        //protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        //{
        //    base.ConfigureConventions(builder);
        //    builder.Properties<DateOnly>()
        //        .HaveConversion<DateOnlyConverter>();
        //    builder.Properties<TimeOnly>()
        //        .HaveConversion<TimeOnlyConverter>();
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            CreateDefaultData(builder);
        }

        private static void CreateDefaultData(ModelBuilder builder)
        {
            var adminRoleGuid = Guid.NewGuid().ToString();
            var instructorRoleGuid = Guid.NewGuid().ToString();
            var studentRoleGuid = Guid.NewGuid().ToString();
            var adminUserGuid = Guid.NewGuid().ToString();
            var instructorUserGuids = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            var studentUserGuids = new[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            var hasher = new PasswordHasher<ApplicationUser>();
            var users = builder.Entity<ApplicationUser>();
            var userRoles = builder.Entity<IdentityUserRole<string>>();
            var students = builder.Entity<Student>();
            var instructors = builder.Entity<Instructor>();
            var roles = builder.Entity<IdentityRole>();
            roles.HasData(
                new IdentityRole
                {
                    Id = adminRoleGuid,
                    Name = DrivingSchoolAPIModels.UserRoles.Admin,
                    NormalizedName = DrivingSchoolAPIModels.UserRoles.Admin.ToUpper()
                });
            roles.HasData(
                new IdentityRole
                {
                    Id = studentRoleGuid,
                    Name = DrivingSchoolAPIModels.UserRoles.Student,
                    NormalizedName = DrivingSchoolAPIModels.UserRoles.Student.ToUpper()
                });
            roles.HasData(
                new IdentityRole
                {
                    Id = instructorRoleGuid,
                    Name = DrivingSchoolAPIModels.UserRoles.Instructor,
                    NormalizedName = DrivingSchoolAPIModels.UserRoles.Instructor.ToUpper()
                });
            users.HasData(
                new ApplicationUser
                {
                    Id = adminUserGuid,
                    UserName = DefaultData.AdminEmail,
                    NormalizedUserName = DefaultData.AdminEmail.ToUpper(),
                    PasswordHash = hasher.HashPassword(null, DefaultData.AdminPassword),
                    Email = DefaultData.AdminEmail,
                    NormalizedEmail = DefaultData.AdminEmail.ToUpper(),
                    FirstName = DefaultData.RandomName,
                    LastName = DefaultData.RandomName,
                    Patronymic = DefaultData.RandomName,
                });
            userRoles.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleGuid,
                    UserId = adminUserGuid,
                });
            for (int i = 0; i < instructorUserGuids.Length; i++)
            {
                string instructorUserGuid = instructorUserGuids[i];
                var mail = $"Instructor{i}@example.com";
                users.HasData(
                    new ApplicationUser
                    {
                        Id = instructorUserGuid,
                        UserName = mail,
                        NormalizedUserName = mail.ToUpper(),
                        PasswordHash = hasher.HashPassword(null, DefaultData.InstructorPassword),
                        Email = mail,
                        NormalizedEmail = mail.ToUpper(),
                        FirstName = DefaultData.RandomName,
                        LastName = DefaultData.RandomName,
                        Patronymic = DefaultData.RandomName,
                    });
                userRoles.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = instructorRoleGuid,
                        UserId = instructorUserGuid,
                    });
                instructors.HasData(
                    new Instructor()
                    {
                        UserId = instructorUserGuid,
                        InstructorId = i + 1
                    });
            }
            for (int i = 0; i < studentUserGuids.Length; i++)
            {
                string studentUserGuid = studentUserGuids[i];
                var mail = $"Student{i}@example.com";
                users.HasData(
                    new ApplicationUser
                    {
                        Id = studentUserGuid,
                        UserName = mail,
                        NormalizedUserName = mail.ToUpper(),
                        PasswordHash = hasher.HashPassword(null, DefaultData.InstructorPassword),
                        Email = mail,
                        NormalizedEmail = mail.ToUpper(),
                        FirstName = DefaultData.RandomName,
                        LastName = DefaultData.RandomName,
                        Patronymic = DefaultData.RandomName,
                    });
                userRoles.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = studentRoleGuid,
                        UserId = studentUserGuid,
                    });
                students.HasData(
                    new Student()
                    {
                        UserId = studentUserGuid,
                        StudentId = i + 1
                    });
            }
        }

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<GradeByInstructorToStudent> GradesByInstructorToStudent { get; set; }
        public DbSet<GradeByStudentToInstructor> GradesByStudentToInstructor { get; set; }
        public DbSet<InnerScheduleOfInstructor> InnerSchedulesOfInstructors { get; set; }
        public DbSet<OuterScheduleOfInstructor> OuterSchedulesOfInstructors { get; set; }
        public DbSet<Class> Classes { get; set; }

    }
}
