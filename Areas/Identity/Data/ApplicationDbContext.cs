using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SewPro.Models; // لا تنسَ تضمين مساحة الأسماء للنموذج

namespace SewPro.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // تعريف DbSet للعملاء
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Tailor> Tailors { get; set; }  // تغيير هنا من DbSetTailor إلى DbSet<Tailor>
        public DbSet<Store> Stores { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }  // إضافة DbSet لطريقة الدفع
        public DbSet<SmsSettings> SmsSettings { get; set; }
                public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierPayment> SupplierPayments { get; set; }
        public DbSet<SewPro.Models.Bill> Bills { get; set; }
        public DbSet<Field> Fields { get; set; }
public DbSet<Status> Statuses { get; set; }
        public DbSet<Unit> Units { get; set; }
public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Employee> Employees { get; set; }
    public DbSet<SalaryPayment> SalaryPayments { get; set; }  // إضافة DbSet هنا
    public DbSet<SalaryDeduction> SalaryDeductions { get; set; }
    public DbSet<Bonus> Bonuses { get; set; }
    public DbSet<SalaryPaymentSummary> SalaryPaymentSummary { get; set; }
            public DbSet<Currency> Currencies { get; set; }
    public DbSet<OpeningBalance> OpeningBalances { get; set; }
        public DbSet<Category> Categories { get; set; } // 
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Template> Templates { get; set; }
        public DbSet<NeckType> NeckTypes { get; set; }
        public DbSet<PocketShape> PocketShapes { get; set; }
public DbSet<SleeveShape> SleeveShapes { get; set; }
        public DbSet<Industry> Industries { get; set; } // جدول الصناعات
public DbSet<FabricType> FabricTypes { get; set; }
public DbSet<Factory> Factories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
             modelBuilder.Entity<SalaryPayment>()
            .HasOne(sp => sp.Employee)
            .WithMany()
            .HasForeignKey(sp => sp.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade); // أو Restrict حسب احتياجاتك
            
             modelBuilder.Entity<SalaryDeduction>()
        .HasKey(sd => sd.DeductionId); // تحديد المفتاح الرئيسي
            
              modelBuilder.Entity<SalaryPayment>()
        .HasKey(sp => sp.PaymentId); // تعيين PaymentId كمفتاح رئيسي
            
            
                modelBuilder.Entity<SalaryPaymentSummary>()
            .HasNoKey();  // تحديد الكائن كـ "كيان بدون مفتاح"
            
        modelBuilder.Entity<Item>()
                .HasOne(i => i.ItemGroup)
                .WithMany(g => g.Items)
                .HasForeignKey(i => i.ItemGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // تعريف خصائص أخرى مثل الطول أو الفهارس
            modelBuilder.Entity<ItemGroup>()
                .Property(g => g.ArabicName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ItemGroup>()
                .Property(g => g.EnglishName)
                .HasMaxLength(200);

            modelBuilder.Entity<Item>()
                .Property(i => i.ArabicName)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Item>()
                .Property(i => i.EnglishName)
                .HasMaxLength(200);

            // تحديد الحقول الاختيارية لـ Seller
            modelBuilder.Entity<Seller>()
                .Property(s => s.JobTitle)
                .IsRequired(false);

            modelBuilder.Entity<Seller>()
                .Property(s => s.EnglishName)
                .IsRequired(false);

            modelBuilder.Entity<Seller>()
                .Property(s => s.Email)
                .IsRequired(false);

            modelBuilder.Entity<Seller>()
                .Property(s => s.Address)
                .IsRequired(false);

            modelBuilder.Entity<Seller>()
                .Property(s => s.Nationality)
                .IsRequired(false);
       
                   // تخصيص جدول الموردين إذا كنت بحاجة إلى ذلك (مثال: تحديد اسم الجدول أو المفاتيح)
            modelBuilder.Entity<Supplier>()
                .ToTable("Suppliers") // تخصيص اسم الجدول
                .HasKey(s => s.Id);  // تحديد المفتاح الأساسي

            // يمكنك إضافة تخصيصات أخرى هنا للـ Suppliers أو جداول أخرى

            // تخصيص حقل IsActive ليكون افتراضيًا true إذا لم يتم تحديده
            modelBuilder.Entity<Supplier>()
                .Property(s => s.IsActive)
                .HasDefaultValue(true); // تعيين القيمة الافتراضية
       }
    }
}
