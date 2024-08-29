using AutoMapper;
using FluentValidation.AspNetCore;
using Payment.BusinessLayer.Abstract;
using Payment.BusinessLayer.Concrete;
using Payment.DataAccessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.DataAccessLayer.EntityFramework;
using Payment.EntityLayer.Concrete;
using Payment.WebApi.Mapping;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>();
        fv.DisableDataAnnotationsValidation = true;
        fv.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
    });

builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();

var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperConfig()));
builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();

builder.Services.AddScoped<IAddressDal, EfAddressDal>();
builder.Services.AddScoped<IAddressService, AddressManager>();

builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserManager>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("PaymentApiCors", opts =>
    {
        opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("PaymentApiCors");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
