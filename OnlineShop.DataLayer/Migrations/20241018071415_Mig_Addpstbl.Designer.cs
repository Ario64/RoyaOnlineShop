﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.DataLayer.Contexts;

#nullable disable

namespace OnlineShop.DataLayer.Migrations
{
    [DbContext(typeof(RoyaContext))]
    [Migration("20241018071415_Mig_Addpstbl")]
    partial class Mig_Addpstbl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Permission.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PermissionId");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Permission.RolePermission", b =>
                {
                    b.Property<int>("RpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RpId"));

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("RpId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"));

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ColorId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ProductGroupId")
                        .HasColumnType("int");

                    b.Property<string>("ProductImageName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("ProductPrice")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SeoDescription")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("SubGroup")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductGroupId");

                    b.HasIndex("SubGroup");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.ProductColor", b =>
                {
                    b.Property<int>("PcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PcId"));

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PcId");

                    b.HasIndex("ColorId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductColors");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.ProductGroup", b =>
                {
                    b.Property<int>("ProductGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductGroupId"));

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("ProductGroupId");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.ProductSize", b =>
                {
                    b.Property<int>("PsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PsId"));

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SizeId")
                        .HasColumnType("int");

                    b.HasKey("PsId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SizeId");

                    b.HasIndex("ProductId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("ActiveCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAvatar")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.UserProduct", b =>
                {
                    b.Property<int>("UserProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserProductId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProducts");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.UserRole", b =>
                {
                    b.Property<int>("UrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UrId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UrId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Wallet.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WalletId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WalletTypeId")
                        .HasColumnType("int");

                    b.HasKey("WalletId");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletTypeId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Wallet.WalletType", b =>
                {
                    b.Property<int>("WalletTypeId")
                        .HasColumnType("int");

                    b.Property<string>("WalletTypeTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("WalletTypeId");

                    b.ToTable("WalletTypes");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Permission.Permission", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Permission.Permission", "_Permission")
                        .WithMany("Permissions")
                        .HasForeignKey("ParentId");

                    b.Navigation("_Permission");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Permission.RolePermission", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Permission.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.DataLayer.Entities.User.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Product", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Product.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.DataLayer.Entities.Product.ProductGroup", "Group")
                        .WithMany("ProductList")
                        .HasForeignKey("SubGroup");

                    b.Navigation("Group");

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.ProductColor", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Product.Color", "Color")
                        .WithMany("ProductColors")
                        .HasForeignKey("ColorId");

                    b.HasOne("OnlineShop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("ProductColors")
                        .HasForeignKey("ProductId");

                    b.Navigation("Color");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.ProductGroup", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Product.ProductGroup", "Groups")
                        .WithMany("ProductGroups")
                        .HasForeignKey("ParentId");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.ProductSize", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineShop.DataLayer.Entities.Product.Size", "Size")
                        .WithMany("ProductSizes")
                        .HasForeignKey("SizeId");

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Size", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Product.Product", null)
                        .WithMany("Sizes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.UserProduct", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.Product.Product", "Product")
                        .WithMany("UserProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.DataLayer.Entities.User.User", "User")
                        .WithMany("UserProducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.UserRole", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.User.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineShop.DataLayer.Entities.User.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Wallet.Wallet", b =>
                {
                    b.HasOne("OnlineShop.DataLayer.Entities.User.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId");

                    b.HasOne("OnlineShop.DataLayer.Entities.Wallet.WalletType", "WalletType")
                        .WithMany("Wallets")
                        .HasForeignKey("WalletTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WalletType");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Permission.Permission", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Color", b =>
                {
                    b.Navigation("ProductColors");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Product", b =>
                {
                    b.Navigation("ProductColors");

                    b.Navigation("ProductSizes");

                    b.Navigation("Sizes");

                    b.Navigation("UserProducts");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.ProductGroup", b =>
                {
                    b.Navigation("ProductGroups");

                    b.Navigation("ProductList");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Product.Size", b =>
                {
                    b.Navigation("ProductSizes");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.User.User", b =>
                {
                    b.Navigation("UserProducts");

                    b.Navigation("UserRoles");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("OnlineShop.DataLayer.Entities.Wallet.WalletType", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}