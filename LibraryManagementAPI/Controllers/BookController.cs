using LibraryManagementAPIModel.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly BookConcrete _book;
        public BookController(BookConcrete book)
        {
            this._book = book;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _book.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _book.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookViewModel model)
        {
            var result = await _book.AddAsync(model);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BookViewModel model)
        {
            var result = await _book.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _book.DeleteAsync(id);
            return Ok(result);
        }
    }
}

