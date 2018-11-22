using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AttendanceTracker_Web.Models.DB.Mapper
{
    public static class DBMappableDataRowExtensions
    {
        public static T ToDTO<T>(this DataRow row) where T: DBMappable, new()
        {
            var propertyMap = new DBPropertyMap(row);
            var model = new T();
            model.MapProperties(propertyMap);
            return model;
        }
    }
}