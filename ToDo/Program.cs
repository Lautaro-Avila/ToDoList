using Microsoft.EntityFrameworkCore;
using ToDo.Repository;
using ToDo.Servicios.InterfazServicio;
using ToDo.Servicios.Extensions;
using ToDo.Repository.Interfaces;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITareasService, TareasService>();

builder.Services.AddScoped<ITareaRepository, TareaRepository>();


String connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ToDoContext>(config =>
{

    config.UseSqlServer(connectionString);

});


var app = builder.Build();



app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();
