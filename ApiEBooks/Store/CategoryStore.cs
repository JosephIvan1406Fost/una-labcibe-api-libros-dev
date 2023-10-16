using ApiEBooks.Modelos.Dto;

namespace ApiEBooks.Store
{
    public static class CategoryStore 
    {
        public static List<CategoryDto> categorylist = new List<CategoryDto>
        {
              new CategoryDto
            {
                Id = 1,
                Name = "Categoría 1"
            },
            new CategoryDto
            {
                Id = 2,
                Name = "Categoría 2"
            },
            new CategoryDto
            {
                Id = 3,
                Name = "Categoría 3"
            }
        };
    }
}
