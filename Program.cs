using ChatAPI.Models.DB;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://localhost:2725", "http://192.168.6.98:2725");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<DBCHAT>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ChatAPI_Connection")));

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
