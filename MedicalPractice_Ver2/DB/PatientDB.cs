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
    class PatientDB
    {
        private static SQLHelper _DB = new SQLHelper("wsmp_constring");

        public static ObservableCollection<Patient> GetAllPatients()
        {
            string strQuery = "SELECT patientID, firstname, lastname, birthdate, street, suburb, state, postcode, mobilenum, homenum, email, medicarenum, notes from patient";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var patients = new ObservableCollection<Patient>();
            foreach(DataRow dr in dt.Rows)
            {
                Patient patient = new Patient()
                {
                    patientID = Convert.ToInt32(dr[0]),
                    firstName = dr[1].ToString(),
                    lastName = dr[2].ToString(),
                    DOB = (DateTime)dr[3],
                    street = dr[4].ToString(),
                    suburb = dr[5].ToString(),
                    state = dr[6].ToString(),
                    postCode = dr[7].ToString(),
                    mobileNum = dr[8].ToString(),
                    homeNum = dr[9].ToString(),
                    email = dr[10].ToString(),
                    medicareNum = dr[11].ToString(),
                    notes = dr[12].ToString()
                };
                patients.Add(patient);
            }

            return patients;
        }

        public static int insertPatient(Patient patient)
        {
            int rowsAffected;

            string strQuery = "INSERT INTO Patient (firstname, lastname, birthdate, street, suburb, state, postcode, mobilenum, homenum, email, medicarenum, notes) " +
                                "VALUES (@firstname, @lastname, @birthday, @street, @suburb, @state, @postcode, @mobilenum, @homenum, @email, @medicarenum, @notes)";

            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[12];
            objParams[0] = new SqlParameter("@firstname", DbType.String);
            objParams[0].Value = patient.firstName;
            objParams[1] = new SqlParameter("@lastname", DbType.String);
            objParams[1].Value = patient.lastName;
            objParams[2] = new SqlParameter("@birthday", DbType.DateTime);
            objParams[2].Value = patient.DOB;
            objParams[3] = new SqlParameter("@street", DbType.String);
            objParams[3].Value = patient.street;
            objParams[4] = new SqlParameter("@suburb", DbType.String);
            objParams[4].Value = patient.suburb;
            objParams[5] = new SqlParameter("@state", DbType.String);
            objParams[5].Value = patient.state;
            objParams[6] = new SqlParameter("@postcode", DbType.String);
            objParams[6].Value = patient.postCode;
            objParams[7] = new SqlParameter("@mobilenum", DbType.String);
            objParams[7].Value = patient.mobileNum;
            objParams[8] = new SqlParameter("@homenum", DbType.String);
            objParams[8].Value = patient.homeNum;
            objParams[9] = new SqlParameter("@email", DbType.String);
            objParams[9].Value = patient.email;
            objParams[10] = new SqlParameter("@medicarenum", DbType.String);
            objParams[10].Value = patient.medicareNum;
            objParams[11] = new SqlParameter("@notes", DbType.String);
            objParams[11].Value = patient.notes;


            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;

        }

        public static int updatePatient(Patient patient)
        {
            int rowsAffected;

            string strQuery = "UPDATE PATIENT SET firstname = @firstname, lastname = @lastname, birthdate = @birthday, street = @street, suburb = @suburb, " +
                                "state = @state, postcode = @postcode, mobilenum = @mobilenum, homenum = @homenum, email = @email, medicarenum = @medicarenum, notes = @notes " +
                                "WHERE patientID = @patientID";

            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[13];
            objParams[0] = new SqlParameter("@firstname", DbType.String);
            objParams[0].Value = patient.firstName;
            objParams[1] = new SqlParameter("@lastname", DbType.String);
            objParams[1].Value = patient.lastName;
            objParams[2] = new SqlParameter("@birthday", DbType.DateTime);
            objParams[2].Value = patient.DOB;
            objParams[3] = new SqlParameter("@street", DbType.String);
            objParams[3].Value = patient.street;
            objParams[4] = new SqlParameter("@suburb", DbType.String);
            objParams[4].Value = patient.suburb;
            objParams[5] = new SqlParameter("@state", DbType.String);
            objParams[5].Value = patient.state;
            objParams[6] = new SqlParameter("@postcode", DbType.String);
            objParams[6].Value = patient.postCode;
            objParams[7] = new SqlParameter("@mobilenum", DbType.String);
            objParams[7].Value = patient.mobileNum;
            objParams[8] = new SqlParameter("@homenum", DbType.String);
            objParams[8].Value = patient.homeNum;
            objParams[9] = new SqlParameter("@email", DbType.String);
            objParams[9].Value = patient.email;
            objParams[10] = new SqlParameter("@medicarenum", DbType.String);
            objParams[10].Value = patient.medicareNum;
            objParams[11] = new SqlParameter("@notes", DbType.String);
            objParams[11].Value = patient.notes;
            objParams[12] = new SqlParameter("@patientID", DbType.Int32);
            objParams[12].Value = patient.patientID;

            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;

        }

        public static int deletePatient(Patient patient)
        {
            int rowsAffected;

            string strQuery = "delete from patient where patientID = @patientID";

            //parameter
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@patientID", DbType.Int32);
            objParams[0].Value = patient.patientID;

            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;
        }


        public static ObservableCollection<Patient> SearchPatient(string firstname)
        {
            //get all the details of the searched name of the client
            string strQuery = "SELECT * FROM patient WHERE FirstName LIKE '%" + firstname + "%';";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var patients = new ObservableCollection<Patient>();
            foreach (DataRow dr in dt.Rows)
            {
                Patient patient = new Patient()
                {
                    patientID = Convert.ToInt32(dr[0]),
                    firstName = dr[1].ToString(),
                    lastName = dr[2].ToString(),
                    DOB = (DateTime)dr[3],
                    street = dr[4].ToString(),
                    suburb = dr[5].ToString(),
                    state = dr[6].ToString(),
                    postCode = dr[7].ToString(),
                    mobileNum = dr[8].ToString(),
                    homeNum = dr[9].ToString(),
                    email = dr[10].ToString(),
                    medicareNum = dr[11].ToString(),
                    notes = dr[12].ToString()
                };
                patients.Add(patient);
            }

            return patients;
        }
    }
}
