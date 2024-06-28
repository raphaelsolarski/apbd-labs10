using WebApplication1.Context;

namespace WebApplication1.Repository;

public class DoctorRepository(PrescriptionsContext context) : IDoctorRepository
{
    public Doctor? FindDoctorById(int id)
    {
        return context.Doctors.SingleOrDefault(d => d.IdDoctor == id);
    }
}