using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto;

public class CreatePrescriptionDto
{
    [Required] public int idDoctor { get; set; }
    [Required] public string patientFirstName { get; set; }
    [Required] public string patientLastName { get; set; }
    [Required] public DateOnly patientBirthdate { get; set; }
    [Required] public DateOnly date { get; set; }
    [Required] public DateOnly dueDate { get; set; }
    [Required] [MaxLength(10)] public List<CreatePrescriptionMedicamentDto> medicaments { get; set; }
}

public class CreatePrescriptionMedicamentDto(
)
{
    [Required] public int IdMedicament { get; set; }
    [Required] public int Dose { get; set; }
    [Required] public string Details { get; set; }
}