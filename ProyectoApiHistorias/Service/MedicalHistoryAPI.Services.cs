using MedicalHistoryAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MedicalHistoryAPI.Services
{
    public interface IMedicalHistoryService
    {
        Task<MedicalHistory> GetMedicalHistoryAsync(string id);
        Task<MedicalHistory> CreateMedicalHistoryAsync(MedicalHistory medicalHistory);
        Task UpdateMedicalHistoryAsync(string id, MedicalHistory medicalHistory);
        Task DeleteMedicalHistoryAsync(string id);

        Task<Diagnostic> GetDiagnosticAsync(string medicalHistoryId, string diagnosticId);
        Task<Diagnostic> AddDiagnosticAsync(string medicalHistoryId, Diagnostic diagnostic);
        Task UpdateDiagnosticAsync(string medicalHistoryId, string diagnosticId, Diagnostic diagnostic);
        Task DeleteDiagnosticAsync(string medicalHistoryId, string diagnosticId);

        Task<Treatment> GetTreatmentAsync(string medicalHistoryId, string treatmentId);
        Task<Treatment> AddTreatmentAsync(string medicalHistoryId, Treatment treatment);
        Task UpdateTreatmentAsync(string medicalHistoryId, string treatmentId, Treatment treatment);
        Task DeleteTreatmentAsync(string medicalHistoryId, string treatmentId);

        Task<Procedure> GetProcedureAsync(string medicalHistoryId, string procedureId);
        Task<Procedure> AddProcedureAsync(string medicalHistoryId, Procedure procedure);
        Task UpdateProcedureAsync(string medicalHistoryId, string procedureId, Procedure procedure);
        Task DeleteProcedureAsync(string medicalHistoryId, string procedureId);

        Task<Attachment> GetAttachmentAsync(string medicalHistoryId, string attachmentId);
        Task<Attachment> AddAttachmentAsync(string medicalHistoryId, Attachment attachment);
        Task DeleteAttachmentAsync(string medicalHistoryId, string attachmentId);
    }

    public class MedicalHistoryService : IMedicalHistoryService
    {
        private readonly IMongoCollection<MedicalHistory> _medicalHistoryCollection;

        public MedicalHistoryService(IMongoDatabase database)
        {
            _medicalHistoryCollection = database.GetCollection<MedicalHistory>("MedicalHistories");
        }

        public Task<Attachment> AddAttachmentAsync(string medicalHistoryId, Attachment attachment)
        {
            throw new NotImplementedException();
        }

        public Task<Diagnostic> AddDiagnosticAsync(string medicalHistoryId, Diagnostic diagnostic)
        {
            throw new NotImplementedException();
        }

        public Task<Procedure> AddProcedureAsync(string medicalHistoryId, Procedure procedure)
        {
            throw new NotImplementedException();
        }

        public Task<Treatment> AddTreatmentAsync(string medicalHistoryId, Treatment treatment)
        {
            throw new NotImplementedException();
        }

        public async Task<MedicalHistory> CreateMedicalHistoryAsync(MedicalHistory medicalHistory)
        {
            var newId = ObjectId.GenerateNewId();

            // Asignar el nuevo ObjectId al campo Id del UserModel
            medicalHistory.Id = newId.ToString();

            await _medicalHistoryCollection.InsertOneAsync(medicalHistory);
            return medicalHistory;
        }

        public Task DeleteAttachmentAsync(string medicalHistoryId, string attachmentId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDiagnosticAsync(string medicalHistoryId, string diagnosticId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMedicalHistoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProcedureAsync(string medicalHistoryId, string procedureId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTreatmentAsync(string medicalHistoryId, string treatmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Attachment> GetAttachmentAsync(string medicalHistoryId, string attachmentId)
        {
            throw new NotImplementedException();
        }

        public Task<Diagnostic> GetDiagnosticAsync(string medicalHistoryId, string diagnosticId)
        {
            throw new NotImplementedException();
        }

        public async Task<MedicalHistory> GetMedicalHistoryAsync(string id)
        {
            var filter = Builders<MedicalHistory>.Filter.Eq("_id", id);
            return await _medicalHistoryCollection.Find(filter).FirstOrDefaultAsync();
        }


        public Task<Procedure> GetProcedureAsync(string medicalHistoryId, string procedureId)
        {
            throw new NotImplementedException();
        }

        public Task<Treatment> GetTreatmentAsync(string medicalHistoryId, string treatmentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDiagnosticAsync(string medicalHistoryId, string diagnosticId, Diagnostic diagnostic)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMedicalHistoryAsync(string id, MedicalHistory medicalHistory)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProcedureAsync(string medicalHistoryId, string procedureId, Procedure procedure)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTreatmentAsync(string medicalHistoryId, string treatmentId, Treatment treatment)
        {
            throw new NotImplementedException();
        }

        // Implementa los métodos de la interfaz aquí
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
