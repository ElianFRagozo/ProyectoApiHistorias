using Microsoft.AspNetCore.Mvc;
using MedicalHistoryAPI.Models;
using MedicalHistoryAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MedicalHistoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalHistoriesController : ControllerBase
    {
        private readonly IMedicalHistoryService _medicalHistoryService;

        public MedicalHistoriesController(IMedicalHistoryService medicalHistoryService)
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

        // Endpoint para agregar un diagnóstico a un historial médico
        [HttpPost("{medicalHistoryId}/diagnostics")]
        public async Task<ActionResult<Diagnostic>> AddDiagnostic(string medicalHistoryId, [FromBody] Diagnostic diagnostic)
        {
            var createdDiagnostic = await _medicalHistoryService.AddDiagnosticAsync(medicalHistoryId, diagnostic);
            return CreatedAtAction(nameof(GetDiagnostic), new { medicalHistoryId = medicalHistoryId, diagnosticId = createdDiagnostic.Id }, createdDiagnostic);
        }

        // Endpoint para actualizar un diagnóstico de un historial médico
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

        // Endpoint para eliminar un diagnóstico de un historial médico
        [HttpDelete("{medicalHistoryId}/diagnostics/{diagnosticId}")]
        public async Task<IActionResult> DeleteDiagnostic(string medicalHistoryId, string diagnosticId)
        {
            await _medicalHistoryService.DeleteDiagnosticAsync(medicalHistoryId, diagnosticId);
            return NoContent();
        }
        // Endpoint para obtener un tratamiento específico de un historial médico
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

        // Endpoint para agregar un tratamiento a un historial médico
        [HttpPost("{medicalHistoryId}/treatments")]
        public async Task<ActionResult<Treatment>> AddTreatment(string medicalHistoryId, [FromBody] Treatment treatment)
        {
            var createdTreatment = await _medicalHistoryService.AddTreatmentAsync(medicalHistoryId, treatment);
            return CreatedAtAction(nameof(GetTreatment), new { medicalHistoryId = medicalHistoryId, treatmentId = createdTreatment.Id }, createdTreatment);
        }

        // Endpoint para actualizar un tratamiento de un historial médico
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

        // Endpoint para eliminar un tratamiento de un historial médico
        [HttpDelete("{medicalHistoryId}/treatments/{treatmentId}")]
        public async Task<IActionResult> DeleteTreatment(string medicalHistoryId, string treatmentId)
        {
            await _medicalHistoryService.DeleteTreatmentAsync(medicalHistoryId, treatmentId);
            return NoContent();
        }

        // Endpoint para obtener un procedimiento específico de un historial médico
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

        // Endpoint para agregar un procedimiento a un historial médico
        [HttpPost("{medicalHistoryId}/procedures")]
        public async Task<ActionResult<Procedure>> AddProcedure(string medicalHistoryId, [FromBody] Procedure procedure)
        {
            var createdProcedure = await _medicalHistoryService.AddProcedureAsync(medicalHistoryId, procedure);
            return CreatedAtAction(nameof(GetProcedure), new { medicalHistoryId = medicalHistoryId, procedureId = createdProcedure.Id }, createdProcedure);
        }

        // Endpoint para actualizar un procedimiento de un historial médico
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

        // Endpoint para eliminar un procedimiento de un historial médico
        [HttpDelete("{medicalHistoryId}/procedures/{procedureId}")]
        public async Task<IActionResult> DeleteProcedure(string medicalHistoryId, string procedureId)
        {
            await _medicalHistoryService.DeleteProcedureAsync(medicalHistoryId, procedureId);
            return NoContent();
        }
        // Endpoint para obtener un archivo adjunto específico de un historial médico
        [HttpGet("{medicalHistoryId}/attachments/{attachmentId}")]
        public async Task<ActionResult<Attachment>> GetAttachment(string medicalHistoryId, string attachmentId)
        {
            var attachment = await _medicalHistoryService.GetAttachmentAsync(medicalHistoryId, attachmentId);
            if (attachment == null)
            {
                return NotFound();
            }
            return Ok(attachment);
        }

        // Endpoint para agregar un archivo adjunto a un historial médico
        [HttpPost("{medicalHistoryId}/attachments")]
        public async Task<ActionResult<Attachment>> AddAttachment(string medicalHistoryId, [FromForm] AttachmentRequest attachmentRequest)
        {
            if (attachmentRequest.File.Length > 0)
            {
                using var ms = new MemoryStream();
                await attachmentRequest.File.CopyToAsync(ms);
                var fileBytes = ms.ToArray();

                var attachment = new Attachment
                {
                    FileName = attachmentRequest.FileName,
                    FilePath = Convert.ToBase64String(fileBytes)
                };

                var createdAttachment = await _medicalHistoryService.AddAttachmentAsync(medicalHistoryId, attachment);
                return CreatedAtAction(nameof(GetAttachment), new { medicalHistoryId = medicalHistoryId, attachmentId = createdAttachment.Id }, createdAttachment);
            }

            return BadRequest();
        }

        // Endpoint para eliminar un archivo adjunto de un historial médico
        [HttpDelete("{medicalHistoryId}/attachments/{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(string medicalHistoryId, string attachmentId)
        {
            await _medicalHistoryService.DeleteAttachmentAsync(medicalHistoryId, attachmentId);
            return NoContent();
        }

    }
}
