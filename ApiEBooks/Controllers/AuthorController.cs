using ApiEBooks.Dto;

using ApiEBooks.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiEBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(ILogger<AuthorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            _logger.LogInformation("Obteniendo autores...");
            return Ok(AuthorStore.AuthorList);
        }

        [HttpGet("{id:int}", Name = "GetAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAuthor(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Ocurrió un error en la operación.");
                return BadRequest();
            }
            var author = AuthorStore.AuthorList.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthorDto> CreateAuthor([FromBody] AuthorDto newAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newAuthor == null)
            {
                return BadRequest(newAuthor);
            }

            newAuthor.Id = GenerateNewAuthorId();
            AuthorStore.AuthorList.Add(newAuthor);
            return CreatedAtRoute("GetAuthor", new { id = newAuthor.Id }, newAuthor);
        }

        private int GenerateNewAuthorId()
        {
            if (AuthorStore.AuthorList.Count == 0)
            {
                return 1;
            }
            else
            {
                int maxId = AuthorStore.AuthorList.Max(a => a.Id);
                return maxId + 1;
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAuthor(int id)
        {
            var authorToDelete = AuthorStore.AuthorList.FirstOrDefault(a => a.Id == id);
            if (authorToDelete == null)
            {
                return NotFound();
            }
            AuthorStore.AuthorList.Remove(authorToDelete);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorDto updatedAuthor)
        {
            if (updatedAuthor == null)
            {
                return BadRequest("El autor proporcionado es nulo.");
            }
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }
            var existingAuthor = AuthorStore.AuthorList.FirstOrDefault(a => a.Id == id);

            if (existingAuthor == null)
            {
                return NotFound();
            }
            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.FirstName = updatedAuthor.FirstName;
            existingAuthor.LastName = updatedAuthor.LastName;
            return NoContent();
        }
    }
}
