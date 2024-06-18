using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MedicalHistoryAPI.Models
{
    public class MedicalHistory
    {
        [BsonId]
        public string Id { get; set; }

        public string PatientId { get; set; }

        [BsonElement("MedicoId")]
        public string MedicoId { get; set; }

        public string IdCita { get; set; }

        [BsonElement("MedicoNombre")]
        public string MedicoNombre { get; set; }

        [BsonElement("MedicoEspecialidad")]
        public string MedicoEspecialidad { get; set; }

        public List<Diagnostic> Diagnostics { get; set; }

        public List<Treatment> Treatments { get; set; }

        public List<Procedure> Procedures { get; set; }

        
        public DateTime FechaCreacion { get; set; }

    }

    public class Diagnostic
    {
        [BsonId]
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }

    public class Treatment
    {
        [BsonId]
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }

    public class Procedure
    {
        [BsonId]
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
