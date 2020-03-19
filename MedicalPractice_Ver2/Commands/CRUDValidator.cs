using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalPractice_Ver2.Commands
{
    class CRUDValidator
    {
        private bool result;
        public string error_msg;
        public bool Name(string firstname, string lastname)
        {
            if (firstname == null && lastname == null)
            {
                result = false;
                error_msg = "Please make sure that you've entered your full name";
                MessageBox.Show(error_msg);
            }
            else if (firstname.Length > 180 && lastname.Length > 180)
            {
                result = false;
                error_msg = "Input must not exceed 180 characters";
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool DOB(DateTime dob)
        {
            if(dob == null)
            {
                result = false;
                error_msg = "Please make sure that you've entered your Date of Birth";
                
            }
            else if (dob >= DateTime.Now)
            {
                result = false;
                error_msg = "Your date of birth should not be the date today or the future.";
            }
            else
            {
                result = true;
            }

            return result;
        }

        public bool ContactNumber(string mobileNum, string homeNum)
        {
            if(mobileNum == "" || homeNum == "")
            {
                result = false;
                error_msg = "Please make sure that you've entered atleast one contact number.";
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
