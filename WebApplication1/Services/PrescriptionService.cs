using WebApplication1.Context;
using WebApplication1.Dto;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public class PrescriptionService(
    PrescriptionsContext context,
    IPatientsRepository patientsRepository,
    IDoctorRepository doctorRepository,
    IPrescriptionRepository prescriptionRepository
)
    : IPrescriptionService
{
    public void PostPrescription(CreatePrescriptionDto createPrescriptionDto)
    {
        var doctor = doctorRepository.FindDoctorById(createPrescriptionDto.idDoctor);
        if (doctor == null)
        {
            throw new NotExistingDoctorException();
        }

        if (createPrescriptionDto.dueDate < createPrescriptionDto.date)
        {
            throw new DueDateIsBeforeDateException();
        }

        var patient = patientsRepository.findPatientByProperties(
            context,
            createPrescriptionDto.patientFirstName,
            createPrescriptionDto.patientLastName,
            createPrescriptionDto.patientBirthdate
        );

        if (patient == null)
        {
            patient = patientsRepository.CreatePatient(context, new Patient
            {
                FirstName = createPrescriptionDto.patientFirstName,
                LastName = createPrescriptionDto.patientLastName,
                Birthdate = createPrescriptionDto.patientBirthdate
            });
        }

        var prescription = prescriptionRepository.CreatePrescription(context, new Prescription()
        {
            Date = createPrescriptionDto.date,
            DueDate = createPrescriptionDto.dueDate,
            Patient = patient,
            Doctor = doctor
        });

        foreach (var medicament in createPrescriptionDto.medicaments)
        {
            var medicamentEntity = context.Medicaments.SingleOrDefault(m => m.IdMedicament == medicament.IdMedicament);
            if (medicamentEntity == null)
            {
                throw new MedicamentDoesNotExistException(medicament.IdMedicament);
            }

            context.PrescriptionMedicaments.Add(new PrescriptionMedicament()
            {
                Prescription = prescription,
                Dose = medicament.Dose,
                Details = medicament.Details,
                Medicament = medicamentEntity
            });
        }

        context.SaveChanges();
    }
}

public class NotExistingDoctorException() : Exception("Doctor with specified id doesn't exist");

public class DueDateIsBeforeDateException() : Exception("Specified due date must be after date");

public class MedicamentDoesNotExistException(int id) : Exception($"Medicament with id={id} doesn't exist");