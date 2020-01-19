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
    class PatientVM : NotifyClass
    {
        private ObservableCollection<Patient> _patients = new ObservableCollection<Patient>();
        private Patient _selectedPatient;
        private int rowsAffected;
        private string _searchInput;

        //string input to bind to search input from uc: SearchBox
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; }
        }
       
        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set { _patients = value; }
        }

        //SelectedPatient to bind to DataGrid
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set { _selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }

        //Constructor
        public PatientVM()
        {
            //display all patients to the datagrid
            DisplayAllPatients();
        }


        //BUTTON COMMANDS FOR UC: Buttons
        public RelayCommand Save
        {
            get { return new RelayCommand(InsertPatient, true); }
        }
        public RelayCommand Update
        {
            get { return new RelayCommand(UpdatePatient, true); }
        }
        public RelayCommand Delete
        {
            get { return new RelayCommand(DeletePatient, true); }
        }
        public RelayCommand Search
        {
            get { return new RelayCommand(SearchPatient, true); }
        }
        public RelayCommand AddNew
        {
            get { return new RelayCommand(AddPatient, true); }
        }

        //Method to displayAllPatients 
        public void DisplayAllPatients()
        {
            var allPatients = PatientDB.GetAllPatients();
            foreach (var item in allPatients)
            {
                Patients.Add(item);
            }
        }

        //Method to call the searchpatient instance from the PatientDB.
        //This method will be attached to the button 'Search' command.
        private void SearchPatient()
        {
            Patients.Clear();
            var search = PatientDB.SearchPatient(SearchInput);
            foreach(var item in search)
            {
                Patients.Add(item);
            }
        }

        //Method to call the instance of insertPatient from the PatientDB.
        //This method will be attached to the button 'Insert' command.
        private void InsertPatient()
        {
            try
            {
                rowsAffected = PatientDB.insertPatient(SelectedPatient);

                Patients.Clear();
                DisplayAllPatients();                

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Patient Added");
                }
                else
                {
                    MessageBox.Show("Insert Failed");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }


        private void UpdatePatient()
        {
            try
            {
                rowsAffected = PatientDB.updatePatient(SelectedPatient);

                Patients.Clear();
                DisplayAllPatients();

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Patient Updated");
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

        private void DeletePatient()
        {
            try
            {
                //If button is clicked, a messagebox will be shown to make sure that the user wants to delete the patient
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the patient?", "Delete", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation, MessageBoxResult.No);

                if(result == MessageBoxResult.Yes)
                {
                    rowsAffected = PatientDB.deletePatient(SelectedPatient);

                    Patients.Clear();
                    DisplayAllPatients();

                    if (rowsAffected != 0)
                    {
                        MessageBox.Show("Patient Deleted");
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed");
                    }
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private void AddPatient()
        {
            Patient patient = new Patient();

            //adding additional row after the last row
            int lastRow = Patients.Count;

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Patients.Add(patient);                   
                }
            }
            
           

        }

    }
}
