// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        Extension (SqlDataReader)
// 
// Description:   
// 
// Changes:
// 1.01.001    First version
// 
// TODO:
// F1000:
// 
// ****************************************************

using Microsoft.Data.SqlClient;

using System;

namespace CGI.BusinessCards.Web.Api.Infrastructure.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static bool HasColumn(this SqlDataReader reader, string colName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(colName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static bool HasColumnAndValue(this SqlDataReader reader, string colName)
        {
            return reader.HasColumn(colName) && !reader.IsDBNull(reader.GetOrdinal(colName));
        }

        public static string GetString(this SqlDataReader reader, string colName)
        {
            return reader.GetString(reader.GetOrdinal(colName));
        }

        public static int GetInt32(this SqlDataReader reader, string colName)
        {
            return reader.GetInt32(reader.GetOrdinal(colName));
        }

        public static long GetInt64(this SqlDataReader reader, string colName)
        {
            return reader.GetInt64(reader.GetOrdinal(colName));
        }

        public static Guid GetGuid(this SqlDataReader reader, string colName)
        {
            return reader.GetGuid(reader.GetOrdinal(colName));
        }

        public static bool GetBoolean(this SqlDataReader reader, string colName)
        {
            return reader.GetBoolean(reader.GetOrdinal(colName));
        }

        public static DateTime GetDateTime(this SqlDataReader reader, string colName)
        {
            return reader.GetDateTime(reader.GetOrdinal(colName));
        }

        public static DateTimeOffset GetDateTimeOffset(this SqlDataReader reader, string colName)
        {
            return reader.GetDateTimeOffset(reader.GetOrdinal(colName));
        }

        public static double GetDouble(this SqlDataReader reader, string colName)
        {
            return reader.GetDouble(reader.GetOrdinal(colName));
        }

        public static string GetStringNullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetString(colName)
                : null;
        }

        public static int? GetInt32Nullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetInt32(colName)
                : new Nullable<int>();
        }

        public static long? GetInt64Nullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetInt64(colName)
                : new Nullable<long>();
        }

        public static Guid? GetGuidNullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetGuid(colName)
                : new Nullable<Guid>();
        }

        public static bool? GetBooleanNullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetBoolean(colName)
                : new Nullable<bool>();
        }

        public static DateTime? GetDateTimeNullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetDateTime(colName)
                : new Nullable<DateTime>();
        }

        public static DateTimeOffset? GetDateTimeOffsetNullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetDateTimeOffset(colName)
                : new Nullable<DateTimeOffset>();
        }

        public static double? GetDoubleNullable(this SqlDataReader reader, string colName)
        {
            return reader.HasColumnAndValue(colName)
                ? reader.GetDouble(colName)
                : new Nullable<double>();
        }

        public static string SafeGetString(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetString(reader, colIndex);
        }

        public static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);
            }
            else
            {
                return null;
            }
        }

        public static int? SafeGetInt32(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetInt32(reader, colIndex);
        }

        public static int? SafeGetInt32(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt32(colIndex);
            }
            else
            {
                return null;
            }
        }

        public static long? SafeGetInt64(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetInt64(reader, colIndex);
        }

        public static long? SafeGetInt64(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt64(colIndex);
            }
            else
            {
                return null;
            }
        }

        public static Guid SafeGetGuid(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetGuid(reader, colIndex);
        }

        public static Guid SafeGetGuid(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetGuid(colIndex);
            }
            else
            {
                return Guid.Empty;
            }
        }

        public static bool SafeGetBoolean(this SqlDataReader reader, string colName)
        {
            return SafeGetBoolean(reader, colName, false);
        }

        public static bool SafeGetBoolean(this SqlDataReader reader, int colIndex)
        {
            return SafeGetBoolean(reader, colIndex, false);
        }

        public static bool SafeGetBoolean(this SqlDataReader reader, string colName, bool defaultValue)
        {
            int colIndex = reader.GetOrdinal(colName);
            return reader.IsDBNull(colIndex) ? defaultValue : reader.GetBoolean(colIndex);
        }

        public static bool SafeGetBoolean(this SqlDataReader reader, int colIndex, bool defaultValue)
        {
            return reader.IsDBNull(colIndex) ? defaultValue : reader.GetBoolean(colIndex);
        }

        public static DateTime SafeGetDateTime(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetDateTime(reader, colIndex);
        }

        public static DateTime SafeGetDateTime(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDateTime(colIndex);
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static DateTimeOffset SafeGetDateTimeOffset(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetDateTimeOffset(reader, colIndex);
        }

        public static DateTimeOffset SafeGetDateTimeOffset(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDateTimeOffset(colIndex);
            }
            else
            {
                return DateTimeOffset.MinValue;
            }
        }

        public static double? SafeGetDouble(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetDouble(reader, colIndex);
        }

        public static double? SafeGetDouble(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDouble(colIndex);
            }
            else
            {
                return null;
            }
        }

        public static string SafeGetStringDefaultEmpty(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetStringDefaultEmpty(reader, colIndex);
        }

        public static string SafeGetStringDefaultEmpty(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);
            }
            else
            {
                return String.Empty;
            }
        }

        public static int SafeGetInt32DefaultZero(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetInt32DefaultZero(reader, colIndex);
        }

        public static int SafeGetInt32DefaultZero(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt32(colIndex);
            }
            else
            {
                return 0;
            }
        }

        public static long SafeGetInt64DefaultZero(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetInt64DefaultZero(reader, colIndex);
        }

        public static long SafeGetInt64DefaultZero(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetInt64(colIndex);
            }
            else
            {
                return 0;
            }
        }

        public static double SafeGetDoubleDefaultZero(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return SafeGetDoubleDefaultZero(reader, colIndex);
        }

        public static double SafeGetDoubleDefaultZero(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDouble(colIndex);
            }
            else
            {
                return 0;
            }
        }

        public static DateTime? SafeGetNullableDateTime(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return reader.GetValue(colIndex) as DateTime?;
        }

        public static DateTime? SafeGetNullableDateTime(this SqlDataReader reader, int colIndex)
        {
            return reader.GetValue(colIndex) as DateTime?;
        }

        // Generic
        public static T GetDef<T>(this SqlDataReader reader, string colName)
        {
            int colIndex = reader.GetOrdinal(colName);
            return GetDef<T>(reader, colIndex);
        }

        public static T GetDef<T>(this SqlDataReader reader, int colIndex)
        {
            var t = reader.GetSqlValue(colIndex);

#if DEBUG
            var tDebug = reader.GetValue(colIndex);
#endif

            if (t == DBNull.Value) return default(T);

            //Check if Boolean
            if (Type.GetTypeCode(reader.GetFieldType(colIndex)) == TypeCode.Byte && typeof(T) == typeof(bool))
            {
                return ((System.Data.SqlTypes.INullable)t).IsNull ? default(T) : (T)Convert.ChangeType(Convert.ToBoolean(reader.GetValue(colIndex)), typeof(bool));
            }

            //Check if Integer
            if (Type.GetTypeCode(reader.GetFieldType(colIndex)) == TypeCode.Byte && typeof(T) == typeof(int))
            {
                return ((System.Data.SqlTypes.INullable)t).IsNull ? default(T) : (T)Convert.ChangeType(Convert.ToInt32(reader.GetValue(colIndex)), typeof(int));
            }

            return ((System.Data.SqlTypes.INullable)t).IsNull ? default(T) : (T)reader.GetValue(colIndex);
        }

        public static T? GetVal<T>(this SqlDataReader reader, string colName) where T : struct
        {
            int colIndex = reader.GetOrdinal(colName);
            return GetVal<T>(reader, colIndex);
        }

        public static T? GetVal<T>(this SqlDataReader reader, int colIndex) where T : struct
        {
            var t = reader.GetSqlValue(colIndex);
            if (t == DBNull.Value) return null;

            //Check if Boolean
            if (Type.GetTypeCode(reader.GetFieldType(colIndex)) == TypeCode.Byte && typeof(T) == typeof(bool))
            {
                return ((System.Data.SqlTypes.INullable)t).IsNull ? (T?)null : (T)Convert.ChangeType(Convert.ToBoolean(reader.GetValue(colIndex)), typeof(bool));
            }

            //Check if Integer
            if (Type.GetTypeCode(reader.GetFieldType(colIndex)) == TypeCode.Byte && typeof(T) == typeof(int))
            {
                return ((System.Data.SqlTypes.INullable)t).IsNull ? (T?)null : (T)Convert.ChangeType(Convert.ToInt32(reader.GetValue(colIndex)), typeof(int));
            }

            return ((System.Data.SqlTypes.INullable)t).IsNull ? (T?)null : (T)reader.GetValue(colIndex);
        }

        public static T GetRef<T>(this SqlDataReader reader, string colName) where T : class
        {
            int colIndex = reader.GetOrdinal(colName);
            return GetRef<T>(reader, colIndex);
        }

        public static T GetRef<T>(this SqlDataReader reader, int colIndex) where T : class
        {
            var t = reader.GetSqlValue(colIndex);
            if (t == DBNull.Value) return null;

            //Check if Boolean
            if (Type.GetTypeCode(reader.GetFieldType(colIndex)) == TypeCode.Byte && typeof(T) == typeof(bool))
            {
                return ((System.Data.SqlTypes.INullable)t).IsNull ? null : (T)Convert.ChangeType(Convert.ToBoolean(reader.GetValue(colIndex)), typeof(bool));
            }

            //Check if Integer
            if (Type.GetTypeCode(reader.GetFieldType(colIndex)) == TypeCode.Byte && typeof(T) == typeof(int))
            {
                return ((System.Data.SqlTypes.INullable)t).IsNull ? null : (T)Convert.ChangeType(Convert.ToInt32(reader.GetValue(colIndex)), typeof(int));
            }

            return ((System.Data.SqlTypes.INullable)t).IsNull ? null : (T)reader.GetValue(colIndex);
        }
    }
}
