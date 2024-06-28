using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("/api/patients")]
[ApiController]
public class PatientsController(IPatientsService patientsService) : ControllerBase
{
    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetPatientInfo(int id)
    {
        var info = patientsService.GetPatientInfo(id);
        if (info != null)
        {
            return Ok(info);
        }

        return NotFound("Can't find patient with specified id");
    }
}