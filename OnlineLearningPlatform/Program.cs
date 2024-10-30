
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Data;
using OnlineLearningPlatform.Services;

var builder = WebApplication.CreateBuilder(args);
var builder2 = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<LearningPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

builder2.Services.AddControllers();
builder2.Services.AddDbContext<LearningPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder2.Services.AddSingleton(new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));
builder2.Services.AddScoped<BlobService>();

var app2 = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

if (app2.Environment.IsDevelopment())
{
    app2.UseDeveloperExceptionPage();
}

app2.UseHttpsRedirection();
app2.UseAuthorization();
app2.MapControllers();
app2.Run();