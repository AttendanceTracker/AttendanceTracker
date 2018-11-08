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

        public Query(string queryString, string connectionString)
        {
            this.connectionString = connectionString;
            this.queryString = queryString;
        }

        public DataTable ExecuteQuery()
        {
            var emptyDictionary = new Dictionary<string, object>();
            return ExecuteQuery(emptyDictionary);
        }

        public DataTable ExecuteQuery(Dictionary<string, object> parameters)
        {
            var resultTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(queryString, connection))
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                connection.Open();
                var reader = command.ExecuteReader();
                resultTable.Load(reader);
                reader.Close();
            }
            return resultTable;
        }
    }
}