using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Helper;
using WebAPI.Request.Student;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<SearchResponse<Student>>> Search(
            [FromBody] StudentRequest request)
        {
            var response = await _studentService.Search(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Student>>> Insert([FromBody] Student student)
        {
            var response = await _studentService.Insert(student);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<Student>>> Update([FromBody] Student student)
        {
            var response = await _studentService.Update(student);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Student>>> Delete(int id)
        {
            var response = await _studentService.Delete(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
