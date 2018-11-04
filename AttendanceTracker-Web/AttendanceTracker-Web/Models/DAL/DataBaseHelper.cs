using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace AttendanceTracker_Web.Models.DAL
{
    public class DataBaseHelper : DataSource
    {
        string connectionString;

        public DataBaseHelper()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        }

        public DataBaseHelper(string connectionSource)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionSource].ConnectionString;
        }

        public override bool DoesDeviceExist(string imei)
        {
            var query = string.Format("exec Devices_GetDevice {0};", imei);
            var results = executeQuery(query);
            return results.Rows.Count > 0;
        }

        public override void AddDevice(string imei, long studentID)
        {
            throw new NotImplementedException();
        }

        private DataTable executeQuery(string query)
        {
            var emptyDictionary = new Dictionary<string, object>();
            return executeQuery(query, emptyDictionary);
        }

        private DataTable executeQuery(string query, Dictionary<string, object> parameters)
        {
            var resultTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                foreach(var param in parameters)
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