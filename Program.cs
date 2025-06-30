using Microsoft.EntityFrameworkCore;
using AleksGeoToursReviewsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<ReviewsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
var port = Environment.GetEnvironmentVariable("PORT") ?? "5055";
app.Urls.Add($"http://*:{port}");

// 👉 Правильный порядок!
app.UseCors("MyAllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapGet("/", () => "Backend API is running");

app.Run();
