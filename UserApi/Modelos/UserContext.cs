using Microsoft.EntityFrameworkCore;

namespace UserApi.Modelos;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public DbSet<UserItem> UserItems { get; set; } = null!;
}