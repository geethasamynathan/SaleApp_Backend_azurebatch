using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaleApp_Backend.Models;
using SaleApp_Backend.Profiles;
using SaleApp_Backend.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITerritoryService, TerritoryService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IOrderDetailsService,OrderDetailsService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IShipperService, ShipperService>();
builder.Services.AddDbContext<NorthwindPubsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));
var mapperConfiguration = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
