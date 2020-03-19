using MedicalPractice_Ver2.Commands;
using MedicalPractice_Ver2.DB;
using MedicalPractice_Ver2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalPractice_Ver2.ViewModel
{
    class AppointmentVM : NotifyClass
    {
        private ObservableCollection<Appointment> _appointment = new ObservableCollection<Appointment>();
        private ObservableCollection<AppointmentType> _appointmentType = new ObservableCollection<AppointmentType>();
        private Appointment _selectedAppointment;
        private int rowsAffected;
        private string _searchInput;

        //string input to bind to search input from uc: SearchBox
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; }
        }

        public ObservableCollection<Appointment> Appointments
        {
            get { return _appointment; }
            set { _appointment = value; }
        }

        public ObservableCollection<AppointmentType> AppointmentTypes
        {
            get { return _appointmentType; }
            set { _appointmentType = value; }
        }

        public Appointment SelectedAppointment
        {
            get { return _selectedAppointment; }
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }

        //constructor
        public AppointmentVM()
        {
            DisplayAllAppointments();
            DisplayAppointmentTypes();
        }

        public void DisplayAllAppointments()
        {
            var allAppointments = AppointmentDB.GetAllAppointments();
            foreach (var item in allAppointments)
            {
                Appointments.Add(item);
            }
        }

        public void DisplayAppointmentTypes()
        {
            var allAppointmentTypes = AppointmentDB.GetAppointmentTypes();
            foreach (var item in allAppointmentTypes)
            {
                AppointmentTypes.Add(item);
            }
        }

        //BUTTON COMMANDS FOR UC: Buttons
        public RelayCommand Save
        {
            get { return new RelayCommand(InsertApppointment, true); }
        }
        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateAppointment, true); }
        }
        //public RelayCommand Delete
        //{
        //    get { return new RelayCommand(DeletePractitioner, true); }
        //}
        //public RelayCommand Search
        //{
        //    get { return new RelayCommand(SearchPractitioner, true); }
        //}
        public RelayCommand AddNew
        {
            get { return new RelayCommand(AddAppointment, true); }
        }

        //Method to call the searchpatient instance from the PatientDB.
        //This method will be attached to the button 'Search' command.
        //private void SearchAppointment()
        //{
        //    Appointments.Clear();
        //    var search = PatientDB.SearchPatient(SearchInput);
        //    foreach (var item in search)
        //    {
        //        Patients.Add(item);
        //    }
        //}

        private void AddAppointment()
        {
            Appointment appointment = new Appointment();

            //adding additional row after the last row
            int lastRow = Appointments.Count;

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Appointments.Add(appointment);
                }
            }

        }
        //Method to call the instance of insertPatient from the PatientDB.
        //This method will be attached to the button 'Insert' command.
        private void InsertApppointment()
        {
            try
            {
                rowsAffected = AppointmentDB.insertAppointment(SelectedAppointment);

                Appointments.Clear();
                DisplayAllAppointments();

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Appointment Added");
                }
                else
                {
                    MessageBox.Show("Insert Failed");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        //Method to call the instance of insertPatient from the PatientDB.
        //This method will be attached to the button 'Insert' command.
        private void UpdateAppointment()
        {
            try
            {
                rowsAffected = AppointmentDB.updateAppointment(SelectedAppointment);

                Appointments.Clear();
                DisplayAllAppointments();

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Appointment updated");
                }
                else
                {
                    MessageBox.Show("Update Failed");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

    }
}
