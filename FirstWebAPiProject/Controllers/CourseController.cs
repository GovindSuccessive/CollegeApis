using FirstClassLibrary;
using FirstClassLibrary.Entity;
using FirstWebAPiProject.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPiProject.Controllers
{
    [ApiController]
    [Route("api/CourseApi")]
    public class CourseController : ControllerBase
    {
        private readonly DataContext dataContext;

        public CourseController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Course>> GetCourseList()
        {
            return Ok(dataContext.Courses);
        }

        [HttpGet("id:int", Name = "GetCourse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Course> GetCourseById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var Course = dataContext.Courses.FirstOrDefault(x => x.Id == id);
            if (Course == null)
            {
                return NotFound();
            }

            return Ok(Course);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CourseDto> CreateCourse([FromBody] CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest(courseDto);
            }
            //student.Id = StudentStore.studentList.OrderByDescending(x => x.Id).FirstOrDefault().Id+1;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var NewCourse = new Course()
            {
                //if (dataContext.Students != null)
                //Id = dataContext.Students.OrderByDescending(x => x.Id).FirstOrDefault(0).Id + 1;


                Name = courseDto.Name,
                Description = courseDto.Description,
            };

            dataContext.Courses.Add(NewCourse);
            dataContext.SaveChanges();
            return Ok();
        }
    }
}
