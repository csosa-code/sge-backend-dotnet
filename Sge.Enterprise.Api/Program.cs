
using Sge.Enterprise.Api.Middlewares;
using Sge.Enterprise.Application.Mapping;
using Sge.Enterprise.Infrastructure.Extensions;
using Sge.Enterprise.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddTransient<ApiResponseMiddleware>();
// builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.Configure<EmployeeSettings>(builder.Configuration.GetSection("EmployeeSettings"));


builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseMiddleware<ApiResponseMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();


app.Run();




