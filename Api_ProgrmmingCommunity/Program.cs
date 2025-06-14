using Api_ProgrammingCommunity.Services;
using Api_ProgrmmingCommunity.Models;
// <--- Add this line for your Hub
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProgrammingCommunityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add SignalR
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(origin => true); // Allow all origins in dev - be careful in production!
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting(); // 🔥 Required for SignalR

app.UseCors();     // 🔥 Allow cross-origin requests (important for frontend SignalR)

app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.MapHub<BuzzerHub>("/buzzerhub");

app.Run();
