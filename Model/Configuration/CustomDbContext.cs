using Microsoft.EntityFrameworkCore;

namespace Model.Configuration;

public class CustomDbContext : DbContext{
    public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
    {
    }
}