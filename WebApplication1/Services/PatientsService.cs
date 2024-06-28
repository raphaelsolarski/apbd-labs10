using WebApplication1.Context;
using WebApplication1.Dto;
using WebApplication1.Repository;

namespace WebApplication1.Services;

public class PatientsService(PrescriptionsContext prescriptionsContext, IPatientsRepository patientsRepository)
    : IPatientsService
{
    public PatientInfoDto? GetPatientInfo(int id)
    {
        var patient = patientsRepository.findPatientByIdWithAllFetched(prescriptionsContext, id);
        if (patient == null) return null;
        return new PatientInfoDto(
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.Birthdate,
            patient.Prescriptions.Select(p =>
                new PatientInfoPrescriptionDto(
                    p.IdPrescription,
                    p.Date,
                    p.DueDate,
                    p.PrescriptionMedicaments.Select(pm =>
                        new PatientInfoMedicamentsDto(pm.Medicament.IdMedicament, pm.Medicament.Name, pm.Dose,
                            pm.Medicament.Description)
                    ).ToList(),
                    new PatientInfoDoctorDto(p.Doctor.IdDoctor, p.Doctor.FirstName, p.Doctor.LastName)
                )
            ).ToList()
        );
    }
}