using MedicalPractice_Ver2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPractice_Ver2.DB
{
    class AvailabilityDB
    {
        private static SQLHelper _DB = new SQLHelper("wsmp_constring");

        public static ObservableCollection<PracType> GetAllPracType()
        {
            string strQuery = "SELECT typeID, typeName from pracType";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var pracTypes = new ObservableCollection<PracType>();
            foreach (DataRow dr in dt.Rows)
            {
                PracType pracType = new PracType()
                {
                    typeID = Convert.ToInt32(dr[0]),
                    typeName = dr[1].ToString()

                };
                pracTypes.Add(pracType);
            }

            return pracTypes;
        }

        public static ObservableCollection<Days> GetAllDays()
        {
            string strQuery = "SELECT dayID, dayName from day";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var days = new ObservableCollection<Days>();
            foreach (DataRow dr in dt.Rows)
            {
                Days day = new Days()
                {
                    dayID = Convert.ToInt32(dr[0]),
                    dayName = dr[1].ToString()

                };
                days.Add(day);
            }

            return days;
        }

        public static ObservableCollection<Days> GetAvailabilities(Practitioner p)
        {
           
            string strQuery = "SELECT Availability.DayID, Day.DayName FROM dbo.Availability, dbo.Day, dbo.Practitioner " +
                "WHERE dbo.Availability.DayID = dbo.Day.DayID AND dbo.Availability.PractitionerID = dbo.Practitioner.PractitionerID " +
                "AND Practitioner.PractitionerID = " + p.practitionerID;

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var days = new ObservableCollection<Days>();
            foreach (DataRow dr in dt.Rows)
            {
                Days day = new Days()
                {
                    dayID = Convert.ToInt32(dr[0]),
                    dayName = dr[1].ToString()
                };
                days.Add(day);
            }
            return days;
        }

        public static int insertAvailDays(int practitionerID, int dayID)
        {
            int rowsAffected; 

            string strQuery = "Insert into Availability (PractitionerID, DayID) VALUES (" + practitionerID + ", " +  dayID +")";

            rowsAffected = _DB.NonQuerySql(strQuery);

            return rowsAffected;
        }

        public static int deleteAvailDays(int practitionerID)
        {
            int rowsAffected;

            string strQuery = "Delete from availability where practitionerID = " + practitionerID;

            rowsAffected = _DB.NonQuerySql(strQuery);

            return rowsAffected;
        }
    }
}
