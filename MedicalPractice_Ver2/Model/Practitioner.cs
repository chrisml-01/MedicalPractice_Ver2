using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPractice_Ver2.Model
{
    class Practitioner
    {
        public int practitionerID { get; set; }
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
        public string medregNum { get; set; }
        public int practType{ get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    class PracType
    {
        public int typeID { get; set; }
        public string typeName { get; set; }
    }
}
