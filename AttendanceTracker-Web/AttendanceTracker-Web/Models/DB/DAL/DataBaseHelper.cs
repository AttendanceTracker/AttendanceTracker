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

        public override Device AddDevice(Device device)
        {
            var queryString = string.Format("exec Devices_AddDevice {0}, {1};", device.DeviceID, device.StudentID);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();

            var resultDevice = GetDevice(device.DeviceID);
            return resultDevice;
        }

        public override Device UpdateDevice(Device device)
        {
            var queryString = string.Format("exec Devices_UpdateDevice {0}, {1}", device.DeviceID, device.StudentID);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();

            var resultDevice = GetDevice(device.DeviceID);
            return resultDevice;
        }

        public override Device GetDevice(long imei)
        {
            var results = GetDeviceData(imei);
            if (results.Rows.Count != 0)
            {
                var data = results.Rows[0];
                var imeiResult = (long)data[0];
                var resultStudentID = (long)data[1];
                var device = dbDTOFactory.Device(imeiResult, resultStudentID);
                return device;
            }
            return null;
        }

        private DataTable GetDeviceData(long imei)
        {
            var queryString = string.Format("exec Devices_GetDevice {0};", imei);
            var query = new Query(queryString, connectionString);
            var results = query.ExecuteQuery();
            return results;
        }

        public override void RemoveDevice(long imei)
        {
            var queryString = string.Format("exec Devices_RemoveDevice {0}", imei);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();
        }

        public override Student AddStudent(Student student)
        {
            var queryString = string.Format("exec Students_AddStudent {0}, '{1}', '{2}', '{3}';", student.CWID, student.FirstName, student.LastName, student.Email);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();

            var resultStudent = GetStudent(student.CWID);
            return resultStudent;
        }

        public override Student UpdateStudent(Student student)
        {
            var queryString = string.Format("exec Students_UpdateStudent {0}, '{1}', '{2}', '{3}';", student.CWID, student.FirstName, student.LastName, student.Email);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();

            var resultStudent = GetStudent(student.CWID);
            return resultStudent;
        }

        public override Student GetStudent(long cwid)
        {
            var results = GetStudentData(cwid);
            if (results.Rows.Count != 0)
            {
                var studentData = results.Rows[0];
                var cwidResult = (long)studentData[0];
                var firstName = (string)studentData[1];
                var lastName = (string)studentData[2];
                var email = (string)studentData[3];
                var resultStudent = dbDTOFactory.Student(cwidResult, firstName, lastName, email);
                return resultStudent;
            }
            return null;
        }

        private DataTable GetStudentData(long cwid)
        {
            var queryString = string.Format("exec Students_GetStudent {0};", cwid);
            var query = new Query(queryString, connectionString);
            var results = query.ExecuteQuery();
            return results;
        }

        public override void RemoveStudent(long cwid)
        {
            var queryString = string.Format("exec Students_RemoveStudent {0};", cwid);
            var query = new Query(queryString, connectionString);
            query.ExecuteQuery();
        }
    }
}