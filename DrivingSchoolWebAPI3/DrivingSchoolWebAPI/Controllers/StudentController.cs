using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DrivingSchoolAPIModels;
using Microsoft.EntityFrameworkCore.Internal;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для учеников
    /// </summary>
    [Authorize(Roles = $"{UserRoles.Student}")]
    [Route("api")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получить своего инструктора
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetMyInstructor))]
        public async Task<ActionResult<Response<Instructor>>> GetMyInstructor([FromBody] string studentUserId)
        {
            try
            {
                // Найти ученика по Id
                var student = await _context.Students
                    .Where(x => x.User.Id == studentUserId)
                    .FirstAsync();
                if (student == null)
                    return NotFound(new Response
                    {
                        Status = "Failure",
                        Message = "Ученик с таким id не найден"
                    });
                if (student.InstructorId == null)
                    return Ok(new Response<Instructor>
                    {
                        Message = "Not found",
                        Status = "OK",
                        Package = null
                    });
                // Вернуть инструктора по Id инструктора, указанного у студента
                return Ok(new Response<Instructor>
                {
                    Message = "OK",
                    Status = "OK",
                    Package = await _context.Instructors
                    .Include(x => x.User)
                    .Where(x => x.InstructorId == student.InstructorId)
                    .FirstAsync()
                });
            }
            catch (Exception e)
            {
                return BadRequest(new Response<Instructor>
                {
                    Status = "Failure",
                    Message = e.Message,
                    Package = null
                });
            }
        }
        /// <summary>
        /// Найти свои занятия
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetClassesOfStudent))]
        public async Task<ActionResult<Response<IEnumerable<Class>>>> GetClassesOfStudent([FromBody] string studentUserId)
        {
            try
            {
                var instructor = (await GetMyInstructor(studentUserId)).Value?.Package;
                if (instructor == null)
                    return Ok(new Response<IEnumerable<Class>>
                    {
                        Message = "Not found",
                        Status = "OK",
                        Package = new List<Class>()
                    });
                return Ok(new Response<IEnumerable<Class>>
                {
                    Message = "OK",
                    Status = "OK",
                    Package = await _context.Classes
                    .Include(x => x.Student)
                    .Include(x => x.Student.User)
                    .Include(x => x.Student.Instructor)
                    .Include(x => x.Student.Instructor.User)
                    .Include(x => x.InnerScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor.User)
                    .Where(x => x.InnerScheduleOfInstructor.Instructor.InstructorId == instructor.InstructorId)
                    .ToListAsync()
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
        /// Найти занятия своего инструктора
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetClassesOfMyInstructor))]
        public async Task<ActionResult<Response<IEnumerable<Class>>>> GetClassesOfMyInstructor([FromBody] string studentUserId)
        {
            try
            {
                // Найти своего инструктора
                var instructor = (await GetMyInstructor(studentUserId)).Value?.Package;
                if (_context.Classes == null ||
                    instructor == null)
                    return Ok(new Response<IEnumerable<Class>>
                    {
                        Message = "Not found",
                        Status = "OK",
                        Package = new List<Class>()
                    });
                // Вернуть список занятий инструктора
                return Ok(new Response<IEnumerable<Class>>
                {
                    Message = "OK",
                    Status = "OK",
                    Package = await _context.Classes
                    .Include(x => x.Student)
                    .Include(x => x.Student.User)
                    .Include(x => x.Student.Instructor)
                    .Include(x => x.Student.Instructor.User)
                    .Include(x => x.InnerScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor.Instructor.User)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor.User)
                    .Where(x => x.InnerScheduleOfInstructor.InstructorId == instructor.InstructorId)
                    .ToListAsync()
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
        /// Получить расписание своего инструктора
        /// </summary>
        /// <param name="studentUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetInnerSchedulesOfMyInstructor))]
        public async Task<ActionResult<Response<IEnumerable<InnerScheduleOfInstructor>>>> GetInnerSchedulesOfMyInstructor([FromBody] string studentUserId)
        {
            try
            {
                // Найти своего инструктора
                var instructor = (await GetMyInstructor(studentUserId)).Value?.Package;
                if (instructor == null || instructor == null)
                    return NotFound(new Response<IEnumerable<InnerScheduleOfInstructor>>
                    {
                        Message = "Not found",
                        Status = "OK",
                        Package = new List<InnerScheduleOfInstructor>()
                    });
                // Вернуть список расписаний инструктора
                return Ok(new Response<IEnumerable<InnerScheduleOfInstructor>>
                {
                    Message = "OK",
                    Status = "OK",
                    Package = await _context.InnerSchedulesOfInstructors
                    .Where(x => x.Instructor.InstructorId == instructor.InstructorId)
                    .Include(x => x.Instructor)
                    .Include(x => x.Instructor.User)
                    .Include(x => x.OuterScheduleOfInstructor)
                    .Include(x => x.OuterScheduleOfInstructor.Instructor)
                    .Include(x => x.OuterScheduleOfInstructor.Instructor.User)
                    .ToListAsync()
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
        /// Поставить оценку инструктору за занятие
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(PostGradeToInstructorForClass))]
        public async Task<ActionResult<Response<GradeByStudentToInstructor>>> PostGradeToInstructorForClass(GradeByStudentToInstructorModel model)
        {
            try
            {
                var grade = new GradeByStudentToInstructor
                {
                    ClassId = model.ClassId,
                    Grade = model.Grade,
                    Comment = model.Comment
                };
                if (await _context.GradesByStudentToInstructor.AnyAsync(x => x.ClassId == grade.ClassId))
                    return BadRequest(new Response
                    {
                        Status = "Failed",
                        Message = "Отметка за данное занятие уже была поставлена."
                    });
                var @class = await _context.Classes.FindAsync(grade.ClassId);
                if (@class.Status != ClassStatus.Завершено)
                    return BadRequest(new Response
                    {
                        Status = "Failed",
                        Message = "Студентам нельзя ставить отметку занятию, которое ещё не завершилось или было отменено."
                    });
                var addedGrade = await _context.GradesByStudentToInstructor.AddAsync(grade);
                await _context.SaveChangesAsync();
                return Ok(new Response<GradeByStudentToInstructor>
                {
                    Message = "OK",
                    Status = "OK",
                    Package = addedGrade.Entity
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
        /// Получить оценки на меня
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetGradesToStudent))]
        public async Task<ActionResult<Response<IEnumerable<GradeByInstructorToStudent>>>> GetGradesToStudent([FromBody] string studentUserId)
        {
            return Ok(new Response<IEnumerable<GradeByInstructorToStudent>>
            {
                Message = "OK",
                Status = "OK",
                Package = await _context.GradesByInstructorToStudent
                .Include(x => x.Class)
                .Include(x => x.Class.Student)
                .Include(x => x.Class.Student.User)
                .Include(x => x.Class.Student.Instructor)
                .Include(x => x.Class.Student.Instructor.User)
                .Include(x => x.Class.InnerScheduleOfInstructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor.User)
                .Include(x => x.Class.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Where(x => x.Class.Student.User.Id == studentUserId)
                .ToListAsync()
            });
        }
        /// <summary>
        /// Получить оценки от меня
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(GetGradesByStudent))]
        public async Task<ActionResult<Response<IEnumerable<GradeByStudentToInstructor>>>> GetGradesByStudent([FromBody] string studentUserId)
        {
            return Ok(new Response<IEnumerable<GradeByStudentToInstructor>>
            {
                Message = "OK",
                Status = "OK",
                Package = await _context.GradesByStudentToInstructor
                .Include(x => x.Class)
                .Include(x => x.Class.Student)
                .Include(x => x.Class.Student.User)
                .Include(x => x.Class.Student.Instructor)
                .Include(x => x.Class.Student.Instructor.User)
                .Include(x => x.Class.InnerScheduleOfInstructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor)
                .Include(x => x.Class.InnerScheduleOfInstructor.Instructor.User)
                .Include(x => x.Class.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                .Where(x => x.Class.Student.User.Id == studentUserId)
                .ToListAsync()
            });
        }
    }
}
