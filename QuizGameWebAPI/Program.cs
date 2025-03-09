using Microsoft.EntityFrameworkCore;
using QuizGameWebAPI;
using QuizGameWebAPI.Data;
using QuizGameWebAPI.Repositories;
using QuizGameWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionstring"))
    ,ServiceLifetime.Transient
    );

//Dependancy injection
//Repositories
builder.Services.AddScoped<QuestionRepository>();
builder.Services.AddScoped<OptionRepository>();
builder.Services.AddScoped<UserRepository>();

//Services
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
