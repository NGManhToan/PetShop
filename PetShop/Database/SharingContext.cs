using Microsoft.EntityFrameworkCore;
using PetShop.Database.SharingModels;

namespace PetShop.Database
{
    public partial class SharingContext : DbContext
    {
        public SharingContext()
        {
        }

        public SharingContext(DbContextOptions<SharingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAppointment> TblAppointments { get; set; } = null!;
        public virtual DbSet<TblBlog> TblBlogs { get; set; } = null!;
        public virtual DbSet<TblBlogCategory> TblBlogCategories { get; set; } = null!;
        public virtual DbSet<TblCart> TblCarts { get; set; } = null!;
        public virtual DbSet<TblCustomer> TblCustomers { get; set; } = null!;
        public virtual DbSet<TblOrder> TblOrders { get; set; } = null!;
        public virtual DbSet<TblOrderDetail> TblOrderDetails { get; set; } = null!;
        public virtual DbSet<TblPet> TblPets { get; set; } = null!;
        public virtual DbSet<TblPetCategory> TblPetCategories { get; set; } = null!;
        public virtual DbSet<TblProduct> TblProducts { get; set; } = null!;
        public virtual DbSet<TblProductCategory> TblProductCategories { get; set; } = null!;
        public virtual DbSet<TblRole> TblRoles { get; set; } = null!;
        public virtual DbSet<TblService> TblServices { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblVendor> TblVendors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=180.93.172.49;port=4786;database=PETSHOP;user=user_share_together;password=RFYsGMZq2*AK", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<TblAppointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_appointment");

                entity.HasIndex(e => e.ServiceId, "FK_Appointment_ServiceId");

                entity.HasIndex(e => e.UserId, "FK_Appointment_UserId");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("appointment_date");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.TblAppointments)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_ServiceId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblAppointments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_UserId");
            });

            modelBuilder.Entity<TblBlog>(entity =>
            {
                entity.HasKey(e => e.BlogId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_blog");

                entity.HasIndex(e => e.BlogCategoryId, "FK_Blog_BlogCategoryId");

                entity.HasIndex(e => e.UserId, "FK_Blog_UserId");

                entity.Property(e => e.BlogId).HasColumnName("blog_id");

                entity.Property(e => e.BlogCategoryId).HasColumnName("blog_category_id");

                entity.Property(e => e.BlogContent)
                    .HasColumnType("text")
                    .HasColumnName("blog_content");

                entity.Property(e => e.BlogTitle)
                    .HasMaxLength(255)
                    .HasColumnName("blog_title");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.BlogCategory)
                    .WithMany(p => p.TblBlogs)
                    .HasForeignKey(d => d.BlogCategoryId)
                    .HasConstraintName("FK_Blog_BlogCategoryId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblBlogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blog_UserId");
            });

            modelBuilder.Entity<TblBlogCategory>(entity =>
            {
                entity.HasKey(e => e.BlogCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_blog_category");

                entity.Property(e => e.BlogCategoryId).HasColumnName("blog_category_id");

                entity.Property(e => e.BlogCategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("blog_category_name");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<TblCart>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_cart");

                entity.HasIndex(e => e.ProductId, "product_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblCarts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("tbl_cart_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblCarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("tbl_cart_ibfk_1");
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_customer");

                entity.HasIndex(e => e.UserId, "user_id")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CustomerContactInfo)
                    .HasMaxLength(100)
                    .HasColumnName("customer_contact_info");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(255)
                    .HasColumnName("customer_name");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TblCustomer)
                    .HasForeignKey<TblCustomer>(d => d.UserId)
                    .HasConstraintName("FK_Customer_UserId");
            });

            modelBuilder.Entity<TblOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_order");

                entity.HasIndex(e => e.CustomerId, "FK_Order_CustomerId");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(20)
                    .HasColumnName("order_status");

                entity.Property(e => e.TotalAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("total_amount");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TblOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_CustomerId");
            });

            modelBuilder.Entity<TblOrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_order_detail");

                entity.HasIndex(e => e.OrderId, "FK_OrderDetail_OrderId");

                entity.HasIndex(e => e.PetId, "FK_OrderDetail_PetId");

                entity.HasIndex(e => e.ProductId, "FK_OrderDetail_ProductId");

                entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PetId).HasColumnName("pet_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_OrderId");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.PetId)
                    .HasConstraintName("FK_OrderDetail_PetId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TblOrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderDetail_ProductId");
            });

            modelBuilder.Entity<TblPet>(entity =>
            {
                entity.HasKey(e => e.PetId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_pet");

                entity.HasIndex(e => e.PetCategoryId, "FK_Pet_PetCategoryId");

                entity.HasIndex(e => e.VendorId, "FK_Pet_VendorId");

                entity.Property(e => e.PetId).HasColumnName("pet_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PetCategoryId).HasColumnName("pet_category_id");

                entity.Property(e => e.PetImages)
                    .HasColumnType("text")
                    .HasColumnName("pet_images");

                entity.Property(e => e.PetName)
                    .HasMaxLength(50)
                    .HasColumnName("pet_name");

                entity.Property(e => e.PetPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("pet_price");

                entity.Property(e => e.PetStatus).HasColumnName("pet_status");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.HasOne(d => d.PetCategory)
                    .WithMany(p => p.TblPets)
                    .HasForeignKey(d => d.PetCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_PetCategoryId");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.TblPets)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_VendorId");
            });

            modelBuilder.Entity<TblPetCategory>(entity =>
            {
                entity.HasKey(e => e.PetCategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_pet_category");

                entity.Property(e => e.PetCategoryId).HasColumnName("pet_category_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PetCategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("pet_category_name");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_product");

                entity.HasIndex(e => e.CategoryId, "FK_Product_CategoryId");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Discount)
                    .HasPrecision(5, 2)
                    .HasColumnName("discount")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ProductDescription)
                    .HasColumnType("text")
                    .HasColumnName("product_description");

                entity.Property(e => e.ProductImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("product_image_url");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("product_price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TblProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_CategoryId");
            });

            modelBuilder.Entity<TblProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_product_category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("category_name");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<TblService>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_service");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ServiceDescription)
                    .HasColumnType("text")
                    .HasColumnName("service_description");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(255)
                    .HasColumnName("service_name");

                entity.Property(e => e.ServicePrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("service_price");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_user");

                entity.HasIndex(e => e.RoleId, "FK_User_RoleId");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_RoleId");
            });

            modelBuilder.Entity<TblVendor>(entity =>
            {
                entity.HasKey(e => e.VendorId)
                    .HasName("PRIMARY");

                entity.ToTable("tbl_vendor");

                entity.Property(e => e.VendorId).HasColumnName("vendor_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("bit(1)");

                entity.Property(e => e.IsDeleted).HasColumnType("bit(1)");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.VendorContactInfo)
                    .HasMaxLength(100)
                    .HasColumnName("vendor_contact_info");

                entity.Property(e => e.VendorName)
                    .HasMaxLength(50)
                    .HasColumnName("vendor_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
