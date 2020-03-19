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
    class StaffVM : NotifyClass
    {
        private ObservableCollection<Staff> _staff = new ObservableCollection<Staff>();
        private Staff _selectedStaff;
        private int rowsAffected;
        private string _searchInput;

        //string input to bind to search input from uc: SearchBox
        public string SearchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; }
        }

        public ObservableCollection<Staff> Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        public Staff SelectedStaff
        {
            get { return _selectedStaff; }
            set
            {
                _selectedStaff = value;
                OnPropertyChanged("SelectedStaff");
            }
        }

        //Constructor
        public StaffVM()
        {
            DisplayAllStaff();
        }

        //BUTTON COMMANDS FOR UC: Buttons
        public RelayCommand Save
        {
            get { return new RelayCommand(InsertStaff, true); }
        }
        public RelayCommand Update
        {
            get { return new RelayCommand(UpdateStaff, true); }
        }
        public RelayCommand Delete
        {
            get { return new RelayCommand(DeleteStaff, true); }
        }
        public RelayCommand Search
        {
            get { return new RelayCommand(SearchStaff, true); }
        }
        public RelayCommand AddNew
        {
            get { return new RelayCommand(AddNewStaff, true); }
        }

        //Method to displayAllStaff 
        public void DisplayAllStaff()
        {
            var allStaff = StaffDB.GetAllStaff();
            foreach (var item in allStaff)
            {
                Staff.Add(item);
            }
        }

        //Method to call the searchpatient instance from the PatientDB.
        //This method will be attached to the button 'Search' command.
        private void SearchStaff()
        {
            Staff.Clear();
            var search = StaffDB.SearchStaff(SearchInput);
            foreach (var item in search) 
            {
                Staff.Add(item);
            }
        }

        private void InsertStaff()
        {
            try
            {
                rowsAffected = StaffDB.insertStaff(SelectedStaff);

                Staff.Clear();
                DisplayAllStaff();

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Staff Added");
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

        private void UpdateStaff()
        {
            try
            {
                rowsAffected = StaffDB.updateStaff(SelectedStaff);

                Staff.Clear();
                DisplayAllStaff();

                if (rowsAffected != 0)
                {
                    MessageBox.Show("Staff Updated");
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

        private void DeleteStaff()
        {
            try
            {
                //If button is clicked, a messagebox will be shown to make sure that the user wants to delete the patient
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the patient?", "Delete", MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    rowsAffected = StaffDB.deleteStaff(SelectedStaff);

                    Staff.Clear();
                    DisplayAllStaff();

                    if (rowsAffected != 0)
                    {
                        MessageBox.Show("Staff Deleted");
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

        private void AddNewStaff()
        {
            Staff staff = new Staff();

            //adding additional row after the last row
            int lastRow = Staff.Count;

            for (int i = 0; i <= lastRow; i++)
            {
                if (i == lastRow)
                {
                    Staff.Add(staff);
                }
            }
        }
    }
}
