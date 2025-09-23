#region
var builder = WebApplication.CreateBuilder(args);
var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString = rawConnectionString.Replace("{DB_PASSWORD}", password);
#endregion 


builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Configuration["ConnectionStrings:FinalConnection"] = connectionString;

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", policy =>
        {
            policy.WithOrigins("http://localhost:5173/")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();


app.Run();