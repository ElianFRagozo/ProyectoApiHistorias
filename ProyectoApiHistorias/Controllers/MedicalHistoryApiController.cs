using Microsoft.AspNetCore.Mvc;
using MedicalHistoryAPI.Models;
using MedicalHistoryAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalHistoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalHistoriesController : ControllerBase
    {
        private readonly MedicalHistoryService _medicalHistoryService;

        public MedicalHistoriesController(MedicalHistoryService medicalHistoryService)
        {
            _medicalHistoryService = medicalHistoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistory>> GetMedicalHistory(string id)
        {
            var medicalHistory = await _medicalHistoryService.GetMedicalHistoryAsync(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            return Ok(medicalHistory);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalHistory>> CreateMedicalHistory([FromBody] MedicalHistory medicalHistory)
        {
            medicalHistory.FechaCreacion = DateTime.UtcNow;

            var createdMedicalHistory = await _medicalHistoryService.CreateMedicalHistoryAsync(medicalHistory);
            return CreatedAtAction(nameof(GetMedicalHistory), new { id = createdMedicalHistory.Id }, createdMedicalHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalHistory(string id, [FromBody] MedicalHistory medicalHistory)
        {
            if (id != medicalHistory.Id)
            {
                return BadRequest();
            }
            await _medicalHistoryService.UpdateMedicalHistoryAsync(id, medicalHistory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalHistory(string id)
        {
            await _medicalHistoryService.DeleteMedicalHistoryAsync(id);
            return NoContent();
        }

        [HttpGet("{medicalHistoryId}/diagnostics/{diagnosticId}")]
        public async Task<ActionResult<Diagnostic>> GetDiagnostic(string medicalHistoryId, string diagnosticId)
        {
            var diagnostic = await _medicalHistoryService.GetDiagnosticAsync(medicalHistoryId, diagnosticId);
            if (diagnostic == null)
            {
                return NotFound();
            }
            return Ok(diagnostic);
        }

        [HttpPost("{medicalHistoryId}/diagnostics")]
        public async Task<ActionResult<Diagnostic>> AddDiagnostic(string medicalHistoryId, [FromBody] Diagnostic diagnostic)
        {
            var createdDiagnostic = await _medicalHistoryService.AddDiagnosticAsync(medicalHistoryId, diagnostic);
            return CreatedAtAction(nameof(GetDiagnostic), new { medicalHistoryId = medicalHistoryId, diagnosticId = createdDiagnostic.Id }, createdDiagnostic);
        }

        [HttpPut("{medicalHistoryId}/diagnostics/{diagnosticId}")]
        public async Task<IActionResult> UpdateDiagnostic(string medicalHistoryId, string diagnosticId, [FromBody] Diagnostic diagnostic)
        {
            if (diagnosticId != diagnostic.Id)
            {
                return BadRequest();
            }
            await _medicalHistoryService.UpdateDiagnosticAsync(medicalHistoryId, diagnosticId, diagnostic);
            return NoContent();
        }

        [HttpDelete("{medicalHistoryId}/diagnostics/{diagnosticId}")]
        public async Task<IActionResult> DeleteDiagnostic(string medicalHistoryId, string diagnosticId)
        {
            await _medicalHistoryService.DeleteDiagnosticAsync(medicalHistoryId, diagnosticId);
            return NoContent();
        }

        [HttpGet("{medicalHistoryId}/treatments/{treatmentId}")]
        public async Task<ActionResult<Treatment>> GetTreatment(string medicalHistoryId, string treatmentId)
        {
            var treatment = await _medicalHistoryService.GetTreatmentAsync(medicalHistoryId, treatmentId);
            if (treatment == null)
            {
                return NotFound();
            }
            return Ok(treatment);
        }

        [HttpPost("{medicalHistoryId}/treatments")]
        public async Task<ActionResult<Treatment>> AddTreatment(string medicalHistoryId, [FromBody] Treatment treatment)
        {
            var createdTreatment = await _medicalHistoryService.AddTreatmentAsync(medicalHistoryId, treatment);
            return CreatedAtAction(nameof(GetTreatment), new { medicalHistoryId = medicalHistoryId, treatmentId = createdTreatment.Id }, createdTreatment);
        }

        [HttpPut("{medicalHistoryId}/treatments/{treatmentId}")]
        public async Task<IActionResult> UpdateTreatment(string medicalHistoryId, string treatmentId, [FromBody] Treatment treatment)
        {
            if (treatmentId != treatment.Id)
            {
                return BadRequest();
            }
            await _medicalHistoryService.UpdateTreatmentAsync(medicalHistoryId, treatmentId, treatment);
            return NoContent();
        }

        [HttpDelete("{medicalHistoryId}/treatments/{treatmentId}")]
        public async Task<IActionResult> DeleteTreatment(string medicalHistoryId, string treatmentId)
        {
            await _medicalHistoryService.DeleteTreatmentAsync(medicalHistoryId, treatmentId);
            return NoContent();
        }

        [HttpGet("{medicalHistoryId}/procedures/{procedureId}")]
        public async Task<ActionResult<Procedure>> GetProcedure(string medicalHistoryId, string procedureId)
        {
            var procedure = await _medicalHistoryService.GetProcedureAsync(medicalHistoryId, procedureId);
            if (procedure == null)
            {
                return NotFound();
            }
            return Ok(procedure);
        }

        [HttpPost("{medicalHistoryId}/procedures")]
        public async Task<ActionResult<Procedure>> AddProcedure(string medicalHistoryId, [FromBody] Procedure procedure)
        {
            var createdProcedure = await _medicalHistoryService.AddProcedureAsync(medicalHistoryId, procedure);
            return CreatedAtAction(nameof(GetProcedure), new { medicalHistoryId = medicalHistoryId, procedureId = createdProcedure.Id }, createdProcedure);
        }

        [HttpPut("{medicalHistoryId}/procedures/{procedureId}")]
        public async Task<IActionResult> UpdateProcedure(string medicalHistoryId, string procedureId, [FromBody] Procedure procedure)
        {
            if (procedureId != procedure.Id)
            {
                return BadRequest();
            }
            await _medicalHistoryService.UpdateProcedureAsync(medicalHistoryId, procedureId, procedure);
            return NoContent();
        }

        [HttpDelete("{medicalHistoryId}/procedures/{procedureId}")]
        public async Task<IActionResult> DeleteProcedure(string medicalHistoryId, string procedureId)
        {
            await _medicalHistoryService.DeleteProcedureAsync(medicalHistoryId, procedureId);
            return NoContent();
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicalHistory>>> GetMedicalHistoriesByPatient(string patientId)
        {
            var medicalHistories = await _medicalHistoryService.GetMedicalHistoriesByPatientAsync(patientId);
            if (medicalHistories == null || !medicalHistories.Any())
            {
                return NotFound("No se encontraron historias médicas para el paciente especificado.");
            }

            return Ok(medicalHistories);
        }

        [HttpGet("cita/{idCita}")]
        public async Task<ActionResult<MedicalHistory>> GetMedicalHistoryByIdCita(string idCita)
        {
            var medicalHistory = await _medicalHistoryService.GetMedicalHistoryByIdCitaAsync(idCita);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            return Ok(medicalHistory);
        }
    }
}
