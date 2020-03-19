using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPractice_Ver2.Model
{
    class Appointment
    {
        public int appointmentID { get; set; }
        public int patientID { get; set; }
        public int practitionerID { get; set; }
        public int appntType { get; set; }
        public DateTime appntDate { get; set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string notes { get; set; }
        public string status { get; set; }

    }

    class AppointmentType
    {
        public int typeID { get; set; }
        public string typeName { get; set; }
    }
}
