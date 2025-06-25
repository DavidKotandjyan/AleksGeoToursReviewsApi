using Microsoft.EntityFrameworkCore;
using AleksGeoToursReviewsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Добавляем подключение к базе
builder.Services.AddDbContext<ReviewsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем контроллеры
builder.Services.AddControllers();

// Включаем Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (если frontend работает отдельно)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Middleware
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.MapGet("/", () => "Backend API is running");
app.Run();
