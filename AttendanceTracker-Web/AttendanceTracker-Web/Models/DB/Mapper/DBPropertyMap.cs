using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace AttendanceTracker_Web.Models.DB.Mapper
{
    public class DBPropertyMap
    {
        public Dictionary<string, object> Map {get; private set;}

        public DBPropertyMap()
        {
            Map = new Dictionary<string, object>();
        }

        public DBPropertyMap(DataRow row)
        {
            var columns = row.Table.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                Map.Add(column.ColumnName, row[i]);
            }
        }

        public bool ContainsKey(string key)
        {
            return Map.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            Map.Add(key, value);
        }

        public T Get<T>(string key)
        {
            var value = Map[key];
            return (T) value;
        }
    }
}