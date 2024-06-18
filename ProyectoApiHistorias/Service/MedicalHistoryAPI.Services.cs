using MedicalHistoryAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using ProyectoApiHistorias.Controllers;


namespace MedicalHistoryAPI.Services
{
    public class MedicalHistoryService
    {
        private readonly IMongoCollection<MedicalHistory> _medicalHistories;

        public MedicalHistoryService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _medicalHistories = database.GetCollection<MedicalHistory>("MedicalHistories");
        }

        public async Task<MedicalHistory> GetMedicalHistoryAsync(string id)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, id);
            return await _medicalHistories.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<MedicalHistory> CreateMedicalHistoryAsync(MedicalHistory medicalHistory)
        {
            var newId = ObjectId.GenerateNewId();
            medicalHistory.Id = newId.ToString();

            await _medicalHistories.InsertOneAsync(medicalHistory);
            return medicalHistory;
        }

        public async Task UpdateMedicalHistoryAsync(string id, MedicalHistory medicalHistory)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, id);

            var update = Builders<MedicalHistory>.Update
                .Set(m => m.PatientId, medicalHistory.PatientId)
                .Set(m => m.Diagnostics, medicalHistory.Diagnostics)
                .Set(m => m.Treatments, medicalHistory.Treatments)
                .Set(m => m.Procedures, medicalHistory.Procedures);

            await _medicalHistories.UpdateOneAsync(filter, update);
        }

        public async Task DeleteMedicalHistoryAsync(string id)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, id);

            await _medicalHistories.DeleteOneAsync(filter);
        }

        public async Task<Diagnostic> GetDiagnosticAsync(string medicalHistoryId, string diagnosticId)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var medicalHistory = await _medicalHistories.Find(filter).FirstOrDefaultAsync();

            if (medicalHistory == null)
            {
                return null;
            }

            var diagnostic = medicalHistory.Diagnostics.FirstOrDefault(d => d.Id == diagnosticId);

            return diagnostic;
        }

        public async Task<Diagnostic> AddDiagnosticAsync(string medicalHistoryId, Diagnostic diagnostic)
        {
            var newId = ObjectId.GenerateNewId();
            diagnostic.Id = newId.ToString();

            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var update = Builders<MedicalHistory>.Update.Push(m => m.Diagnostics, diagnostic);

            await _medicalHistories.UpdateOneAsync(filter, update);

            return diagnostic;
        }

        public async Task UpdateDiagnosticAsync(string medicalHistoryId, string diagnosticId, Diagnostic diagnostic)
        {
    
            var filter = Builders<MedicalHistory>.Filter.And(
            Builders<MedicalHistory>.Filter.Eq("_id", medicalHistoryId),
            Builders<MedicalHistory>.Filter.ElemMatch("Diagnostics", Builders<Diagnostic>.Filter.Eq("Id", diagnosticId))
               );

            var update = Builders<MedicalHistory>.Update
                .Set("Diagnostics.$", diagnostic); // Usa el operador de posición $ para actualizar el diagnóstico específico

            await _medicalHistories.UpdateOneAsync(filter, update);
        }

        public async Task DeleteDiagnosticAsync(string medicalHistoryId, string diagnosticId)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var update = Builders<MedicalHistory>.Update.PullFilter(m => m.Diagnostics, Builders<Diagnostic>.Filter.Eq(d => d.Id, diagnosticId));

            await _medicalHistories.UpdateOneAsync(filter, update);
        }

        public async Task<Treatment> GetTreatmentAsync(string medicalHistoryId, string treatmentId)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var medicalHistory = await _medicalHistories.Find(filter).FirstOrDefaultAsync();

            if (medicalHistory == null)
            {
                return null;
            }

            var treatment = medicalHistory.Treatments.FirstOrDefault(t => t.Id == treatmentId);

            return treatment;
        }

        public async Task<Treatment> AddTreatmentAsync(string medicalHistoryId, Treatment treatment)
        {
            var newId = ObjectId.GenerateNewId();
            treatment.Id = newId.ToString();

            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var update = Builders<MedicalHistory>.Update.Push(m => m.Treatments, treatment);

            await _medicalHistories.UpdateOneAsync(filter, update);

            return treatment;
        }

        public async Task UpdateTreatmentAsync(string medicalHistoryId, string treatmentId, Treatment treatment)
        {
            var filter = Builders<MedicalHistory>.Filter.And(
            Builders<MedicalHistory>.Filter.Eq("_id", medicalHistoryId),
            Builders<MedicalHistory>.Filter.ElemMatch("Treatments", Builders<Treatment>.Filter.Eq("Id", treatmentId))
               );

            var update = Builders<MedicalHistory>.Update
                .Set("Treatments.$", treatment); // Usa el operador de posición $ para actualizar el diagnóstico específico

            await _medicalHistories.UpdateOneAsync(filter, update);
        }

        public async Task DeleteTreatmentAsync(string medicalHistoryId, string treatmentId)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var update = Builders<MedicalHistory>.Update.PullFilter(m => m.Treatments, Builders<Treatment>.Filter.Eq(d => d.Id, treatmentId));

            await _medicalHistories.UpdateOneAsync(filter, update);
        }

        public async Task<Procedure> GetProcedureAsync(string medicalHistoryId, string procedureId)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var medicalHistory = await _medicalHistories.Find(filter).FirstOrDefaultAsync();

            if (medicalHistory == null)
            {
                return null;
            }

            var procedure = medicalHistory.Procedures.FirstOrDefault(t => t.Id == procedureId);

            return procedure;
        }

        public async Task<Procedure> AddProcedureAsync(string medicalHistoryId, Procedure procedure)
        {
            var newId = ObjectId.GenerateNewId();
            procedure.Id = newId.ToString();

            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var update = Builders<MedicalHistory>.Update.Push(m => m.Procedures, procedure);

            await _medicalHistories.UpdateOneAsync(filter, update);

            return procedure;
        }

        public async Task UpdateProcedureAsync(string medicalHistoryId, string procedureId, Procedure procedure)
        {
            var filter = Builders<MedicalHistory>.Filter.And(
            Builders<MedicalHistory>.Filter.Eq("_id", medicalHistoryId),
            Builders<MedicalHistory>.Filter.ElemMatch("Procedures", Builders<Procedure>.Filter.Eq("Id", procedureId))
               );

            var update = Builders<MedicalHistory>.Update
                .Set("Procedures.$", procedure); // Usa el operador de posición $ para actualizar el diagnóstico específico

            await _medicalHistories.UpdateOneAsync(filter, update);
        }

        public async Task DeleteProcedureAsync(string medicalHistoryId, string procedureId)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.Id, medicalHistoryId);
            var update = Builders<MedicalHistory>.Update.PullFilter(m => m.Procedures, Builders<Procedure>.Filter.Eq(d => d.Id, procedureId));

            await _medicalHistories.UpdateOneAsync(filter, update);
        }

         public async Task<IEnumerable<MedicalHistory>> GetMedicalHistoriesByPatientAsync(string patientId)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.PatientId, patientId);
            var medicalHistories = await _medicalHistories.Find(filter).ToListAsync();
            return medicalHistories;
        }

        public async Task<MedicalHistory> GetMedicalHistoryByIdCitaAsync(string idCita)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq(m => m.IdCita, idCita);
            return await _medicalHistories.Find(filter).FirstOrDefaultAsync();
        }

    }
}


namespace ProyectoApiHistorias.Controllers
{
    public interface IMongoDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string NotesCollectionName { get; set; }
    }

    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string NotesCollectionName { get; set; }
    }
}
