namespace HospitalAppointmentMVC.Models
{
    public class Appointment
    {
        public int DoctorId { get; set; }

        public string PatientName { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public DateTime AppointmentDate { get; set; }
    }
}