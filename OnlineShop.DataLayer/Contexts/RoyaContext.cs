using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Data;
using OnlineShop.DataLayer.Entities.Permission;
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

    #endregion

    #region Wallet Tables

    public DbSet<WalletType> WalletTypes { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    #endregion

    #region Role Tables

    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }

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
                u.Property(p => p.Address).HasMaxLength(300).IsRequired();
                u.Property(p => p.UserAvatar).HasMaxLength(50);
                u.Property(p => p.IsActive).HasDefaultValue(false);
                u.Property(p => p.RegisterDate);
                u.Property(p => p.IsDeleted).HasDefaultValue(false);
                u.HasQueryFilter(h => h.IsDeleted == false);
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
                p.Property(p => p.PermissionTitle);
          
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

        base.OnModelCreating(modelBuilder);
    }
}