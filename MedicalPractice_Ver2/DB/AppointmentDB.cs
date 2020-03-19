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
    class AppointmentDB
    {
        private static SQLHelper _DB = new SQLHelper("wsmp_constring");

        public static ObservableCollection<Appointment> GetAllAppointments()
        {
            
            string strQuery = "SELECT appointmentID, patientID, practitionerID, appTypeID, appDate, startTime, endTime, notes, status from appointment";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var allAppointments = new ObservableCollection<Appointment>();
            foreach (DataRow dr in dt.Rows)
            {
                Appointment appointment = new Appointment()
                {
                    appointmentID = Convert.ToInt32(dr[0]),
                    patientID = Convert.ToInt32(dr[1]),
                    practitionerID = Convert.ToInt32(dr[2]),
                    appntType = Convert.ToInt32(dr[3]),
                    appntDate = (DateTime)dr[4],
                    startTime = (TimeSpan)dr[5],
                    endTime = (TimeSpan)dr[6],
                    notes = dr[7].ToString(),
                    status = dr[8].ToString()
                };
                allAppointments.Add(appointment);
            }

            return allAppointments;
        }

        public static ObservableCollection<AppointmentType> GetAppointmentTypes()
        {

            string strQuery = "SELECT appTypeID, appTypeName from AppointmentType";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var allAppTypes = new ObservableCollection<AppointmentType>();
            foreach (DataRow dr in dt.Rows)
            {
                AppointmentType appTypes = new AppointmentType()
                {
                    typeID = Convert.ToInt32(dr[0]),              
                    typeName = dr[1].ToString()
                };
                allAppTypes.Add(appTypes);
            }

            return allAppTypes;
        }

        public static int insertAppointment(Appointment appointment)
        {
            int rowsAffected;

            string strQuery = "INSERT INTO Appointment (patientID, appTypeID, appDate, startTime, endTime, notes, status) " +
                                "VALUES (@patientID, @appntType, @appntDate, @startTime, @endTime, @notes, @status)";

            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[7];
            objParams[0] = new SqlParameter("@patientID", DbType.Int32);
            objParams[0].Value = appointment.patientID;
            objParams[1] = new SqlParameter("@appntType", DbType.Int32);
            objParams[1].Value = appointment.appntType;
            objParams[2] = new SqlParameter("@appntDate", DbType.DateTime);
            objParams[2].Value = appointment.appntDate;
            objParams[3] = new SqlParameter("@startTime", DbType.Time);
            objParams[3].Value = appointment.startTime;
            objParams[4] = new SqlParameter("@endTime", DbType.Time);
            objParams[4].Value = appointment.endTime;
            objParams[5] = new SqlParameter("@notes", DbType.String);
            objParams[5].Value = appointment.notes;
            objParams[6] = new SqlParameter("@status", DbType.String);
            objParams[6].Value = "Pending";


            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;
        }

        public static int updateAppointment(Appointment appointment)
        {
            int rowsAffected;

            string strQuery = "UPDATE Appointment SET patientID = @patientID, practitionerID = @practitionerID, appntType = @appntType, appntDate = @appntDate, startTime = @startTime, " +
                                "endTime = @endTime, notes = @notes, status = @status WHERE appointmentID = @appointmentID";
            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[8];
            objParams[0] = new SqlParameter("@patientID", DbType.Int32);
            objParams[0].Value = appointment.patientID;
            objParams[1] = new SqlParameter("@practitionerID", DbType.Int32);
            objParams[1].Value = appointment.practitionerID;
            objParams[2] = new SqlParameter("@appntType", DbType.Int32);
            objParams[2].Value = appointment.appntType;
            objParams[3] = new SqlParameter("@appntDate", DbType.DateTime);
            objParams[3].Value = appointment.appntDate;
            objParams[4] = new SqlParameter("@startTime", DbType.String);
            objParams[4].Value = appointment.startTime;
            objParams[5] = new SqlParameter("@endTime", DbType.Time);
            objParams[5].Value = appointment.endTime;
            objParams[6] = new SqlParameter("@notes", DbType.Time);
            objParams[6].Value = appointment.notes;
            objParams[6] = new SqlParameter("@status", DbType.Time);
            objParams[6].Value = appointment.status;
            objParams[6] = new SqlParameter("@appointmentID", DbType.Int32);
            objParams[6].Value = appointment.appointmentID;


            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;
        }


    }
}
