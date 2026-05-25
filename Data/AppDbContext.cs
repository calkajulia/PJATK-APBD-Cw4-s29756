using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s29756.Models;

namespace PJATK_APBD_Cw4_s29756.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pc> Pcs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<PcComponent> PcComponents { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pc>(entity =>
        {
            entity.HasKey(pc => pc.Id);
            entity.Property(pc => pc.Name).IsRequired().HasMaxLength(50);
            entity.Property(pc => pc.Weight).HasColumnType("float(5)");
            entity.Property(pc => pc.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.HasKey(componentType => componentType.Id);
            entity.Property(componentType => componentType.Abbreviation).IsRequired().HasMaxLength(30);
            entity.Property(componentType => componentType.Name).IsRequired().HasMaxLength(150);
        });

        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.HasKey(manufacturer => manufacturer.Id);
            entity.Property(manufacturer => manufacturer.Abbreviation).IsRequired().HasMaxLength(30);
            entity.Property(manufacturer => manufacturer.FullName).IsRequired().HasMaxLength(300);
            entity.Property(manufacturer => manufacturer.FoundationDate).IsRequired();
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(component => component.Code);
            entity.Property(component => component.Code).HasMaxLength(10);
            entity.Property(component => component.Name).IsRequired().HasMaxLength(300);
            entity.Property(component => component.Description).IsRequired().HasColumnType("nvarchar(max)");

            entity.HasOne(component => component.Manufacturer)
                .WithMany(manufacturer => manufacturer.Components)
                .HasForeignKey(component => component.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(component => component.Type)
                .WithMany(componentType => componentType.Components)
                .HasForeignKey(component => component.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.HasKey(pcComponent => new { pcComponent.PcId, pcComponent.ComponentCode });

            entity.HasOne(pcComponent => pcComponent.Pc)
                .WithMany(pc => pc.PcComponents)
                .HasForeignKey(pcComponent => pcComponent.PcId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(pcComponent => pcComponent.Component)
                .WithMany(component => component.PcComponents)
                .HasForeignKey(pcComponent => pcComponent.ComponentCode)
                .OnDelete(DeleteBehavior.Restrict);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
        );

        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateOnly(1969, 5, 1) },
            new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateOnly(1993, 4, 5) },
            new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateOnly(1994, 1, 1) }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Component { Code = "GPU0000001", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ComponentManufacturersId = 2, ComponentTypesId = 2 },
            new Component { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturersId = 3, ComponentTypesId = 3 }
        );

        modelBuilder.Entity<Pc>().HasData(
            new Pc { Id = 1, Name = "Gaming Beast X", Weight = 12.5, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
            new Pc { Id = 2, Name = "Office Mini Pro", Weight = 4.2, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
            new Pc { Id = 3, Name = "Workstation Ultra", Weight = 18.0, Warranty = 48, CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0), Stock = 3 }
        );

        modelBuilder.Entity<PcComponent>().HasData(
            new PcComponent { PcId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PcComponent { PcId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new PcComponent { PcId = 1, ComponentCode = "RAM0000001", Amount = 2 },
            new PcComponent { PcId = 2, ComponentCode = "CPU0000001", Amount = 1 },
            new PcComponent { PcId = 2, ComponentCode = "RAM0000001", Amount = 1 },
            new PcComponent { PcId = 3, ComponentCode = "CPU0000001", Amount = 2 },
            new PcComponent { PcId = 3, ComponentCode = "GPU0000001", Amount = 2 }
        );
    }
}
