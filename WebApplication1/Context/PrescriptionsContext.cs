using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Context;

public class PrescriptionsContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    
    public DbSet<AppUser> AppUsers { get; set; }

    public PrescriptionsContext()
    {
    }

    public PrescriptionsContext(DbContextOptions<PrescriptionsContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            "Server=127.0.0.1,1433; Database=Prescriptions; User Id=SA; Password=Foo_bar_3;Encrypt=False");
}

public class Medicament
{
    [Key] public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }

    public List<PrescriptionMedicament> PrescriptionMedicaments { get; } = new();
}

[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
}

public class Prescription
{
    [Key] public int IdPrescription { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    public List<PrescriptionMedicament> PrescriptionMedicaments { get; } = new();
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}

public class Doctor
{
    [Key] public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<Prescription> Prescriptions { get; } = new();
}

public class Patient
{
    [Key] public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public List<Prescription> Prescriptions { get; } = new();
}


public class AppUser
{
    [Key] public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExp { get; set; }
}