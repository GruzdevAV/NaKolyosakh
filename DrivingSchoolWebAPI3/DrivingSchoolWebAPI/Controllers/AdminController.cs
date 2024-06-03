using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DrivingSchoolAPIModels;
using System.Text;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для администратора
    /// </summary>
    [Authorize(Roles = $"{UserRoles.Admin}")]
    [Route("api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        /// <summary>
        /// Получить список пользователей
        /// </summary>
        [HttpGet]
        [Route(nameof(GetUsers))]
        public async Task<ActionResult<Response<IEnumerable<ApplicationUser>>>> GetUsers()
        {
            try
            {
                if (_context.Users == null)
                {
                    return StatusCode(
                        StatusCodes.Status404NotFound,
                        new Response
                        {
                            Message = "Not found",
                            Status = "Failure"
                        }
                        );
                }
                return new Response<IEnumerable<ApplicationUser>>
                {
                    Status = "OK",
                    Message = "OK",
                    Package = await _context.Users.ToListAsync()
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
        /// Зарегистрировать ученика
        /// </summary>
        [HttpPost]
        [Route(nameof(RegisterStudent))]
        public async Task<ActionResult<Response<Student>>> RegisterStudent([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<Student>
                {
                    Status = "Error",
                    Message = "Пользователь уже существует!",
                    Package = await _context.Students
                    .Where(x => x.UserId == userExists.Id)
                    .Include(x => x.User)
                    .Include(x => x.Instructor)
                    .Include(x => x.Instructor.User)
                    .FirstAsync()
                });

            ApplicationUser user = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var err in result.Errors)
                    sb.AppendJoin('\n', err.Description);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<Student> { Status = "Error", Message = $"{sb}", Package = null });
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Student);
            var student = new Student
            {
                UserId = user.Id,
                User = user,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic
            };
            // Добавление студента в базу данных
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return Ok(new Response<Student>
            {
                Status = "Success",
                Message = "Пользователь создан успешно!",
                Package = student
            });
        }
        /// <summary>
        /// Зарегистрировать инструктора
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(RegisterInstructor))]
        public async Task<ActionResult<Response<Instructor>>> RegisterInstructor([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<Instructor>
                    {
                        Status = "Error",
                        Message = "Пользователь уже существует!",
                        Package = await _context.Instructors
                    .Where(x => x.UserId == userExists.Id)
                    .Include(x => x.User)
                    .FirstAsync()
                    });

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = model.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var err in result.Errors)
                    sb.AppendJoin('\n', err.Description);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"{sb}" });
            }
            await _userManager.AddToRoleAsync(user, UserRoles.Instructor);
            var instructor = new Instructor
            {
                UserId = user.Id,
                User = user,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic
            };
            // Добавление инструктора в базу данных
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();

            return Ok(new Response<Instructor>
            {
                Status = "Success",
                Message = "Пользователь создан успешно!",
                Package = instructor
            });
        }
        /// <summary>
        /// Назначить инструктора ученику
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(SetInstructorToStudent))]
        public async Task<ActionResult<Response<InstructorStudentPairModel>>> SetInstructorToStudent(InstructorStudentPairModel model)
        {
            try
            {
                var student = await _context.Students.FindAsync(model.StudentId);
                var instructor = await _context.Instructors.FindAsync(model.InstructorId);
                if (student == null || instructor == null)
                    return NotFound(new Response
                    {
                        Status = "Failure",
                        Message = $"Студент или инструктор не найдены."
                    });
                student.Instructor = instructor;
                student.InstructorId = model.InstructorId;
                await _context.SaveChangesAsync();
                return Ok(new Response<InstructorStudentPairModel>
                {
                    Status = "Success",
                    Message = "Ученик назначен успешно",
                    Package = model
                });

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
        /// Установить внутреннее расписание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(SetInnerSchedule))]
        public async Task<ActionResult<Response>> SetInnerSchedule(InstructorScheduleModel model)
        {
            try
            {
                var instructor = await _context.Instructors.FindAsync(model.InstructorId);
                if (instructor == null)
                    return NotFound(new Response
                    {
                        Status = "Failure",
                        Message = "Инструктор не найден."
                    });
                var schedule = new InnerScheduleOfInstructor
                {
                    InstructorId = model.InstructorId,
                    DayOfWork = model.DayOfWork,
                };
                // Проверка на наличие расписания у текущего инструктора, которое совпадает дню с новым
                var schedules = await _context.InnerSchedulesOfInstructors
                    .Include(x => x.Instructor)
                    .Include(x => x.Instructor.User)
                    .Where(x => x.InstructorId == schedule.InstructorId)
                    .Where(x => x.DayOfWork == schedule.DayOfWork)
                    .ToListAsync();
                // Если есть хоть 1, то это ошибка
                if (schedules.Count > 0)
                    return BadRequest(new Response
                    {
                        Status = "Failure",
                        Message = "У данного инструктора уже есть расписание, занимающее этот день."
                    });
                var t = await _context.InnerSchedulesOfInstructors.AddAsync(schedule);
                await _context.SaveChangesAsync();
                schedule = t.Entity;
                var classes = new List<Class>();
                foreach (var c in model.Classes)
                {
                    classes.Add(
                        new Class
                        {
                            InnerScheduleOfInstructorId = schedule.InnerScheduleOfInstructorId,
                            StartTime = TimeOnly.FromTimeSpan(c.StartTime),
                            Duration = c.Duration,
                        });
                }
                await _context.Classes.AddRangeAsync(classes);
                await _context.SaveChangesAsync();
                return Ok(new Response<InnerScheduleOfInstructorModel>
                {
                    Status = "Success",
                    Message = "Расписание создано успешно",
                    Package = new InnerScheduleOfInstructorModel(schedule, classes)
                });

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
