using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrivingSchoolWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorId);
                    table.ForeignKey(
                        name: "FK_Instructors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OuterSchedulesOfInstructors",
                columns: table => new
                {
                    OuterScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    GoogleSheetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoogleSheetPageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesOfClassesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatesOfClassesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassesRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FreeClassExampleRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotFreeClassExampleRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearRange = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OuterSchedulesOfInstructors", x => x.OuterScheduleId);
                    table.ForeignKey(
                        name: "FK_OuterSchedulesOfInstructors_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId");
                });

            migrationBuilder.CreateTable(
                name: "InnerSchedulesOfInstructors",
                columns: table => new
                {
                    InnerScheduleOfInstructorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    DayOfWork = table.Column<DateOnly>(type: "date", nullable: false),
                    OuterScheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InnerSchedulesOfInstructors", x => x.InnerScheduleOfInstructorId);
                    table.ForeignKey(
                        name: "FK_InnerSchedulesOfInstructors_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InnerSchedulesOfInstructors_OuterSchedulesOfInstructors_OuterScheduleId",
                        column: x => x.OuterScheduleId,
                        principalTable: "OuterSchedulesOfInstructors",
                        principalColumn: "OuterScheduleId");
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InnerScheduleOfInstructorId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classes_InnerSchedulesOfInstructors_InnerScheduleOfInstructorId",
                        column: x => x.InnerScheduleOfInstructorId,
                        principalTable: "InnerSchedulesOfInstructors",
                        principalColumn: "InnerScheduleOfInstructorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "GradesByInstructorToStudent",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<byte>(type: "tinyint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradesByInstructorToStudent", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_GradesByInstructorToStudent_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradesByStudentToInstructor",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<byte>(type: "tinyint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradesByStudentToInstructor", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_GradesByStudentToInstructor_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28bd263f-58f2-4bad-a517-4e79408a1970", null, "Instructor", "INSTRUCTOR" },
                    { "cbd20502-86dd-40f3-b3fa-7cc68d0bd8ac", null, "Student", "STUDENT" },
                    { "f7a9e00d-7b9b-4af9-9028-7f6cb55503e4", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureBytes", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "37407b70-3493-426c-8a81-948f2f0c33f7", 0, "908f213e-675e-47f6-bf3c-eebc956413c6", "Instructor3@example.com", false, "Rodriguez", "Crawford", false, null, "INSTRUCTOR3@EXAMPLE.COM", "INSTRUCTOR3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEEvqjA9eKAMtpUwtJDK7jlSih41WvyWM3VhP76HbZciabu7c13fTeagWXl6IE2RCug==", "Williams", null, false, null, "d9d9fbe1-e0c1-4cd7-a47b-e55e59a3119f", false, "Instructor3@example.com" },
                    { "39b3c16e-dfa9-4d00-b09f-a91b8f5b14e0", 0, "59faeda6-1b7c-4d61-981f-3f055d0cc414", "Student0@example.com", false, "Eleanor", "Brooks", false, null, "STUDENT0@EXAMPLE.COM", "STUDENT0@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJC9++4wgSUaJsHxSBSBcWaqQpl09iNjIFFMPmXDjcqqoUU++vVNHP4iUNIM98ajJw==", "Timothy", null, false, null, "6c40611d-932c-4185-bcc7-802df343903a", false, "Student0@example.com" },
                    { "806b750e-093f-49c3-af63-23fe79b0877d", 0, "6333b999-eb3f-43d6-bb79-39cf491489d7", "admin@example.com", false, "Morales", "Angela", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDOjG137oMT7pGk3n/NuZwYFYc/fzhdQ5aTmT9EQIMh3A19iNKOBidh9PZSAr+c3EQ==", "Chambers", null, false, null, "6f781366-7010-469d-bb4b-5c0e6b7133c1", false, "admin@example.com" },
                    { "950fe809-938c-41df-9a77-1b2d5afabb04", 0, "62387a17-b61f-4a9e-811a-a799d7f8d81a", "Student4@example.com", false, "Elaine", "Smith", false, null, "STUDENT4@EXAMPLE.COM", "STUDENT4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHm9T1R4y5z76XzyFz54x0JqRS5awGEJ/1D9BKjnAoQkJsyIc/9ezo5P2u4TXPsQFg==", "Mattie", null, false, null, "e0bc52d8-dfb4-4950-bdce-ef80126f667a", false, "Student4@example.com" },
                    { "a85de3dd-97b0-4f6c-9a04-ef782e943242", 0, "9688bdd8-824a-4463-b90f-658ea32ba8ab", "Student2@example.com", false, "Morgan", "Charlotte", false, null, "STUDENT2@EXAMPLE.COM", "STUDENT2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEB/H8bp6EXLLAZYqC8konf6ulyXmYqKCR1u7G5H7k4VXCh/aYR410NQIn/cWvyNlvA==", "Morgan", null, false, null, "942f2a0a-039c-4b6b-8fe8-515a837c81eb", false, "Student2@example.com" },
                    { "b4d9d5ad-6365-46ab-9b1d-b572640dcada", 0, "2fd0e681-9dea-4692-8fa9-9f0675fc8005", "Instructor0@example.com", false, "Mattie", "Jeremy", false, null, "INSTRUCTOR0@EXAMPLE.COM", "INSTRUCTOR0@EXAMPLE.COM", "AQAAAAIAAYagAAAAENuxV3b+BFM/E4r8ndSbmzgkGnCh7Yf7GWVjj8h8MnspLLxcQH0+v9DSnOysf+WTgQ==", "Jeremy", null, false, null, "9024030f-2192-4f98-99cb-af55a7a503bb", false, "Instructor0@example.com" },
                    { "b6f86a9f-fc58-4aea-a223-085c61a919b9", 0, "3f14bdaa-e6ef-46d7-8638-87bdd2251755", "Instructor2@example.com", false, "Rodriguez", "Brown", false, null, "INSTRUCTOR2@EXAMPLE.COM", "INSTRUCTOR2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHFkpGeXa9ORnl4yub5P07L8sXlBBgnmI38UxyCMCAvlmzrX8BE8Z7N2llbVcXg3/g==", "Washington", null, false, null, "4bbdd35b-0a75-4816-8cc8-8938404821e7", false, "Instructor2@example.com" },
                    { "ce2e457d-d9df-4b14-8234-811080578b7e", 0, "f0131ba0-0f5e-46bd-bf63-08bda95cb1ac", "Instructor4@example.com", false, "Sara", "Jeremy", false, null, "INSTRUCTOR4@EXAMPLE.COM", "INSTRUCTOR4@EXAMPLE.COM", "AQAAAAIAAYagAAAAENcNfEJbg9hED04gPsXsHmPY1/dCYy5UsZtEXSHkK6ZIpJLEW21CBDuonODVihSdKA==", "Eleanor", null, false, null, "6a5b5185-b499-4280-94cc-9997db78e56b", false, "Instructor4@example.com" },
                    { "f04a501b-78f0-43b3-a00c-dabe9039df55", 0, "1ee3091c-2f98-49e1-9ba2-0eb509ad3b6f", "Instructor1@example.com", false, "Mattie", "Timothy", false, null, "INSTRUCTOR1@EXAMPLE.COM", "INSTRUCTOR1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKsRSiZ7CinmGLYpRU91oxBK7bIuTasO2d9YRfBpZvmW1ry0mNPrkAMnRsbLFAipDg==", "Amy", null, false, null, "48ef1693-a5ae-4e4d-89ec-9b7cac575d20", false, "Instructor1@example.com" },
                    { "fd488461-92e4-4cff-80da-f2da167b829b", 0, "42eefae5-3700-4252-9f87-c6ea1ebf3294", "Student1@example.com", false, "Mattie", "Jones", false, null, "STUDENT1@EXAMPLE.COM", "STUDENT1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBj4+RfLe9wGysmKGi+oQ8c/U1VgLReJmDrVa9+qtPsn/SGs9Ciy/ocJvL9IkgLGdA==", "Nelson", null, false, null, "b3059945-3855-406d-bbcc-8d20bd630393", false, "Student1@example.com" },
                    { "fd6c2aa5-52a0-47af-868f-008ca6cabd9f", 0, "bd923892-2569-44f4-b107-6e5cde676f02", "Student3@example.com", false, "Morgan", "Vivian", false, null, "STUDENT3@EXAMPLE.COM", "STUDENT3@EXAMPLE.COM", "AQAAAAIAAYagAAAAECUcETSKibs5v/R9o9bNmJiOa0+n9uW7KF7Te2ML28SoQcsfKGVZHzABFLf4K2k/Jg==", "Sara", null, false, null, "369e7e9e-2332-4e1d-8ac0-796bb330e441", false, "Student3@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "28bd263f-58f2-4bad-a517-4e79408a1970", "37407b70-3493-426c-8a81-948f2f0c33f7" },
                    { "cbd20502-86dd-40f3-b3fa-7cc68d0bd8ac", "39b3c16e-dfa9-4d00-b09f-a91b8f5b14e0" },
                    { "f7a9e00d-7b9b-4af9-9028-7f6cb55503e4", "806b750e-093f-49c3-af63-23fe79b0877d" },
                    { "cbd20502-86dd-40f3-b3fa-7cc68d0bd8ac", "950fe809-938c-41df-9a77-1b2d5afabb04" },
                    { "cbd20502-86dd-40f3-b3fa-7cc68d0bd8ac", "a85de3dd-97b0-4f6c-9a04-ef782e943242" },
                    { "28bd263f-58f2-4bad-a517-4e79408a1970", "b4d9d5ad-6365-46ab-9b1d-b572640dcada" },
                    { "28bd263f-58f2-4bad-a517-4e79408a1970", "b6f86a9f-fc58-4aea-a223-085c61a919b9" },
                    { "28bd263f-58f2-4bad-a517-4e79408a1970", "ce2e457d-d9df-4b14-8234-811080578b7e" },
                    { "28bd263f-58f2-4bad-a517-4e79408a1970", "f04a501b-78f0-43b3-a00c-dabe9039df55" },
                    { "cbd20502-86dd-40f3-b3fa-7cc68d0bd8ac", "fd488461-92e4-4cff-80da-f2da167b829b" },
                    { "cbd20502-86dd-40f3-b3fa-7cc68d0bd8ac", "fd6c2aa5-52a0-47af-868f-008ca6cabd9f" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "UserId" },
                values: new object[,]
                {
                    { 1, "b4d9d5ad-6365-46ab-9b1d-b572640dcada" },
                    { 2, "f04a501b-78f0-43b3-a00c-dabe9039df55" },
                    { 3, "b6f86a9f-fc58-4aea-a223-085c61a919b9" },
                    { 4, "37407b70-3493-426c-8a81-948f2f0c33f7" },
                    { 5, "ce2e457d-d9df-4b14-8234-811080578b7e" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "InstructorId", "UserId" },
                values: new object[,]
                {
                    { 1, null, "39b3c16e-dfa9-4d00-b09f-a91b8f5b14e0" },
                    { 2, null, "fd488461-92e4-4cff-80da-f2da167b829b" },
                    { 3, null, "a85de3dd-97b0-4f6c-9a04-ef782e943242" },
                    { 4, null, "fd6c2aa5-52a0-47af-868f-008ca6cabd9f" },
                    { 5, null, "950fe809-938c-41df-9a77-1b2d5afabb04" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InnerScheduleOfInstructorId",
                table: "Classes",
                column: "InnerScheduleOfInstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_StudentId",
                table: "Classes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerSchedulesOfInstructors_InstructorId",
                table: "InnerSchedulesOfInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_InnerSchedulesOfInstructors_OuterScheduleId",
                table: "InnerSchedulesOfInstructors",
                column: "OuterScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_UserId",
                table: "Instructors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OuterSchedulesOfInstructors_InstructorId",
                table: "OuterSchedulesOfInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstructorId",
                table: "Students",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GradesByInstructorToStudent");

            migrationBuilder.DropTable(
                name: "GradesByStudentToInstructor");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "InnerSchedulesOfInstructors");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "OuterSchedulesOfInstructors");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
