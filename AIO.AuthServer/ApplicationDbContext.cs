using Microsoft.EntityFrameworkCore;

namespace AIO.AuthServer;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
}