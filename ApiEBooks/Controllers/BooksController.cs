using ApiEBooks.Dto;

using ApiEBooks.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiEBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BooksDto))]
        public ActionResult<IEnumerable<BooksDto>> GetBooks()
        {
            _logger.LogInformation("Obteniendo libros...");
            return Ok(BooksStore.BooksList);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BooksDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetBook(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Ocurrió un error en la operación.");
                return BadRequest();
            }
            var book = BooksStore.BooksList.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BooksDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BooksDto> CreateBook([FromBody] BooksDto newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newBook == null)
            {
                return BadRequest(newBook);
            }

            newBook.Id = GenerateNewBookId();
            BooksStore.BooksList.Add(newBook);
            return CreatedAtRoute("GetBook", new { id = newBook.Id }, newBook);
        }

        private int GenerateNewBookId()
        {
            if (BooksStore.BooksList.Count == 0)
            {
                return 1;
            }
            else
            {
                int maxId = BooksStore.BooksList.Max(b => b.Id);
                return maxId + 1;
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBook(int id)
        {
            var bookToDelete = BooksStore.BooksList.FirstOrDefault(b => b.Id == id);
            if (bookToDelete == null)
            {
                return NotFound();
            }
            BooksStore.BooksList.Remove(bookToDelete);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateBook(int id, [FromBody] BooksDto updatedBook)
        {
            if (updatedBook == null)
            {
                return BadRequest("El libro proporcionado es nulo.");
            }
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }
            var existingBook = BooksStore.BooksList.FirstOrDefault(b => b.Id == id);

            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = updatedBook.Title;
            existingBook.Description = updatedBook.Description;
            existingBook.Pages = updatedBook.Pages;
            existingBook.Author = updatedBook.Author;
            existingBook.Category = updatedBook.Category;
            return NoContent();
        }
    }
};
