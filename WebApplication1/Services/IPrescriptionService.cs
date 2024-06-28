using WebApplication1.Dto;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    void PostPrescription(CreatePrescriptionDto createPrescriptionDto);
    
}