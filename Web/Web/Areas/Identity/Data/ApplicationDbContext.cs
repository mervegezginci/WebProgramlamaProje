using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Areas.Identity.Data;
using Web.Models;

namespace Web.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

	// Kitap tablosuna karşılık gelen
	public DbSet<Kitap> kitaplar { get; set; }

	// Dil tablosuna karşılık gelen
	public DbSet<Dil> diller { get; set; }

	// Kategori tablosuna karşılık gelen
	public DbSet<Kategori> kategoriler { get; set; }

	// KitapYazar tablosuna karşılık gelen
	public DbSet<KitapYazar> kitapYazarlar { get; set; }

	// Yazar tablosuna karşılık gelen
	public DbSet<Yazar> yazarlar { get; set; }

	// Yorumlar tablosuna karşılık gelen
	public DbSet<Yorumlar> yorumlar { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}
