using MedicalPractice_Ver2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPractice_Ver2.Model
{
    class Availability : NotifyClass
    {

        public int practitionerID { get; set; }
        public int dayID { get; set; }
        
    }

    class Days : NotifyClass
    {
        public int dayID { get; set; }
        public string dayName { get; set; }
        private bool _isChecked = false;
        public bool isChecked
        {
            get => _isChecked;
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    OnPropertyChanged("isChecked");
                }
            }
        }

    }

    class TimeSlot
    {
        public int slotID { get; set; }
        public string timeSlot { get; set; }
    }
}
