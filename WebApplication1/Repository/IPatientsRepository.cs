using WebApplication1.Context;

namespace WebApplication1.Repository;

public interface IPatientsRepository
{
    Patient? findPatientByProperties(PrescriptionsContext context, string firstName, string lastName, DateOnly birthdate);
    Patient CreatePatient(PrescriptionsContext context, Patient patient);

    Patient? findPatientByIdWithAllFetched(PrescriptionsContext context, int id);
}