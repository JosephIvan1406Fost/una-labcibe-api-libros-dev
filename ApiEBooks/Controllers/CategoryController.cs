using ApiEBooks.Modelos;
using ApiEBooks.Modelos.Dto;
using ApiEBooks.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        public ActionResult<IEnumerable<CategoryDto>> GetCategorys()
        {
            _logger.LogInformation("Obteniendo...");
            return Ok(CategoryStore.categorylist);

        }



        [HttpGet("{id:int}",Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCategory(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Ocurrió un error en la operación.");
                return BadRequest();
            }
            var category = CategoryStore.categorylist.FirstOrDefault(v => v.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryDto> CreateCategory([FromBody] CategoryDto newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CategoryStore.categorylist.FirstOrDefault(v=>v.Name.ToLower() == newCategory.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CategoryExist", "La categoria ya existe ");
                return BadRequest(ModelState);
            }

            if (newCategory == null)
            {
                return BadRequest(newCategory);
            }
            newCategory.Id = GenerateNewCategoryId();
            CategoryStore.categorylist.Add(newCategory);
            return CreatedAtRoute("GetCategory", new { id = newCategory.Id }, newCategory);
        }

        private int GenerateNewCategoryId()
        {
            if (CategoryStore.categorylist.Count == 0)
            {
               
                return 1;
            }
            else
            {
                int maxId = CategoryStore.categorylist.Max(c => c.Id);
                return maxId + 1;
            }
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCategory(int id)
        {
            var categoryToDelete = CategoryStore.categorylist.FirstOrDefault(c => c.Id == id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            CategoryStore.categorylist.Remove(categoryToDelete);
            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest("La categoría proporcionada es nula.");
            }
            if (id <= 0)
            {
                return BadRequest("El ID debe ser mayor que 0.");
            }
            var existingCategory = CategoryStore.categorylist.FirstOrDefault(c => c.Id == id);

            if (existingCategory == null)
            {
                return NotFound();
            }
            existingCategory.Name = updatedCategory.Name; 
            return NoContent();
        }



    }


}
