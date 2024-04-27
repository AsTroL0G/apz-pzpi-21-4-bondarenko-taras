using CargoTrackApi.Application.ApplicationExtentions;
using CargoTrackApi.Infrastructure.InfrastructureExtentions;
using CargoTrackApi.Persistance.PersistanceExtentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMapper();
builder.Services.AddInfrastructure();
builder.Services.AddServices(builder.Configuration);
builder.Services.AddJWTTokenAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Заміни це на реальний початковий URL твого фронтенду
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
    options.AddPolicy("OwnerPolicy", policy => policy.RequireRole("Owner"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();