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
    class PractitionerDB
    {
        private static SQLHelper _DB = new SQLHelper("wsmp_constring");

        public static ObservableCollection<Practitioner> GetAllPractitioners()
        {
            string strQuery = "SELECT practitionerID, firstname, lastname, birthdate, street, suburb, state, postcode, mobilenum, homenum, email, medRegNum, pracType, username, password from practitioner";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var practitioners = new ObservableCollection<Practitioner>();
            foreach (DataRow dr in dt.Rows)
            {
                Practitioner practitioner = new Practitioner()
                {
                    practitionerID = Convert.ToInt32(dr[0]),
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
                    medregNum = dr[11].ToString(),
                    practType = Convert.ToInt32(dr[12]),
                    username = dr[13].ToString(),
                    password = dr[14].ToString()
                };
                practitioners.Add(practitioner);
            }

            return practitioners;
        }

        public static int insertPractitioner(Practitioner practitioner)
        {
            int rowsAffected;

            string strQuery = "INSERT INTO Practitioner (firstname, lastname, birthdate, street, suburb, state, postcode, mobilenum, homenum, email, medRegNum, pracType, username, password) " +
                                "VALUES (@firstname, @lastname, @birthday, @street, @suburb, @state, @postcode, @mobilenum, @homenum, @email, @medRegNum, @pracType, @username, @password); SELECT SCOPE_IDENTITY()";

            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[14];
            objParams[0] = new SqlParameter("@firstname", DbType.String);
            objParams[0].Value = practitioner.firstName;
            objParams[1] = new SqlParameter("@lastname", DbType.String);
            objParams[1].Value = practitioner.lastName;
            objParams[2] = new SqlParameter("@birthday", DbType.DateTime);
            objParams[2].Value = practitioner.DOB;
            objParams[3] = new SqlParameter("@street", DbType.String);
            objParams[3].Value = practitioner.street;
            objParams[4] = new SqlParameter("@suburb", DbType.String);
            objParams[4].Value = practitioner.suburb;
            objParams[5] = new SqlParameter("@state", DbType.String);
            objParams[5].Value = practitioner.state;
            objParams[6] = new SqlParameter("@postcode", DbType.String);
            objParams[6].Value = practitioner.postCode;
            objParams[7] = new SqlParameter("@mobilenum", DbType.String);
            objParams[7].Value = practitioner.mobileNum;
            objParams[8] = new SqlParameter("@homenum", DbType.String);
            objParams[8].Value = practitioner.homeNum;
            objParams[9] = new SqlParameter("@email", DbType.String);
            objParams[9].Value = practitioner.email;
            objParams[10] = new SqlParameter("@medRegNum", DbType.String);
            objParams[10].Value = practitioner.medregNum;
            objParams[11] = new SqlParameter("@pracType", DbType.Int32);
            objParams[11].Value = practitioner.practType;
            objParams[12] = new SqlParameter("@username", DbType.String);
            objParams[12].Value = practitioner.username;
            objParams[13] = new SqlParameter("@password", DbType.String);
            objParams[13].Value = practitioner.password;

            //rowsAffected = _DB.NonQuerySql(strQuery, objParams);
            object practId = _DB.scalarSQL(strQuery, objParams);

            practitioner.practitionerID = Convert.ToInt32(practId);

            return practitioner.practitionerID;
        }

        public static int updatePractitioner(Practitioner practitioner)
        {
            int rowsAffected;

            string strQuery = "UPDATE Practitioner SET firstname = @firstname, lastname = @lastname, birthdate = @birthday, street = @street, suburb = @suburb, " +
                                "state = @state, postcode = @postcode, mobilenum = @mobilenum, homenum = @homenum, email = @email, medRegNum = @medRegNum, pracType = @pracType, " +
                                "username = @username, password = @password WHERE practitionerID = @practitionerID";

            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[15];
            objParams[0] = new SqlParameter("@firstname", DbType.String);
            objParams[0].Value = practitioner.firstName;
            objParams[1] = new SqlParameter("@lastname", DbType.String);
            objParams[1].Value = practitioner.lastName;
            objParams[2] = new SqlParameter("@birthday", DbType.DateTime);
            objParams[2].Value = practitioner.DOB;
            objParams[3] = new SqlParameter("@street", DbType.String);
            objParams[3].Value = practitioner.street;
            objParams[4] = new SqlParameter("@suburb", DbType.String);
            objParams[4].Value = practitioner.suburb;
            objParams[5] = new SqlParameter("@state", DbType.String);
            objParams[5].Value = practitioner.state;
            objParams[6] = new SqlParameter("@postcode", DbType.String);
            objParams[6].Value = practitioner.postCode;
            objParams[7] = new SqlParameter("@mobilenum", DbType.String);
            objParams[7].Value = practitioner.mobileNum;
            objParams[8] = new SqlParameter("@homenum", DbType.String);
            objParams[8].Value = practitioner.homeNum;
            objParams[9] = new SqlParameter("@email", DbType.String);
            objParams[9].Value = practitioner.email;
            objParams[10] = new SqlParameter("@medRegNum", DbType.String);
            objParams[10].Value = practitioner.medregNum;
            objParams[11] = new SqlParameter("@pracType", DbType.Int32);
            objParams[11].Value = practitioner.practType;
            objParams[12] = new SqlParameter("@username", DbType.String);
            objParams[12].Value = practitioner.username;
            objParams[13] = new SqlParameter("@password", DbType.String);
            objParams[13].Value = practitioner.password;
            objParams[14] = new SqlParameter("@practitionerID", DbType.Int32);
            objParams[14].Value = practitioner.practitionerID;

            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;
        }

        public static ObservableCollection<Practitioner> SearchPractitioner(string firstname)
        {
            //get all the details of the searched name of the client
            string strQuery = "SELECT * FROM practitioner WHERE FirstName LIKE '%" + firstname + "%';";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var practitioners = new ObservableCollection<Practitioner>();
            foreach (DataRow dr in dt.Rows)
            {
                Practitioner practitioner = new Practitioner()
                {
                    practitionerID = Convert.ToInt32(dr[0]),
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
                    medregNum = dr[11].ToString(),
                    practType = Convert.ToInt32(dr[12]),
                    username = dr[13].ToString(),
                    password = dr[14].ToString()
                };
                practitioners.Add(practitioner);
            }

            return practitioners;
        }
    }
}
