namespace WebApplication1.Dto;

public class PatientInfoDto(
    int idPatient,
    string firstName,
    string lastName,
    DateOnly birthdate,
    List<PatientInfoPrescriptionDto> prescriptions
)
{
    public int IdPatient { get; } = idPatient;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public DateOnly Birthdate { get; } = birthdate;
    public List<PatientInfoPrescriptionDto> Prescriptions { get; } = prescriptions;
}

public class PatientInfoPrescriptionDto(
    int idPrescription,
    DateOnly date,
    DateOnly dueDate,
    List<PatientInfoMedicamentsDto> medicaments,
    PatientInfoDoctorDto doctor
)
{
    public int IdPrescription { get; } = idPrescription;
    public DateOnly Date { get; } = date;
    public DateOnly DueDate { get; } = dueDate;
    public List<PatientInfoMedicamentsDto> Medicaments { get; } = medicaments;
    public PatientInfoDoctorDto Doctor { get; } = doctor;
}

public class PatientInfoMedicamentsDto(
    int idMedicament,
    string name,
    int dose,
    string description
)
{
    public int IdMedicament { get; } = idMedicament;
    public string Name { get; } = name;
    public int Dose { get; } = dose;
    public string Description { get; } = description;
}

public class PatientInfoDoctorDto(
    int idDoctor,
    string firstName,
    string lastName
)
{
    public int IdDoctor { get; } = idDoctor;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
}