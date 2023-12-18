using FirstClassLibrary;
using FirstWebAPiProject.Data;
using FirstWebAPiProject.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPiProject.Controllers
{
    [ApiController]
    [Route("api/StudentApi")]
    public class StudentController:ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDto>> GetStudentList()
        {
            return Ok(StudentStore.studentList);
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
            var studnet = StudentStore.studentList.FirstOrDefault(x => x.Id == id);
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
            if(student.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            student.Id = StudentStore.studentList.OrderByDescending(x => x.Id).FirstOrDefault().Id+1;
            StudentStore.studentList.Add(student);

            return CreatedAtRoute("GetStudent",new { id = student.Id } , student);
        }
    }
}
