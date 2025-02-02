using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SewPro.Data; // اسم النيمسبيس الخاص بقاعدة البيانات في مشروعك
using Microsoft.AspNetCore.Identity.UI.Services;
using SewPro.Services; // تأكد من تضمين النيمسبيس الصحيح

var builder = WebApplication.CreateBuilder(args);

// إعداد قاعدة البيانات
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 5, 9)) // عدل الإصدار حسب إصدار MariaDB
    ));

// إضافة خدمات Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        // تخصيص إعدادات Identity إذا لزم الأمر
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// تكوين الكوكيز
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Account/Login"; // مسار تسجيل الدخول
    options.AccessDeniedPath = "/Account/AccessDenied"; // مسار رفض الوصول
    options.SlidingExpiration = true;
});

// إضافة خدمات Razor Pages
builder.Services.AddRazorPages(); // إضافة خدمة Razor Pages

// إضافة خدمات MVC مع دعم الـ Area
builder.Services.AddControllersWithViews();

// إضافة NullEmailSender لتعطيل إرسال البريد الإلكتروني
builder.Services.AddTransient<IEmailSender, NullEmailSender>(); // نمرر NullEmailSender بدلاً من IEmailSender

var app = builder.Build();

// تفعيل المصادقة والتفويض
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// تفعيل المصادقة والتفويض
app.UseAuthentication();
app.UseAuthorization();

// توجيه المستخدم إلى صفحة تسجيل الدخول بشكل افتراضي
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// توجيه المستخدم إلى الصفحة الرئيسية بعد التسجيل
app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization(); // تأكيد أن المستخدم يجب أن يكون مسجلاً دخوله

app.MapControllerRoute(
    name: "identity",
    pattern: "Identity/{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization(); // تأكيد أن المستخدم يجب أن يكون مسجلاً دخوله

// تفعيل صفحات Razor
app.MapRazorPages();

app.Run();