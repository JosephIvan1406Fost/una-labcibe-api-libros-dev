
using ApiEBooks.Dto;
using ApiEBooks.Modelos.Dto;

namespace ApiEBooks.Store
{
    public static class BooksStore
    {
        public static List<BooksDto> BooksList = new List<BooksDto>
        {
            new BooksDto
            {
                Id = 1,
                Title = "Libro 1",
                Description = "Descripción del Libro 1",
                Pages = 200,
                Author = AuthorStore.AuthorList.FirstOrDefault(author => author.Id == 1),
                Category = CategoryStore.categorylist.FirstOrDefault(category => category.Id == 1)
            },
            new BooksDto
            {
                Id = 2,
                Title = "Libro 2",
                Description = "Descripción del Libro 2",
                Pages = 150,
                Author = AuthorStore.AuthorList.FirstOrDefault(author => author.Id == 2),
                Category = CategoryStore.categorylist.FirstOrDefault(category => category.Id == 2)
            },
            new BooksDto
            {
                Id = 3,
                Title = "Libro 3",
                Description = "Descripción del Libro 3",
                Pages = 300,
                Author = AuthorStore.AuthorList.FirstOrDefault(author => author.Id == 3),
                Category = CategoryStore.categorylist.FirstOrDefault(category => category.Id == 3)
            }
        };
    }
}
