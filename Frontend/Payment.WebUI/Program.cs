using Payment.DataAccessLayer.Concrete;
using Payment.EntityLayer.Concrete;
using Payment.WebUI.Models.Mail;
using Payment.BusinessLayer.Abstract;
using Payment.BusinessLayer.Concrete;
using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.EntityFramework;
using FluentValidation.AspNetCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.DisableDataAnnotationsValidation = true;
        fv.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
    });

builder.Services.AddScoped<Payment.WebUI.Models.Mail.IEmailSender, SmtpEmailSender>(i =>
    new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"],
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:Username"],
        builder.Configuration["EmailSender:Password"])
);

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddControllers().AddXmlSerializerFormatters();


builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
