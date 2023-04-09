using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Request.Book;
using WebAPI.Responses;
using WebAPI.Services.BookServices;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Book>>> Insert([FromBody] Book book)
        {
            var response = await _bookService.Insert(book);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<Book>>> Update([FromBody] Book book)
        {
            var response = await _bookService.Update(book);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<SearchResponse<Book>>> Search([FromBody] BookRequest request)
        {
            //var response = await _bookService.Search(request);
            var response = await _bookService.SearchWithSP(request);
            if (!response.IsSuccess)
            {
                return BadRequest(response);

            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<Book>>> Delete(int id)
        {
            var response = await _bookService.Delete(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
