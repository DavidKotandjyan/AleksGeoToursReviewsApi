using Microsoft.EntityFrameworkCore;
using AleksGeoToursReviewsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// ��������� ����������� � ����
builder.Services.AddDbContext<ReviewsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� �����������
builder.Services.AddControllers();

// �������� Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS (���� frontend �������� ��������)
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
