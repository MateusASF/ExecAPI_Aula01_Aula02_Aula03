using ApiAula01.Filters;
using ApiAula01.Repository;
using Cliente.Core.Interface;
using Cliente.Core.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//interfaces
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

//filters
builder.Services.AddMvc(options => options.Filters.Add<GeneralExceptionFilter>()); // => Global Filter
builder.Services.AddScoped<LogActionFilterCpfExiste>();
builder.Services.AddScoped<LogActionFilterCpfNaBase>();


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
