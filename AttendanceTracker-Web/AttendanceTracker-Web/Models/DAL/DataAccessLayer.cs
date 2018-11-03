using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DAL
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

        public bool DoesDeviceExist(string imei)
        {
            return Source.DoesDeviceExist(imei);
        }

        public void AddDevice(string imei, long studentID)
        {
            Source.AddDevice(imei, studentID);
        }
    }
}