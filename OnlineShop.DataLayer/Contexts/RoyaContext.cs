using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Data;
using OnlineShop.DataLayer.Entities.Permission;
using OnlineShop.DataLayer.Entities.Product;
using OnlineShop.DataLayer.Entities.User;
using OnlineShop.DataLayer.Entities.Wallet;

namespace OnlineShop.DataLayer.Contexts;


public class RoyaContext : DbContext
{
    public RoyaContext(DbContextOptions<RoyaContext> options) : base(options)
    {

    }

    #region User Tables

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserProduct> UserProducts { get; set; }

    #endregion

    #region Wallet Tables

    public DbSet<WalletType> WalletTypes { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    #endregion

    #region Role Tables

    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    #endregion

    #region Product Tables

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<ProductSize> ProductSizes { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region User Data

        modelBuilder.Entity<User>(
            u =>
            {
                u.HasKey(h => h.UserId);
                u.Property(p => p.FullName).HasMaxLength(150).IsRequired();
                u.Property(p => p.UserName).HasMaxLength(30).IsRequired();
                u.Property(p => p.Email).HasMaxLength(100).IsRequired();
                u.Property(p => p.PhoneNumber).HasMaxLength(11).IsRequired();
                u.Property(p => p.ActiveCode).HasMaxLength(50).IsRequired();
                u.Property(p => p.Address).HasMaxLength(300);
                u.Property(p => p.UserAvatar).HasMaxLength(50);
                u.Property(p => p.IsActive).HasDefaultValue(false);
                u.Property(p => p.RegisterDate);
                u.Property(p => p.IsDeleted).HasDefaultValue(false);
                u.HasQueryFilter(h => !h.IsDeleted);
            });

        modelBuilder.Entity<Role>(
            r =>
            {
                r.HasKey(h => h.RoleId);
                r.Property(p => p.RoleTitle).HasMaxLength(30).IsRequired();
                r.HasQueryFilter(h => h.IsDeleted == false);
            });

        modelBuilder.Entity<UserRole>(
            ur =>
            {
                ur.HasKey(h => h.UrId);
                ur.HasOne(h => h.User).WithMany("UserRoles")
                    .HasForeignKey(f => f.UserId);
                ur.HasOne(h => h.Role).WithMany("UserRoles")
                    .HasForeignKey(f => f.RoleId);
            });

        modelBuilder.Entity<UserProduct>(
            up =>
            {
                up.HasKey(h => h.UserProductId);
                up.HasOne(h => h.Product).WithMany("UserProducts")
                    .HasForeignKey(f => f.ProductId);
                up.HasOne(h => h.User).WithMany("UserProducts")
                    .HasForeignKey(f => f.UserId);
            });

        #endregion

        #region Wallet Data

        modelBuilder.Entity<WalletType>(
            w =>
            {
                w.HasKey(h => h.WalletTypeId);
                w.Property(p => p.WalletTypeTitle).HasMaxLength(150).IsRequired();

            });

        modelBuilder.Entity<Wallet>(
            w =>
            {
                w.HasKey(h => h.WalletId);
                w.Property(p => p.Amount).IsRequired();
                w.Property(p => p.Description).HasMaxLength(500);
                w.Property(p => p.IsPayed);
                w.Property(p => p.CreateDate);
                w.HasOne(h => h.User).WithMany("Wallets")
                    .HasForeignKey(f => f.UserId);
                w.HasOne(h => h.WalletType).WithMany("Wallets")
                    .HasForeignKey(f => f.WalletTypeId);

            });

        #endregion

        #region Permission Data

        modelBuilder.Entity<Permission>(
            p =>
            {
                p.HasKey(h => h.PermissionId);
                p.Property(h => h.PermissionTitle).HasMaxLength(200).IsRequired();
                p.HasOne(h => h._Permission).WithMany("Permissions")
                    .HasForeignKey(f => f.ParentId);

            });

        modelBuilder.Entity<RolePermission>(
            rp =>
            {
                rp.HasKey(h => h.RpId);
                rp.HasOne(h => h.Role).WithMany("RolePermissions")
                    .HasForeignKey(f => f.RoleId);
                rp.HasOne(h => h.Permission).WithMany("RolePermissions")
                    .HasForeignKey(f => f.PermissionId);
            });

        #endregion

        #region Product Data

        modelBuilder.Entity<ProductGroup>(
            pg =>
            {
                pg.HasKey(h => h.ProductGroupId);
                pg.Property(p => p.GroupTitle).HasMaxLength(150).IsRequired();
                pg.HasOne(h => h.Groups).WithMany("ProductGroups")
                    .HasForeignKey(f => f.ParentId);
                pg.HasQueryFilter(h => !h.IsDeleted);
            });

        modelBuilder.Entity<Product>(
            pr =>
            {
                pr.HasKey(h => h.ProductId);
                pr.Property(p => p.ProductName).HasMaxLength(400).IsRequired();
                pr.Property(p => p.Description).IsRequired();
                pr.Property(p => p.ProductPrice).IsRequired();
                pr.Property(p => p.Quantity).IsRequired();
                pr.Property(p => p.Tags).HasMaxLength(600).IsRequired();
                pr.Property(p => p.SeoDescription).HasMaxLength(300).IsRequired();
                pr.Property(p => p.ProductImageName).HasMaxLength(60).IsRequired();
                pr.Property(p => p.CreateDate);
                pr.HasOne(h => h.ProductGroup).WithMany("Products")
                    .HasForeignKey(f => f.ProductGroupId);
                pr.HasOne(h => h.Group).WithMany("ProductList")
                    .HasForeignKey(f => f.SubGroup);
                pr.HasQueryFilter(h => !h.IsDeleted);
            });

        modelBuilder.Entity<Color>(
            c =>
            {
                c.HasKey(h => h.ColorId);
                c.Property(p => p.ColorName).HasMaxLength(100).IsRequired();
                c.HasQueryFilter(h => !h.IsDeleted);
            });

        modelBuilder.Entity<ProductColor>(
            pc =>
            {
                pc.HasKey(h => h.PcId);
                pc.Property(p => p.Quantity).HasMaxLength(100);
                pc.HasOne(h => h.Color).WithMany("ProductColors")
                    .HasForeignKey(f => f.ColorId);
                pc.HasOne(h => h.Product).WithMany("ProductColors")
                    .HasForeignKey(f => f.ProductId);
            }
    );

        modelBuilder.Entity<Size>(
                s =>
                {
                    s.HasKey(h => h.SizeId);
                    s.Property(p => p.SizeName).HasMaxLength(50).IsRequired();
                    s.Property(p => p.Description).HasMaxLength(500).IsRequired();
                    s.Property(p => p.IsDeleted);
                    s.HasQueryFilter(h => !h.IsDeleted);
                });

        modelBuilder.Entity<ProductSize>(
            ps =>
            {
                ps.HasKey(h => h.PsId);
                ps.Property(p => p.Quantity).HasMaxLength(100);
                ps.HasOne(h => h.Product).WithMany("ProductSizes")
                    .HasForeignKey(f => f.ProductId);
                ps.HasOne(h => h.Size).WithMany("ProductSizes")
                    .HasForeignKey(f => f.SizeId);
            });

        #endregion

        base.OnModelCreating(modelBuilder);
    }

}