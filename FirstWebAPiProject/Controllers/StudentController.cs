using FirstClassLibrary;
using FirstClassLibrary.Entity;
using FirstWebAPiProject.Data;
using FirstWebAPiProject.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPiProject.Controllers
{
    [ApiController]
    [Route("api/StudentApi")]
    public class StudentController:ControllerBase
    {
        private readonly DataContext dataContext;

        public StudentController(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDto>> GetStudentList()
        {
            return Ok(dataContext.Students);
        }

        [HttpGet("id:int",Name ="GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StudentDto> GetStudentList(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var studnet = dataContext.Students.FirstOrDefault(x => x.Id == id);
            if(studnet == null)
            {
                return NotFound();
            }

            return Ok(studnet);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDto> CreateStudent([FromBody]StudentDto student)
        {
            if (student == null)
            {
                return BadRequest(student);
            }
            if(student.Id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //student.Id = StudentStore.studentList.OrderByDescending(x => x.Id).FirstOrDefault().Id+1;

            var studentabc = new Student()
            {
                Name = student.Name,
                PhoneNo = student.PhoneNo,
            };

            dataContext.Students.Add(studentabc);
            dataContext.SaveChanges();
            return CreatedAtRoute("GetStudent",new { id = student.Id } , student);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeleteStudent(int id) { 
            if(id<0)
            {
                return BadRequest();
            }
            var student = dataContext.Students.FirstOrDefault(x=>x.Id==id);
            if(student == null)
            {
                return NotFound(student);
            }
            dataContext.Students.Remove(student);
            dataContext.SaveChanges();
            return Ok(student); 
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult UpdateStudent([FromBody] StudentDto student)
        {
            if (student.Id < 0)
            {
                return BadRequest();
            }
            var Existingstudent = dataContext.Students.FirstOrDefault(x => x.Id == student.Id);
            if (Existingstudent == null)
            {
                return NotFound(Existingstudent);
            }
            Existingstudent.Name=student.Name;
            Existingstudent.PhoneNo=student.PhoneNo;
            dataContext.Students.Add(Existingstudent);
            dataContext.SaveChanges();
            return Ok(student);
        }
    }
}
