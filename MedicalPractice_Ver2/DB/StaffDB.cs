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
    class StaffDB
    {
        private static SQLHelper _DB = new SQLHelper("wsmp_constring");

        public static ObservableCollection<Staff> GetAllStaff()
        {
            string strQuery = "SELECT staffID, firstname, lastname, birthdate, street, suburb, state, postcode, mobilenum, homenum, email, username, password from staff";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var allstaff = new ObservableCollection<Staff>();
            foreach (DataRow dr in dt.Rows)
            {
                Staff staff = new Staff()
                {
                    staffID = Convert.ToInt32(dr[0]),
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
                    username = dr[11].ToString(),
                    password = dr[12].ToString()
                };
                allstaff.Add(staff);
            }

            return allstaff;
        }

        public static int insertStaff(Staff staff)
        {
            int rowsAffected;

            string strQuery = "INSERT INTO Staff (firstname, lastname, birthdate, street, suburb, state, postcode, mobilenum, homenum, email, username, password) " +
                                "VALUES (@firstname, @lastname, @birthday, @street, @suburb, @state, @postcode, @mobilenum, @homenum, @email, @username, @password)";

            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[12];
            objParams[0] = new SqlParameter("@firstname", DbType.String);
            objParams[0].Value = staff.firstName;
            objParams[1] = new SqlParameter("@lastname", DbType.String);
            objParams[1].Value = staff.lastName;
            objParams[2] = new SqlParameter("@birthday", DbType.DateTime);
            objParams[2].Value = staff.DOB;
            objParams[3] = new SqlParameter("@street", DbType.String);
            objParams[3].Value = staff.street;
            objParams[4] = new SqlParameter("@suburb", DbType.String);
            objParams[4].Value = staff.suburb;
            objParams[5] = new SqlParameter("@state", DbType.String);
            objParams[5].Value = staff.state;
            objParams[6] = new SqlParameter("@postcode", DbType.String);
            objParams[6].Value = staff.postCode;
            objParams[7] = new SqlParameter("@mobilenum", DbType.String);
            objParams[7].Value = staff.mobileNum;
            objParams[8] = new SqlParameter("@homenum", DbType.String);
            objParams[8].Value = staff.homeNum;
            objParams[9] = new SqlParameter("@email", DbType.String);
            objParams[9].Value = staff.email;
            objParams[10] = new SqlParameter("@username", DbType.String);
            objParams[10].Value = staff.username;
            objParams[11] = new SqlParameter("@password", DbType.String);
            objParams[11].Value = staff.password;


            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;
        }

        public static int deleteStaff(Staff staff)
        {
            int rowsAffected;

            string strQuery = "delete from staff where staffID = @staffID";

            //parameter
            SqlParameter[] objParams;
            objParams = new SqlParameter[1];
            objParams[0] = new SqlParameter("@staffID", DbType.Int32);
            objParams[0].Value = staff.staffID;

            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;
        }

        public static int updateStaff(Staff staff)
        {
            int rowsAffected;

            string strQuery = "UPDATE Staff SET firstname = @firstname, lastname = @lastname, birthdate = @birthday, street = @street, suburb = @suburb, " +
                               "state = @state, postcode = @postcode, mobilenum = @mobilenum, homenum = @homenum, email = @email, username = @username, password = @password " +
                               "WHERE staffID = @staffID";

            //parameters
            SqlParameter[] objParams;
            objParams = new SqlParameter[13];
            objParams[0] = new SqlParameter("@firstname", DbType.String);
            objParams[0].Value = staff.firstName;
            objParams[1] = new SqlParameter("@lastname", DbType.String);
            objParams[1].Value = staff.lastName;
            objParams[2] = new SqlParameter("@birthday", DbType.DateTime);
            objParams[2].Value = staff.DOB;
            objParams[3] = new SqlParameter("@street", DbType.String);
            objParams[3].Value = staff.street;
            objParams[4] = new SqlParameter("@suburb", DbType.String);
            objParams[4].Value = staff.suburb;
            objParams[5] = new SqlParameter("@state", DbType.String);
            objParams[5].Value = staff.state;
            objParams[6] = new SqlParameter("@postcode", DbType.String);
            objParams[6].Value = staff.postCode;
            objParams[7] = new SqlParameter("@mobilenum", DbType.String);
            objParams[7].Value = staff.mobileNum;
            objParams[8] = new SqlParameter("@homenum", DbType.String);
            objParams[8].Value = staff.homeNum;
            objParams[9] = new SqlParameter("@email", DbType.String);
            objParams[9].Value = staff.email;
            objParams[10] = new SqlParameter("@username", DbType.String);
            objParams[10].Value = staff.username;
            objParams[11] = new SqlParameter("@password", DbType.String);
            objParams[11].Value = staff.password;
            objParams[12] = new SqlParameter("@staffID", DbType.Int32);
            objParams[12].Value = staff.staffID;


            rowsAffected = _DB.NonQuerySql(strQuery, objParams);

            return rowsAffected;
        }

        public static ObservableCollection<Staff> SearchStaff(string firstname)
        {
            //get all the details of the searched name of the client
            string strQuery = "SELECT * FROM staff WHERE FirstName LIKE '%" + firstname + "%';";

            DataTable dt = new DataTable();
            dt = _DB.executeSQL(strQuery);

            var allstaff = new ObservableCollection<Staff>();
            foreach (DataRow dr in dt.Rows)
            {
                Staff staff = new Staff()
                {
                    staffID = Convert.ToInt32(dr[0]),
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
                    username = dr[11].ToString(),
                    password = dr[12].ToString()
                };
                allstaff.Add(staff);
            }

            return allstaff;
        }

    }
}
