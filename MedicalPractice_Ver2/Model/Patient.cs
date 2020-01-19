using MedicalPractice_Ver2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPractice_Ver2.Model
{
    class Patient : NotifyClass
    {
        public int patientID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime DOB { get; set; }
        public string street { get; set; }
        public string suburb { get; set; }
        public string state { get; set; }
        public string postCode { get; set; }
        public string mobileNum { get; set; }
        public string homeNum { get; set; }
        public string email { get; set; }
        public string medicareNum { get; set; }
        public string notes { get; set; }

        public int PatientID
        {
            get { return patientID; }
            set { patientID = value;
                OnPropertyChanged("patientID");
            }
        }
    }
}
