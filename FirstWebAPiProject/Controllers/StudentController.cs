using FirstClassLibrary;
using FirstClassLibrary.Entity;
using FirstWebAPiProject.Logger;
using FirstWebAPiProject.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebAPiProject.Controllers
{
    [ApiController]
    [Route("api/StudentApi")]
    public class StudentController : ControllerBase
    {
        private readonly DataContext dataContext;
        private readonly IMyLogger _myLogger;

        public StudentController(DataContext dataContext, IMyLogger myLogger) {
            this.dataContext = dataContext;
            _myLogger = myLogger;
        }

      /*  public StudentController(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }*/

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentList()
        {
            var student = await dataContext.Students.ToListAsync();
            return Ok(student);
        }

        [HttpGet("id:int", Name = "GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Student>> GetStudentList(int id)
        {
            if (id > dataContext.Students.OrderByDescending(x=>x.Id).First().Id || id < 0)
            {
                _myLogger.Log("Id is Invalid while Getting Student By Id");
                return BadRequest("Id is not exist");
            }
            var student = await dataContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                _myLogger.Log("Student is null");
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] StudentDto student)
        {
            if (student == null)
            {
                return BadRequest(student);
            }
            //student.Id = StudentStore.studentList.OrderByDescending(x => x.Id).FirstOrDefault().Id+1;
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var NewStudent = new Student()
            {
               //if (dataContext.Students != null)
               //Id = dataContext.Students.OrderByDescending(x => x.Id).FirstOrDefault(0).Id + 1;


                Name = student.Name,
                PhoneNo = student.PhoneNo,
                Password = student.Password,
                ConfirmPassword = student.ConfirmPassword,
                Age = student.Age
            };

            dataContext.Students.Add(NewStudent);
            await dataContext.SaveChangesAsync();
            return Ok("Student is added Successfully");
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteStudent(int id) {
            if (id > dataContext.Students.OrderByDescending(x=>x.Id).First().Id)
            {
                return BadRequest("Id is not exist");
            }
            var student =  dataContext.Students.FirstOrDefault(x=>x.Id == id);
            if (student == null)
            {
                return NotFound(student);
            }
            dataContext.Students.Remove(student);
            await dataContext.SaveChangesAsync();
            return Ok("Student is deleted Successfully");
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateStudent(int id,[FromBody] Student student)
        {
            if (student.Id < 0)
            {
                return BadRequest();
            }
            var Existingstudent = dataContext.Students.FirstOrDefault(x => x.Id == id);
            if (Existingstudent == null)
            {
                return NotFound(Existingstudent);
            }
            Existingstudent.Name=student.Name;
            Existingstudent.PhoneNo=student.PhoneNo;
            await dataContext.SaveChangesAsync();
            return Ok("Student is updated Successfully");
        }
    }
}
 