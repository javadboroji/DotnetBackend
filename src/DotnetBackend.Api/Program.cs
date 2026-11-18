using DotnetBackend.Application.Products;
using DotnetBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DotnetBackend.Infrastructure.Persistence.Repositories;
using DotnetBackend.Application.Categories;
using DotnetBackend.Api.Common.Exceptions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<IProductsRepository, ProductRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoryRepository>();




// Add services to the container.
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();






builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
