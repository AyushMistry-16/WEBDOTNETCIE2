using HospitalAppointmentMVC.Extensions;
using HospitalAppointmentMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalAppointmentMVC.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public DoctorController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        private List<Doctor> GetDoctors()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    DoctorName = "Dr. Raj Patel",
                    Specialization = "Cardiologist",
                    Experience = 10,
                    ConsultationFee = 800
                },

                new Doctor
                {
                    DoctorId = 2,
                    DoctorName = "Dr. Neha Shah",
                    Specialization = "Dermatologist",
                    Experience = 7,
                    ConsultationFee = 600
                },

                new Doctor
                {
                    DoctorId = 3,
                    DoctorName = "Dr. Amit Mehta",
                    Specialization = "Neurologist",
                    Experience = 12,
                    ConsultationFee = 1000
                },

                new Doctor
                {
                    DoctorId = 4,
                    DoctorName = "Dr. Priya Desai",
                    Specialization = "Pediatrician",
                    Experience = 8,
                    ConsultationFee = 700
                }
            };
        }

        // Doctor List
        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue(
                "DoctorList",
                out List<Doctor>? doctors))
            {
                doctors = GetDoctors();

                var cacheOptions =
                    new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(
                            TimeSpan.FromMinutes(3)
                        );

                _memoryCache.Set(
                    "DoctorList",
                    doctors,
                    cacheOptions
                );
            }

            return View(doctors);
        }

        // Doctor Details
        [ResponseCache(Duration = 60)]
        public IActionResult Details(int doctorId)
        {
            var doctors = GetDoctors();

            var doctor = doctors.FirstOrDefault(
                d => d.DoctorId == doctorId
            );

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }
    }
}