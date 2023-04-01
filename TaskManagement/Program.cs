using TaskManagement.Repository;
using TaskManagement.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<TicketService, TicketService>();
builder.Services.AddScoped<TicketImageService, TicketImageService>();
builder.Services.AddScoped<TicketRepository, TicketRepository>();
builder.Services.AddScoped<TicketImageRepository, TicketImageRepository>();;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
