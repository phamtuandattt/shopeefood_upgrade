using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopeeFood_WebAPI.DAL.ModelResonposeDtos.CityResponseDtos;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class ShopeefoodDbContext : DbContext
{
    public ShopeefoodDbContext()
    {
    }

    public ShopeefoodDbContext(DbContextOptions<ShopeefoodDbContext> options)
        : base(options)
    {
    }

    //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------
    public DbSet<CityBusinessFieldResponseDto> CityBusinessResponseDtos { get; set; }
    public DbSet<ShopInCityResponseDto> ShopInCityResponseDtos { get; set; }

    //------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------

    public virtual DbSet<BusinessField> BusinessFields { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CityBusinessFieldsShop> CityBusinessFieldsShops { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<CustomerExternalLogin> CustomerExternalLogins { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<MenuDetailShop> MenuDetailShops { get; set; }

    public virtual DbSet<MenuShop> MenuShops { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-KD2BPDJ;database=shopeefood_db;Integrated Security = True;uid=sa;pwd=Aa123456@;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessField>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK__Business__C8B6FF27FED7C0D5");

            entity.Property(e => e.FieldId).HasColumnName("FieldID");
            entity.Property(e => e.FieldName).HasMaxLength(255);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__F2D21A969BF4F7D0");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName).HasMaxLength(255);

            entity.HasMany(d => d.Fields).WithMany(p => p.Cities)
                .UsingEntity<Dictionary<string, object>>(
                    "CityBusinessField",
                    r => r.HasOne<BusinessField>().WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CityBusin__Field__3F466844"),
                    l => l.HasOne<City>().WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__CityBusin__CityI__3E52440B"),
                    j =>
                    {
                        j.HasKey("CityId", "FieldId").HasName("PK__CityBusi__9E59756401D699A7");
                        j.ToTable("CityBusinessFields");
                        j.IndexerProperty<int>("CityId").HasColumnName("CityID");
                        j.IndexerProperty<int>("FieldId").HasColumnName("FieldID");
                    });
        });

        modelBuilder.Entity<CityBusinessFieldsShop>(entity =>
        {
            entity.HasKey(e => new { e.CityId, e.FieldId, e.ShopId }).HasName("PK__CityBusi__B63EB0322AA481CC");

            entity.ToTable("CityBusinessFieldsShop");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.FieldId).HasColumnName("FieldID");
            entity.Property(e => e.ShopId).HasColumnName("ShopID");

            entity.HasOne(d => d.City).WithMany(p => p.CityBusinessFieldsShops)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CityBusin__CityI__66603565");

            entity.HasOne(d => d.Field).WithMany(p => p.CityBusinessFieldsShops)
                .HasForeignKey(d => d.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CityBusin__Field__6754599E");

            entity.HasOne(d => d.Shop).WithMany(p => p.CityBusinessFieldsShops)
                .HasForeignKey(d => d.ShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CityBusin__ShopI__68487DD7");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B848489A51");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Avata).HasMaxLength(200);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RefreshTokenExpiryTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Customer__091C2A1B9A53E935");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AddressType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.IsDefault).HasDefaultValue(false);
            entity.Property(e => e.Note).HasMaxLength(200);
            entity.Property(e => e.Street).HasMaxLength(200);
            entity.Property(e => e.WardId).HasColumnName("WardID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CustomerA__Custo__0C85DE4D");
        });

        modelBuilder.Entity<CustomerExternalLogin>(entity =>
        {
            entity.HasKey(e => e.ExternalLoginId).HasName("PK__Customer__A8FDB38E5796280E");

            entity.Property(e => e.ExternalLoginId).HasColumnName("ExternalLoginID");
            entity.Property(e => e.AvatarUrl).HasMaxLength(300);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Provider).HasMaxLength(50);
            entity.Property(e => e.ProviderEmail).HasMaxLength(100);
            entity.Property(e => e.ProviderUserId).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerExternalLogins)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CustomerE__Custo__114A936A");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK__District__85FDA4A671DA9EF4");

            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DistrictName).HasMaxLength(255);

            entity.HasOne(d => d.City).WithMany(p => p.Districts)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Districts__CityI__398D8EEE");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1661D89EF");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__85FB4E38B3104BB7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D1053408FFE5E7").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MenuDetailShop>(entity =>
        {
            entity.HasKey(e => e.CategoryItemId).HasName("PK__MenuDeta__E04E1100BF2727A9");

            entity.ToTable("MenuDetailShop");

            entity.Property(e => e.CategoryItemId).HasColumnName("CategoryItemID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.HasOne(d => d.Category).WithMany(p => p.MenuDetailShops)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__MenuDetai__Categ__4F7CD00D");
        });

        modelBuilder.Entity<MenuShop>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__MenuShop__19093A2B63F90B5D");

            entity.ToTable("MenuShop");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ShopId).HasColumnName("ShopID");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__Shops__67C55629D442AF51");

            entity.Property(e => e.ShopId).HasColumnName("ShopID");
            entity.Property(e => e.ShopName).HasMaxLength(255);
            entity.Property(e => e.ShopUptime)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.WardId).HasName("PK__Wards__C6BD9BEA6DB54803");

            entity.Property(e => e.WardId).HasColumnName("WardID");
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.WardName).HasMaxLength(100);

            entity.HasOne(d => d.District).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK__Wards__DistrictI__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
