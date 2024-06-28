using WebApplication1.Context;

namespace WebApplication1.Repository;

public interface IDoctorRepository
{
    Doctor? FindDoctorById(int id);
}