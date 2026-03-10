using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebClient.Hubs;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Controllers.Api
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorApiController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IHubContext<AppHub> _hubContext;

        public AuthorApiController(IAuthorService authorService, IHubContext<AppHub> hubContext)
        {
            _authorService = authorService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAuthor = await _authorService.CreateAuthorAsync(author);
            await _hubContext.Clients.All.SendAsync("DropdownsUpdated");

            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorId }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest("AuthorId mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedAuthor = await _authorService.UpdateAuthorAsync(id, author);

            if (updatedAuthor == null)
            {
                return NotFound();
            }

            await _hubContext.Clients.All.SendAsync("DropdownsUpdated");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var success = await _authorService.DeleteAuthorAsync(id);
            if (!success)
            {
                return NotFound();
            }

            await _hubContext.Clients.All.SendAsync("DropdownsUpdated");

            return NoContent();
        }
    }
}
