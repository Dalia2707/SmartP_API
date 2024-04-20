using Microsoft.Extensions.FileProviders;
using SmartPlant.Api.Configurations;
using SmartPlant.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDatabase"));////


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<UserServices>();/////
builder.Services.AddSingleton<PlantServices>();/////
builder.Services.AddSingleton<ElectrovalveServices>();/////
builder.Services.AddSingleton<HumServices>();/////
builder.Services.AddSingleton<SizeServices>();/////
builder.Services.AddSingleton<DetallePlantaServices>();/////



builder.Services.AddCors(option => {
    option.AddPolicy("NuevaPolitica", app => {
        app.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/Uploads"
});

app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");
app.UseAuthorization();

app.MapControllers();

app.Run();
