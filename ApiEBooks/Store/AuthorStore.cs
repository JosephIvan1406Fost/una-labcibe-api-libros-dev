

using ApiEBooks.Dto;


namespace ApiEBooks.Store
    {
        public static class AuthorStore
        {
            public static List<AuthorDto> AuthorList = new List<AuthorDto>
        {
            new AuthorDto
            {
                Id = 1,
                Name = "Autor 1",
                FirstName = "Nombre1",
                LastName = "Apellido1"
            },
            new AuthorDto
            {
                Id = 2,
                Name = "Autor 2",
                FirstName = "Nombre2",
                LastName = "Apellido2"
            },
            new AuthorDto
            {
                Id = 3,
                Name = "Autor 3",
                FirstName = "Nombre3",
                LastName = "Apellido3"
            }
        };
        }
    }


