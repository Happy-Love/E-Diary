using E_Diary.API.Filters.ExceptionFilters;
using E_Diary.Core.Exceptions;
using E_Diary.Core.IntegrationServices;
using E_Diary.Core.Services;
using E_Diary.Domain.Entities.Interfaces;
using E_Diary.Infrastructure.Persistence;
using E_Diary.Infrastructure.Persistence.Settings;
using E_Diary.Infrastructure.PubSub.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Monolith.Diploma.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "gcloud.credentials.json");

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new BadRequestOnExceptionAttribute(typeof(ValidationException), typeof(ArgumentNullException), typeof(ArgumentOutOfRangeException),
                        typeof(OperationNotAllowedException)));
    config.Filters.Add(new NotFoundOnExceptionAttribute(typeof(ResourceNotFoundException)));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.Request;
});

builder.Services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddScoped<DbContext, ApplicationDatabaseContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

builder.Services.AddScoped<ISchoolerMarkService, SchoolerMarkService>();
builder.Services.AddScoped<ISchoolerService, SchoolerService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGCloudBillingService, GCloudBillingService>();

var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
