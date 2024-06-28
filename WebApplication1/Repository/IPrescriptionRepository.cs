using WebApplication1.Context;

namespace WebApplication1.Repository;

public interface IPrescriptionRepository
{
    Prescription CreatePrescription(PrescriptionsContext context, Prescription prescription);
}