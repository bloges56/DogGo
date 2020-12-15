using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DogGo.Repository.Utils
{
    public class ReaderUtils
    {
        public static string GetNullableString(SqlDataReader reader, string columnName)
        {
            if(!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return reader.GetString(reader.GetOrdinal(columnName));
            }

            return null;
        }
        public static Object GetNullableObject(string value)
        {
            if(value != null)
            {
                return value;
            }
            else
            {
                return DBNull.Value;
            }
        }
    }
}
