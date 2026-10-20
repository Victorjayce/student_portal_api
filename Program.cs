using Microsoft.EntityFrameworkCore;
using Students_portal.Data;
using Students_portal.Interface;
using Students_portal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                
builder.Services.AddScoped<IStudent, StudentService>();
builder.Services.AddScoped<IFaculty, FacultyService>();
builder.Services.AddScoped<IDepartment, DepartmentService>();
builder.Services.AddScoped<ICourse, CourseService>();
builder.Services.AddScoped<Students_portal.Interface.IResult, ResultService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
