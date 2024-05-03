using MongoDB.Driver;
using MedicalHistoryAPI.Services;
using ProyectoApiHistorias.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la conexión a MongoDB para MedicalHistoryAPI
builder.Services.AddSingleton<IMongoDatabaseSettings>(sp =>
    new ProyectoApiHistorias.Controllers.MongoDatabaseSettings
    {
        ConnectionString = "mongodb+srv://eenriquefragozo:PRaCUGb0aXeffjYF@cluster0.uawckuf.mongodb.net/?retryWrites=true&w=majority&appName=Cluster",
        DatabaseName = "MedicalHistorias"
    });

// Crear una instancia de MongoClient
var mongoClient = new MongoClient(builder.Configuration.GetSection("MongoDatabaseSettings:ConnectionString").Value);

// Obtener la base de datos necesaria utilizando el cliente de MongoDB
var database = mongoClient.GetDatabase(builder.Configuration.GetSection("MongoDatabaseSettings:DatabaseName").Value);

// Registrar la instancia de la base de datos en el contenedor de servicios
builder.Services.AddSingleton(database);

// Crear una instancia de MedicalHistoryService y registrarla como un servicio de ámbito
builder.Services.AddScoped<IMedicalHistoryService, MedicalHistoryService>();

var app = builder.Build();

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
