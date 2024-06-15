using MongoDB.Driver;
using MedicalHistoryAPI.Services;
using ProyectoApiHistorias.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de la conexi�n a MongoDB para MedicalHistoryAPI
builder.Services.AddSingleton<IMongoDatabaseSettings>(sp =>
    new ProyectoApiHistorias.Controllers.MongoDatabaseSettings
    {
        ConnectionString = "mongodb+srv://eenriquefragozo:PRaCUGb0aXeffjYF@cluster0.uawckuf.mongodb.net/?retryWrites=true&w=majority&appName=Cluster",
        DatabaseName = "MedicalHistorias"
    });

// Crear una instancia de MongoClient
var mongoClient = new MongoClient(builder.Configuration.GetSection("MongoDatabaseSettings:ConnectionString").Value);


var database = mongoClient.GetDatabase(builder.Configuration.GetSection("MongoDatabaseSettings:DatabaseName").Value);


builder.Services.AddSingleton(database);


builder.Services.AddScoped<MedicalHistoryService>();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
