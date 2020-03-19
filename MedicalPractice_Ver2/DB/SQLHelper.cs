using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//added namespace
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MedicalPractice_Ver2.DB
{
    class SQLHelper
    {
        //private variable to hold the connection string settings
        private string _conn;

        public SQLHelper(string dbname)
        {
            //getting connection string from app.config
            _conn = ConfigurationManager.ConnectionStrings[dbname].ConnectionString;
        }

        public DataTable executeSQL(string sql)
        {
            //create the connection object and get the connection string from the private variable _conn
            SqlConnection objConnection = new SqlConnection(_conn);

            //create command object
            //get the SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //open database connection
            objConnection.Open();

            SqlDataReader objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable objDataTable = new DataTable();

            objDataTable.Load(objDataReader);

            return objDataTable;
        }

        public object scalarSQL(string sql)
        {
            //create the connection object and get the connection string from the private variable _conn
            SqlConnection objConnection = new SqlConnection(_conn);

            //create command object
            //get the SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //create a variable that will hold the return value
            object objRetValue;

            //open database connection
            objConnection.Open();

            //exeucte SQL
            objRetValue = objCommand.ExecuteScalar();

            //close connection
            objConnection.Close();

            //execute SQL and return value
            return objRetValue;
        }

        //EXECUTE NONQUERY
        public int NonQuerySql(string sql)
        {

            SqlConnection objConnection = new SqlConnection(_conn);


            SqlCommand objCommand = new SqlCommand(sql, objConnection);


            int intRetValue;

            //open database connection
            objConnection.Open();

            //execute SQL
            intRetValue = objCommand.ExecuteNonQuery();

            //close connection
            objConnection.Close();

            //execute SQL and return value
            return intRetValue;
        }


        //PARAMETER METHOD
        private void fillParameters(SqlCommand objCommand, SqlParameter[] parameters)
        {
            int i;

            //for each parameter add it to the command object parameters collection
            for (i = 0; i < parameters.Length; i++)
            {
                objCommand.Parameters.Add(parameters[i]);
            }
        }

        //executeSQL with parameter
        public DataTable executeSQL(string sql, SqlParameter[] parameters)
        {
            //create the connection object and get the connection string from the private variable _conn
            SqlConnection objConnection = new SqlConnection(_conn);

            //create command object
            //get the SQL to execute from the sql parameter passed as input to this function
            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //fill parameters - connectiong to the command object
            fillParameters(objCommand, parameters);

            //open database connection
            objConnection.Open();

            //execute sql and store in dataReader
            SqlDataReader objDataReader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable objDataTable = new DataTable();

            objDataTable.Load(objDataReader);

            return objDataTable;
        }

        //scalarSQL with parameters
        public object scalarSQL(string sql, SqlParameter[] parameters)
        {

            SqlConnection objConnection = new SqlConnection(_conn);


            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //fill parameters
            fillParameters(objCommand, parameters);

            object objRetValue;

            objConnection.Open();

            //exeucte SQL and close
            objRetValue = objCommand.ExecuteScalar();
            objConnection.Close();

            //execute SQL and return value
            return objRetValue;
        }

        //nonquery with parameters
        public int NonQuerySql(string sql, SqlParameter[] parameters)
        {

            SqlConnection objConnection = new SqlConnection(_conn);


            SqlCommand objCommand = new SqlCommand(sql, objConnection);

            //fill parameters
            fillParameters(objCommand, parameters);

            int intRetValue;

            //open database connection
            objConnection.Open();

            //execute SQL
            intRetValue = objCommand.ExecuteNonQuery();

            //close connection
            objConnection.Close();

            //execute SQL and return value
            return intRetValue;
        }    

        public int? getIntfromDB(object value)
        {
            if (value == DBNull.Value) return null;
            return Convert.ToInt32(value);
        }
    }
}
