using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SilmaKlinika.Areas.Identity.Data;

namespace SilmaKlinika.Areas.Identity.Data;

public class SilmaKlinikaContext : IdentityDbContext<SilmaKlinikaUser>
{
    public SilmaKlinikaContext(DbContextOptions<SilmaKlinikaContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new SilmaKlinikaUserEntityConfiguration());
    }
}
public class SilmaKlinikaUserEntityConfiguration : IEntityTypeConfiguration<SilmaKlinikaUser>
{
    public void Configure(EntityTypeBuilder<SilmaKlinikaUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}