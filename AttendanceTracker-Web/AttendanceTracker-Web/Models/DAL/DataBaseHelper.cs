using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using AttendanceTracker_Web.Models.DTOs.DB;
using AttendanceTracker_Web.Models.Factories;

namespace AttendanceTracker_Web.Models.DAL
{
    public class DataBaseHelper : DataSource
    {
        string connectionString;
        DataBaseDTOFactory dbDTOFactory;

        public DataBaseHelper()
        {
            Init("Default");
        }

        public DataBaseHelper(string connectionSource)
        {
            Init(connectionSource);
        }

        private void Init(string connectionSource)
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionSource].ConnectionString;
            dbDTOFactory = new DataBaseDTOFactory();
        }

        public override bool DoesDeviceExist(long imei)
        {
            var results = GetDevice(imei);
            return results.Rows.Count > 0;
        }

        public override Device AddDevice(Device device)
        {
            var query = string.Format("exec Devices_AddDevice {0}, {1};", device.DeviceID, device.StudentID);
            ExecuteQuery(query);

            var results = GetDevice(device.DeviceID);
            var deviceData = results.Rows[0];
            var resultIMEI = (long)deviceData[0];
            var resultStudentID = (long)deviceData[1];
            var resultDevice = dbDTOFactory.Device(resultIMEI, resultStudentID);
            return resultDevice;
        }

        private DataTable GetDevice(long imei)
        {
            var query = string.Format("exec Devices_GetDevice {0};", imei);
            var results = ExecuteQuery(query);
            return results;
        }

        public override Student AddStudent(Student student)
        {
            var query = string.Format("exec Students_AddStudent {0}, '{1}', '{2}', '{3}';", student.CWID, student.FirstName, student.LastName, student.Email);
            ExecuteQuery(query);

            var results = GetStudent(student.CWID);
            var studentData = results.Rows[0];
            var cwid = (long)studentData[0];
            var firstName = (string)studentData[1];
            var lastName = (string)studentData[2];
            var email = (string)studentData[3];
            var resultStudent = dbDTOFactory.Student(cwid, firstName, lastName, email);
            return resultStudent;
        }
        private DataTable GetStudent(long cwid)
        {
            var query = string.Format("exec Students_GetStudent {0};", cwid);
            var results = ExecuteQuery(query);
            return results;
        }

        private DataTable ExecuteQuery(string query)
        {
            var emptyDictionary = new Dictionary<string, object>();
            return ExecuteQuery(query, emptyDictionary);
        }

        private DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            var resultTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
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