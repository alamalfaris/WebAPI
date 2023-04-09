global using WebAPI.Data;
global using Microsoft.EntityFrameworkCore;
global using WebAPI.Services.StudentServices;
global using WebAPI.Services.BookServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//General config
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IBookService, BookService>();

//Database config
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
