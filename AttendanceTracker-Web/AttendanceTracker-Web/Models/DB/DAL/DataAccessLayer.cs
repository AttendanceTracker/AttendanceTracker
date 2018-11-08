using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB
{
    public enum DALDataSource { Test, DB, None };

    public class DataAccessLayer
    {
        public DataSource Source { get; }

        public DataAccessLayer(DALDataSource dataSource)
        {
            Source = GetDataSource(dataSource);
        }

        private DataSource GetDataSource(DALDataSource dataSource)
        {
            switch(dataSource)
            {
                case DALDataSource.Test:
                    return new TestDataHelper();
                case DALDataSource.DB:
                    return new DataBaseHelper();
            }
            return null;
        }

        public Device AddDevice(Device device)
        {
            return Source.AddDevice(device);
        }

        public Device UpdateDevice(Device device)
        {
            return Source.UpdateDevice(device);
        }

        public Device GetDevice(long imei)
        {
            return Source.GetDevice(imei);
        }

        public void RemoveDevice(long imei)
        {
            Source.RemoveDevice(imei);
        }

        public Student AddStudent(Student student)
        {
            return Source.AddStudent(student);
        }

        public Student UpdateStudent(Student student)
        {
            return Source.UpdateStudent(student);
        }

        public Student GetStudent(long cwid)
        {
            return Source.GetStudent(cwid);
        }

        public void RemoveStudent(long cwid)
        {
            Source.RemoveDevice(cwid);
        }
    }
}