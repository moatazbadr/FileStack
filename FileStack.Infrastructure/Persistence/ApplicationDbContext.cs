using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FileStack.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<TempUser> TempUsers { get; set; }
    public DbSet<OTPToken> OTPTokens { get; set; }
}
