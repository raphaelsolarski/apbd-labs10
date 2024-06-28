using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("/api/prescriptions")]
[ApiController]
public class PrescriptionController(IPrescriptionService prescriptionService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public IActionResult PostPrescription(CreatePrescriptionDto createPrescriptionDto)
    {
        try
        {
            prescriptionService.PostPrescription(createPrescriptionDto);
        }
        catch (Exception e)
        {
            if (e is MedicamentDoesNotExistException || e is DueDateIsBeforeDateException ||
                e is NotExistingDoctorException)
            {
                return BadRequest(e.Message);
            }

            throw new Exception("Unexpected during creating prescription", e);
        }

        return Created();
    }
}