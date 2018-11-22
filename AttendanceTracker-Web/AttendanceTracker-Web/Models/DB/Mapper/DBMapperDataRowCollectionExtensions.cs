using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AttendanceTracker_Web.Models.DB.Mapper
{
    public static class DBMapperDataRowCollectionExtensions
    {
        public static List<T> ToDTOList<T>(this DataRowCollection rows) where T : DBMappable, new()
        {
            var dtos = new List<T>();
            foreach (DataRow row in rows)
            {
                var dto = row.ToDTO<T>();
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}