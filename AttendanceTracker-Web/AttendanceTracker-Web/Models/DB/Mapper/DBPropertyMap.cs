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
            init();
        }

        public DBPropertyMap(DataRow row)
        {
            init();
            var columns = row.Table.Columns;
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                Add(column.ColumnName, row[i]);
            }
        }

        private void init()
        {
            Map = new Dictionary<string, object>();
        }

        public bool ContainsKey(string key)
        {
            return Map.ContainsKey(key.ToUpper());
        }

        public void Add(string key, object value)
        {
            Map.Add(key.ToUpper(), value);
        }

        public T Get<T>(string key)
        {
            var value = Map[key.ToUpper()];
            if (value != DBNull.Value)
            {
                return (T)value;
            }
            return default(T);
        }
    }
}