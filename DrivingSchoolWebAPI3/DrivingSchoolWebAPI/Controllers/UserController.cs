using Microsoft.AspNetCore.Mvc;
using DrivingSchoolAPIModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для всех зарегистрированных пользователей
    /// </summary>
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить список всех инструкторов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetInstructors))]
        public async Task<ActionResult<Response<IEnumerable<Instructor>>>> GetInstructors()
        {
            return new Response<IEnumerable<Instructor>>
            {
                Status = "OK",
                Message = "OK",
                Package = await _context.Instructors
                .Include(x => x.User)
                .ToListAsync()
            };
        }
        /// <summary>
        /// Получить список всех студентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetStudents))]
        public async Task<ActionResult<Response<IEnumerable<Student>>>> GetStudents()
        {
            return new Response<IEnumerable<Student>>
            {
                Status = "OK",
                Message = "OK",
                Package = await _context.Students
                .Include(x => x.User)
                .Include(x => x.Instructor)
                .Include(x => x.Instructor.User)
                .ToListAsync()
            };

        }
        /// <summary>
        /// Получить список всех инструкторов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetInstructor))]
        public async Task<ActionResult<Response<Instructor>>> GetInstructor(int instructorId)
        {
            if (_context.Instructors == null)
                return new Response<Instructor>
                {
                    Status = "OK",
                    Message = "Not found",
                    Package = null
                };

            return new Response<Instructor>
            {
                Status = "OK",
                Message = "OK",
                Package = await _context.Instructors
                .Include(x => x.User)
                .Where(x => x.InstructorId == instructorId)
                .FirstAsync()
            };
        }
        /// <summary>
        /// Получить список всех студентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetStudent))]
        public async Task<ActionResult<Response<Student>>> GetStudent(int studentId)
        {
            return new Response<Student>
            {
                Status = "OK",
                Message = "OK",
                Package = await _context.Students
                .Include(x => x.User)
                .Include(x => x.Instructor)
                .Include(x => x.Instructor.User)
                .Where(x => x.StudentId == studentId)
                .FirstAsync()
            };
        }
        /// <summary>
        /// Получить список всех занятий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetClasses))]
        public async Task<ActionResult<Response<IEnumerable<Class>>>> GetClasses()
        {
            try
            {
                return new Response<IEnumerable<Class>>
                {
                    Status = "OK",
                    Message = "OK",
                    Package = await _context.Classes
                    .Include(x => x.Student)
                    .Include(x => x.Student.User)
                    .Include(x => x.Student.Instructor)
                    .Include(x => x.Student.Instructor.User)
                    .Include(x => x.InnerScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor.User)
                    .ToListAsync()
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Class>>
                {
                    Message = ex.Message,
                    Status = "Failure",
                    Package = null
                };
            }
        }
        /// <summary>
        /// Получить занятие по id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetClass))]
        public async Task<ActionResult<Response<Class>>> GetClass(int classId)
        {
            try
            {
                return new Response<Class>
                {
                    Status = "OK",
                    Message = "OK",
                    Package = await _context.Classes
                    .Include(x => x.Student)
                    .Include(x => x.Student.User)
                    .Include(x => x.Student.Instructor)
                    .Include(x => x.Student.Instructor.User)
                    .Include(x => x.InnerScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor.User)
                    .Where(x => x.ClassId == classId)
                    .FirstAsync()
                };
            }
            catch (Exception ex)
            {
                return new Response<Class>
                {
                    Message = ex.Message,
                    Status = "Failure",
                    Package = null
                };
            }
        }
        /// <summary>
        /// Получить список всех расписаний
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetInnerSchedules))]
        public async Task<ActionResult<Response<IEnumerable<InnerScheduleOfInstructor>>>> GetInnerSchedules()
        {
            try
            {
                if (_context.InnerSchedulesOfInstructors == null)
                    return new Response<IEnumerable<InnerScheduleOfInstructor>>
                    {
                        Status = "OK",
                        Message = "Not found",
                        Package = null
                    };

                return new Response<IEnumerable<InnerScheduleOfInstructor>>
                {
                    Status = "OK",
                    Message = "OK",
                    Package = await _context.InnerSchedulesOfInstructors
                    .Include(x => x.Instructor)
                    .Include(x => x.Instructor.User)
                    .ToListAsync()
                };
            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = e.Message
                });
            }

        }
        /// <summary>
        /// Получить список инструкторов с рейтингом
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetInstructorRatings))]
        public async Task<ActionResult<Response<IEnumerable<InstructorRating>>>> GetInstructorRatings()
        {
            try
            {
                var instructorRatings = (from instructor in _context.Instructors
                                         join user in _context.Users on instructor.UserId equals user.Id
                                         join instructorSchedule in _context.InnerSchedulesOfInstructors on instructor.InstructorId equals instructorSchedule.InstructorId into schedules
                                         from schedule in schedules.DefaultIfEmpty()
                                         join classObj in _context.Classes on schedule.InnerScheduleOfInstructorId equals classObj.InnerScheduleOfInstructorId into classes
                                         from classItem in classes.DefaultIfEmpty()
                                         join grade in _context.GradesByStudentToInstructor on classItem.ClassId equals grade.ClassId into grades
                                         from gradeItem in grades.DefaultIfEmpty()
                                         group new { instructor, gradeItem, user } by new
                                         {
                                             instructor.InstructorId,
                                             instructor.UserId,
                                             user.FirstName,
                                             user.LastName,
                                             user.Patronymic,
                                             user.Email,
                                             user.PhoneNumber,
                                             user.ProfilePictureBytes
                                         } into g
                                         select new InstructorRating
                                         {
                                             InstructorId = g.Key.InstructorId,
                                             UserId = g.Key.UserId,
                                             Grade = g.Average(x => (float?)x.gradeItem.Grade) ?? 0.0f,
                                             NumberOfGrades = g.Count(x => x.gradeItem != null),
                                             User = new ApplicationUser
                                             {
                                                 Email = g.Key.Email,
                                                 PhoneNumber = g.Key.PhoneNumber,
                                                 FirstName = g.Key.FirstName,
                                                 LastName = g.Key.LastName,
                                                 Patronymic = g.Key.Patronymic,
                                                 ProfilePictureBytes = g.Key.ProfilePictureBytes
                                             }
                                         });
                if (instructorRatings == null)
                    return new Response<IEnumerable<InstructorRating>>
                    {
                        Status = "OK",
                        Message = "Not found",
                        Package = null
                    };

                return new Response<IEnumerable<InstructorRating>>
                {
                    Status = "OK",
                    Message = "OK",
                    Package = await instructorRatings
                    .ToListAsync()
                };
            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = e.Message
                });
            }

        }
        /// <summary>
        /// Получить список учеников с рейтингом
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetStudentRatings))]
        public async Task<ActionResult<Response<IEnumerable<StudentRating>>>> GetStudentRatings([FromBody] string? instructorId=null)
        {
            try
            {
                var groupedGradeses = await _context.Database.SqlQueryRaw<string>(
                    "select cast(students.studentid as NVARCHAR(MAX))+';'+CAST(count(grade) as NVARCHAR(MAX))+';'+CAST(coalesce(avg(grade),0.0) as NVARCHAR(MAX)) " +
                    " from students left join classes on students.studentid = classes.studentid " +
                    " left join GradesByInstructorToStudent on classes.classid = GradesByInstructorToStudent.classid " +
                    " group by students.studentid ")
                    .ToListAsync();
                var students = await _context.Students
                    .Include(x => x.User)
                    .Include(x => x.Instructor)
                    .Include(x => x.Instructor.User)
                    .Where(x => instructorId == null || x.Instructor.UserId == instructorId)
                    .ToListAsync();
                var dict = new Dictionary<int, (int count, float avg)>();
                foreach (var w in groupedGradeses)
                {
                    if (w == null) continue;
                    var values = w.Split(';');
                    var studentId = int.Parse(values[0]);
                    var count = int.Parse(values[1]);
                    var avg = float.Parse(values[2].Replace('.', ','));
                    dict[studentId] = (count, avg);
                }
                var studentRatings = new List<StudentRating>();
                foreach (var student in students)
                {
                    if (!dict.TryGetValue(student.StudentId, out var rating))
                        rating = (0, 0);
                    studentRatings.Add(StudentRating.FromStudent(student, rating.count, rating.avg));
                }
                return new Response<IEnumerable<StudentRating>>
                {
                    Status = "OK",
                    Message = "OK",
                    Package = studentRatings
                };
            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = e.Message
                });
            }
        }
        /// <summary>
        /// Получить данные о себе
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetMe))]
        public async Task<ActionResult<Response<ApplicationUser>>> GetMe([FromBody] string userId)
        {
            try
            {
                // Найти себя
                var user = await _context.Users.Where(x => x.Id == userId).FirstAsync();
                if (user == null) return NotFound(new Response
                {
                    Status = "Failure",
                    Message = "Вас нет"
                });
                return new Response<ApplicationUser>
                {
                    Package = user,
                    Status = "OK",
                    Message = "OK"
                };
            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = e.Message
                });
            }

        }
        /// <summary>
        /// Изменить данные о себе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditMe")]
        public async Task<ActionResult<Response<ApplicationUser>>> EditMe([FromBody] EditMeModel model)
        {
            try
            {
                // Найти себя
                var user = await _context.Users
                    .FindAsync(model.Id);
                if (user == null) return NotFound(new Response
                {
                    Status = "Failure",
                    Message = "Вас нет"
                });
                if (model.EmailAddress != null) user.Email = model.EmailAddress;
                if (model.PhoneNumber != null) user.PhoneNumber = model.PhoneNumber;
                if (model.LastName != null) user.LastName = model.LastName;
                if (model.FirstName != null) user.FirstName = model.FirstName;
                user.Patronymic = model.Patronymic;
                user.ProfilePictureBytes = model.ProfilePictureBytes;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = e.Message
                });
            }

        }
        /// <summary>
        /// Установить себе картинку
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(SetMeImage))]
        public async Task<ActionResult<Response>> SetMeImage([FromBody] SetMeImageModel model)
        {
            try
            {
                // Найти себя
                var user = await _context.Users
                    .FindAsync(model.Id);
                if (user == null) return NotFound(new Response
                {
                    Status = "Failure",
                    Message = "Вас нет"
                });
                user.ProfilePictureBytes = model.ImageBytes;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new Response
                {
                    Status = "Failure",
                    Message = e.Message
                });
            }

        }
    }
}
