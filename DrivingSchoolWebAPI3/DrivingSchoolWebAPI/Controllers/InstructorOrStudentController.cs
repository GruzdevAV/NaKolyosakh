using Microsoft.AspNetCore.Mvc;
using DrivingSchoolAPIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DrivingSchoolWebAPI.DataSheetModels;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace DrivingSchoolWebAPI.Controllers
{
    /// <summary>
    /// Контроллер для входа в систему
    /// </summary>
    //[Route("api/[controller]")]
    [Authorize(Roles = $"{UserRoles.Student}, {UserRoles.Instructor}")]
    [Route("api")]
    [ApiController]
    public class InstructorOrStudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InstructorOrStudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Установить занятие
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(SetClass))]
        public async Task<ActionResult<Response<ClassStudentPairModel>>> SetClass(ClassStudentPairModel model)
        {
            try
            {
                var @class = await _context.Classes
                    .Include(x => x.InnerScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor.User)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor.Instructor.User)
                    .Include(x => x.Student)
                    .Include(x => x.Student.User)
                    .Include(x => x.Student.Instructor)
                    .Include(x => x.Student.Instructor.User)
                    .Where(x => x.ClassId == model.ClassId)
                    .FirstAsync();
                if (@class == null)
                    return NotFound(new Response
                    {
                        Message = "Занятие с таким id не найдено",
                        Status = "Failure"
                    });
                var student = await _context.Students
                    .Where(x => x.StudentId == model.StudentId)
                    .FirstAsync();
                if (@class == null)
                    return NotFound(new Response
                    {
                        Message = "Ученик с таким id не найден",
                        Status = "Failure"
                    });
                @class.StudentId = model.StudentId;
                var cl = _context.Classes.Update(@class);
                await _context.SaveChangesAsync();
                @class = cl.Entity;
                if (@class.IsOuterClass)
                {
                    @class.Student = await _context.Students
                        .Include(x => x.User)
                        .Include(x => x.Instructor)
                        .Include(x => x.Instructor.User)
                        .Where(x => x.StudentId == @class.StudentId)
                        .FirstAsync();
                    bool isMarked = Methods.MakeSureOuterMarked(@class, DefaultData.ApiClient);
                }
                return Ok(new Response<ClassStudentPairModel>
                {
                    Message = "OK",
                    Status = "OK",
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
        /// Отменить занятие
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(CancelClass))]
        public async Task<ActionResult<int>> CancelClass([FromBody] int classId)
        {
            try
            {
                var @class = await _context.Classes
                    .Include(x => x.InnerScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.Instructor.User)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor.Instructor)
                    .Include(x => x.InnerScheduleOfInstructor.OuterScheduleOfInstructor.Instructor.User)
                    .Include(x => x.Student)
                    .Include(x => x.Student.User)
                    .Include(x => x.Student.Instructor)
                    .Include(x => x.Student.Instructor.User)
                    .Where(x => x.ClassId == classId)
                    .FirstAsync();
                if (@class == null) return NotFound(new Response
                {
                    Message = "Занятие с таким id не найдено",
                    Status = "Failure"
                });
                @class.StudentId = null;
                //@class.Status = ClassStatus.Отменено;
                var cl = _context.Classes.Update(@class);
                await _context.SaveChangesAsync();
                @class = cl.Entity;
                await _context.SaveChangesAsync();
                if (@class.IsOuterClass)
                {
                    bool isMarked = Methods.MakeSureOuterMarked(@class, DefaultData.ApiClient);
                }
                return Ok(new Response<int>
                {
                    Message = "OK",
                    Status = "OK",
                    Package = classId
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
