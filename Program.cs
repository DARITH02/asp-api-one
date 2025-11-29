using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi1.Data;
using Microsoft.OpenApi.Models;


var builder =WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "📦 Stock Management API",
        Version = "v1",
        Description = "Manage products, categories, and suppliers easily."
    });
});


// Add DbContext

//builder.Services.AddDbContext<AppContext>();


builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Repositories and Services
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ProductService>();


//prevent loop fo null object 
/*
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // ✅ Prevent loop
        */




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "📦 Stock Management API v1");
        c.RoutePrefix = "docs"; // access at /docs instead of /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
