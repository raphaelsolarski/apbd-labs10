using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;

namespace WebApplication1.Repository;

public class PatientsRepository : IPatientsRepository
{
    public Patient? findPatientByProperties(PrescriptionsContext context, string firstName, string lastName, DateOnly birthdate)
    {
        return context.Patients.SingleOrDefault(p =>
            p.Birthdate == birthdate &&
            p.FirstName == firstName &&
            p.LastName == lastName);
    }

    public Patient CreatePatient(PrescriptionsContext context, Patient patient)
    {
        context.Patients.Add(patient);
        return patient;
    }

    public Patient? findPatientByIdWithAllFetched(PrescriptionsContext context, int id)
    {
        return context.Patients
            .Include("Prescriptions.PrescriptionMedicaments.Medicament")
            .Include("Prescriptions.Doctor")
            .SingleOrDefault(p => p.IdPatient == id);
    }
}