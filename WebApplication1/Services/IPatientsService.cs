using WebApplication1.Dto;

namespace WebApplication1.Services;

public interface IPatientsService
{
    PatientInfoDto? GetPatientInfo(int id);
}

