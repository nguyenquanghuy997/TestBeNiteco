using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestBENiteco2.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NitecoContext>(x => x
    .UseMySql("server=localhost;port=3306;user=root;password=qgIcmBxb2wYGjN@C;database=TestNiteco",
        new MySqlServerVersion(new Version(8, 0, 30)), b =>
        {
            b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
            b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        }));
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();