using FirstClassLibrary;
using FirstClassLibrary.Entity;
using FirstWebAPiProject.Model.Dto;
using FirstWebAPiProject.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseList()
        {
            var courses = await dataContext.Courses.ToListAsync();
            Log.Information("Course List => {@result}",courses);
            return Ok(courses);
        }

        [HttpGet("{id:int}", Name = "GetCourse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var course = await dataContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data is null");
            }

            var validator = new CourseValidator();
            var validationResult = validator.Validate(courseDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var newCourse = new Course()
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
            };

            dataContext.Courses.Add(newCourse);
            await dataContext.SaveChangesAsync();
            return Ok("Course is added successfully");
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            var course = dataContext.Courses.FirstOrDefault(x => x.Id == id);
            if (course == null)
            {
                return NotFound(course);
            }
            dataContext.Courses.Remove(course);
            await dataContext.SaveChangesAsync();
            return Ok("Course is Deleted Successfully");
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (course.Id < 0)
            {
                return BadRequest();
            }
            var ExistingCourse = dataContext.Courses.FirstOrDefault(x => x.Id == id);
            if (ExistingCourse == null)
            {
                return NotFound(ExistingCourse);
            }
            ExistingCourse.Name = course.Name;
            ExistingCourse.Description= course.Description;
            await dataContext.SaveChangesAsync();
            return Ok("Course is updated Successfully");
        }
    }    

}
