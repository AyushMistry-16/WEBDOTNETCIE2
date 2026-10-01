using HospitalAppointmentMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentMVC.Controllers
{
    public class AppointmentController : Controller
    {
        // Display appointment form
        [HttpGet]
        public IActionResult Create(int doctorId)
        {
            var appointment = new Appointment
            {
                DoctorId = doctorId
            };

            return View(appointment);
        }

        // Submit appointment
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }

            // Store Patient Name in Cookie
            Response.Cookies.Append(
                "PatientName",
                appointment.PatientName
            );

            // Store Specialization in Session
            HttpContext.Session.SetString(
                "Specialization",
                appointment.Specialization
            );

            return RedirectToAction(
                "Success",
                appointment
            );
        }

        // Success page
        public IActionResult Success(Appointment appointment)
        {
            ViewBag.PatientName =
                Request.Cookies["PatientName"];

            ViewBag.Specialization =
                HttpContext.Session.GetString(
                    "Specialization"
                );

            return View(appointment);
        }
    }
}