using WebApplication1.Context;

namespace WebApplication1.Repository;

public class PrescriptionRepository : IPrescriptionRepository
{
    public Prescription CreatePrescription(PrescriptionsContext context, Prescription prescription)
    {
        context.Prescriptions.Add(prescription);
        return prescription;
    }
}