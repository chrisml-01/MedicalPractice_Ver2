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
    class PractitionerVM : NotifyClass
    {
        private ObservableCollection<Practitioner> _practitioner = new ObservableCollection<Practitioner>();
        private ObservableCollection<PracType> _pracType = new ObservableCollection<PracType>();
        private ObservableCollection<Days> _days = new ObservableCollection<Days>();
        private Practitioner _selectedPractitioner;        
        private int rowsAffected;
        private string _searchInput;

        //string input to bind to search input from uc: SearchBox
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; }
        }

        public ObservableCollection<Practitioner> Practitioners
        {
            get { return _practitioner; }
            set { _practitioner = value; }
        }

        public ObservableCollection<PracType> PracTypes
        {
            get { return _pracType; }
            set { _pracType = value; }
        }
        public ObservableCollection<Days> Days
        {
            get { return _days; }
            set { _days = value; }
        }

        public Practitioner SelectedPractitioner
        {
            get { return _selectedPractitioner; }
            set
            {
                _selectedPractitioner = value;
                getAvail();
                OnPropertyChanged("SelectedPractitioner");
            }
        }

        //constructor
        public PractitionerVM()
        {
            DisplayAllPractitioner();
            DisplayPracType();
            DisplayDays();
            
        }

        //BUTTON COMMANDS FOR UC: Buttons
        public RelayCommand Save
        {
            get { return new RelayCommand(InsertPractitioner, true); }
        }
        public RelayCommand Update
        {
            get { return new RelayCommand(UpdatePractitioner, true); }
        }
        public RelayCommand Delete
        {
            get { return new RelayCommand(DeletePractitioner, true); }
        }
        public RelayCommand Search
        {
            get { return new RelayCommand(SearchPractitioner, true); }
        }
        public RelayCommand AddNew
        {
            get { return new RelayCommand(AddPractitioner, true); }
        }

        //Method to displayAllPractitioner 
        public void DisplayAllPractitioner()
        {
            var allPractitioner = PractitionerDB.GetAllPractitioners();
            foreach (var item in allPractitioner)
            {
                Practitioners.Add(item);
            }
        }

        public void DisplayPracType()
        {
            var allPracType = AvailabilityDB.GetAllPracType();
            foreach (var item in allPracType)
            {
                PracTypes.Add(item);
            }
        }

        public void DisplayDays()
        {
            var alldays = AvailabilityDB.GetAllDays();
            foreach (var item in alldays)
            {
                Days.Add(item);
            }
        }

        public void getAvail()
        {
            foreach(Days days in Days)
            {
                days.isChecked = false;
                foreach (Days avail in AvailabilityDB.GetAvailabilities(SelectedPractitioner))
                {
                    if(avail.dayID == days.dayID)
                    {
                        days.isChecked = true;
                    }
                }
            }
        }

        //Method to call the searchpatient instance from the PatientDB.
        //This method will be attached to the button 'Search' command.
        private void SearchPractitioner()
        {
            Practitioners.Clear();
            var search = PractitionerDB.SearchPractitioner(SearchInput);
            foreach (var item in search)
            {
                Practitioners.Add(item);
            }
        }

        //Method to call the instance of insertPatient from the PatientDB.
        //This method will be attached to the button 'Insert' command.
        private void InsertPractitioner()
        {
            try
            {
                rowsAffected = PractitionerDB.insertPractitioner(SelectedPractitioner);
                insertDays(SelectedPractitioner);
            
                //Practitioners.Clear();
                //DisplayAllPractitioner();

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Practitioner Added");
                }
                else
                {
                    MessageBox.Show("Insert Failed");
                }


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void insertDays(Practitioner practitioner)
        {

            int lastRow = Practitioners.Count;

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    foreach (Days d in Days)
                    {
                        if (d.isChecked == true)
                        {
                            rowsAffected = AvailabilityDB.insertAvailDays(practitioner.practitionerID, d.dayID);
                        }
                    }
                }
            }
         
        }

        private void UpdatePractitioner()
        {
            try
            {
                rowsAffected = PractitionerDB.updatePractitioner(SelectedPractitioner);
               
                int delete = AvailabilityDB.deleteAvailDays(SelectedPractitioner.practitionerID);

                foreach (Days d in Days)
                {
                    if (d.isChecked == true)
                    {
                        rowsAffected = AvailabilityDB.insertAvailDays(SelectedPractitioner.practitionerID, d.dayID);
                    }
                }

                //Practitioners.Clear();
                //DisplayAllPractitioner();

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Practitioner Updated");
                }
                else
                {
                    MessageBox.Show("Update Failed");
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void DeletePractitioner()
        {
            MessageBox.Show("Deletion is not supported");
        }

        private void AddPractitioner()
        {
            Practitioner practitioner = new Practitioner();

            //adding additional row after the last row
            int lastRow = Practitioners.Count;

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Practitioners.Add(practitioner);
                }
            }

        }

    }
}
