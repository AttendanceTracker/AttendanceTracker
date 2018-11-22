using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AttendanceTracker_Web.Models.DB.Mapper
{
    public abstract class DBMappable
    {
        public abstract void MapProperties(DBPropertyMap map);
    }
}