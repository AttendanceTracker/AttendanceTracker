using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace AttendanceTracker_Web.Models.DB
{
    public class DataBaseHelper : DataSource
    {
        string connectionString;
        DataBaseFactory dbDTOFactory;

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
            dbDTOFactory = new DataBaseFactory();
        }

        public override bool DoesDeviceExist(long imei)
        {
            var results = GetDevice(imei);
            return results.Rows.Count > 0;
        }

        public override Device AddDevice(Device device)
        {
            var queryString = string.Format("exec Devices_AddDevice {0}, {1};", device.DeviceID, device.StudentID);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();

            var results = GetDevice(device.DeviceID);
            var deviceData = results.Rows[0];
            var resultIMEI = (long)deviceData[0];
            var resultStudentID = (long)deviceData[1];
            var resultDevice = dbDTOFactory.Device(resultIMEI, resultStudentID);
            return resultDevice;
        }

        private DataTable GetDevice(long imei)
        {
            var queryString = string.Format("exec Devices_GetDevice {0};", imei);
            var query = new Query(queryString, connectionString);
            var results = query.ExecuteQuery();
            return results;
        }

        public override Student AddStudent(Student student)
        {
            var queryString = string.Format("exec Students_AddStudent {0}, '{1}', '{2}', '{3}';", student.CWID, student.FirstName, student.LastName, student.Email);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();

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
            var queryString = string.Format("exec Students_GetStudent {0};", cwid);
            var query = new Query(queryString, connectionString);
            var results = query.ExecuteQuery();
            return results;
        }
    }
}