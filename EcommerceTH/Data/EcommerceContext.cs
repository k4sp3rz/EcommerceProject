using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EcommerceTH.data;

public partial class EcommerceContext : DbContext
{
    public EcommerceContext()
    {
    }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderPro> OrderPros { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Ecommerce;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Idcate).HasName("PK__Category__43A29526DC661554");

            entity.ToTable("Category");

            entity.Property(e => e.Idcate)
                .ValueGeneratedNever()
                .HasColumnName("IDCate");
            entity.Property(e => e.NameCate)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Idcus).HasName("PK__Customer__91A99BACE93DCC0B");

            entity.ToTable("Customer");

            entity.Property(e => e.Idcus)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDCus");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmailCus)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NameCus)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneCus)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Viplevel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("VIPLevel");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Idorder).HasName("PK__OrderDet__5CBBCADBA856BD77");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Idorder)
                .ValueGeneratedNever()
                .HasColumnName("IDOrder");
            entity.Property(e => e.IdorderPro).HasColumnName("IDOrderPro");
            entity.Property(e => e.Idpro).HasColumnName("IDPro");

            entity.HasOne(d => d.IdorderProNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdorderPro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__IDOrd__44FF419A");

            entity.HasOne(d => d.IdproNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.Idpro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__IDPro__440B1D61");
        });

        modelBuilder.Entity<OrderPro>(entity =>
        {
            entity.HasKey(e => e.IdorderPro).HasName("PK__OrderPro__805485F4E060CE61");

            entity.ToTable("OrderPro");

            entity.Property(e => e.IdorderPro)
                .ValueGeneratedNever()
                .HasColumnName("IDOrderPro");
            entity.Property(e => e.Idcus).HasColumnName("IDCus");
            entity.Property(e => e.OrderAddress)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdcusNavigation).WithMany(p => p.OrderPros)
                .HasForeignKey(d => d.Idcus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderPro__IDCus__3B75D760");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Idpro).HasName("PK__Product__98F92859C11F98CA");

            entity.ToTable("Product");

            entity.Property(e => e.Idpro)
                .ValueGeneratedNever()
                .HasColumnName("IDPro");
            entity.Property(e => e.DesPro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Idcate).HasColumnName("IDCate");
            entity.Property(e => e.ImagePro)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NamePro)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdcateNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Idcate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__IDCate__412EB0B6");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Idpromote).HasName("PK__Promotio__B455A7125FF397B2");

            entity.ToTable("Promotion");

            entity.Property(e => e.Idpromote)
                .ValueGeneratedNever()
                .HasColumnName("IDPromote");
            entity.Property(e => e.IdorderPro).HasColumnName("IDOrderPro");
            entity.Property(e => e.PromoteCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdorderProNavigation).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.IdorderPro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Promotion__IDOrd__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
