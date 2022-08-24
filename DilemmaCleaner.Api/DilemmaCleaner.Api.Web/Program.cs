using DilemmaCleaner.Api.Web.Concepts;
using DilemmaCleaner.Api.Web.Infrastructure.Cache;
using DilemmaCleaner.Api.Web.Infrastructure.Prismic;
using ExceptionHandlerMiddleware = DilemmaCleaner.Api.Web.Infrastructure.Middlewares.ExceptionHandlerMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCacheDependencies(builder.Configuration);
builder.Services.AddPrismicDependencies(builder.Configuration);
builder.Services.AddConcepts();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.UseCors(policyBuilder => policyBuilder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(_ => true)
);

app.Run();