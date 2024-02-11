using DbAssignment.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbAssignment.Contexts;

public partial class DataContext2 : DbContext
{
    public DataContext2()
    {
    }

    public DataContext2(DbContextOptions<DataContext2> options)  : base(options)
    {
    }

    public virtual DbSet<ManufacturerEntity> Manufacturers { get; set; }

    public virtual DbSet<ProductAttributeEntity> ProductAttributes { get; set; }

    public virtual DbSet<ProductImageEntity> ProductImages { get; set; }

    public virtual DbSet<ReviewEntity> Reviews { get; set; }

    public virtual DbSet<SupplierEntity> Suppliers { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ManufacturerEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC07A374CA6C");
        });

        modelBuilder.Entity<ProductAttributeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductA__3214EC0724BEE215");
        });

        modelBuilder.Entity<ProductImageEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC0711ACDBDD");
        });

        modelBuilder.Entity<ReviewEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC07E6290B45");
        });

        modelBuilder.Entity<SupplierEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC07BB8E9D53");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
