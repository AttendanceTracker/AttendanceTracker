using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace AttendanceTracker_Web.Models.DB
{
    public class Query
    {
        private string connectionString;
        private string queryString;
        private List<SqlParameter> parameters;

        public Query(string queryString, string connectionString)
        {
            this.connectionString = connectionString;
            this.queryString = queryString;
            parameters = new List<SqlParameter>();
        }

        public void AddParameter(string parameterName, object value)
        {
            var parameter = new SqlParameter(parameterName, value);
            parameters.Add(parameter);
        }

        public DataTable Execute()
        {
            var resultTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(queryString, connection))
            {
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param);
                }

                connection.Open();
                var reader = command.ExecuteReader();
                resultTable.Load(reader);
                reader.Close();
            }
            return resultTable;
        }

        public object ExecuteScalar()
        {
            object result = null;
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(queryString, connection))
            {
                foreach (var param in parameters)
                {
                    command.Parameters.Add(param);
                }

                connection.Open();
                result = command.ExecuteScalar();
            }
            return result;
        }
    }
}