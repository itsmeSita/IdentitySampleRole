
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentitySampleRole.StartUp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//adding DbContext
/* builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    option.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("Infrastructure"));

});  */ 





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services from the Infrastructure project
builder.Services.AddInfrastructure(builder.Configuration);

// Add internal dependences from the StartUp project
builder.Services.AddInternalDependencies(builder.Configuration);

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
