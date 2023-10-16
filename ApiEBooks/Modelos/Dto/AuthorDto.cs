using System.ComponentModel.DataAnnotations;

namespace ApiEBooks.Dto
{
    public class AuthorDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
