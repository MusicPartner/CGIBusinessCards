// ****************************************************
// Made by CGI, Copyright CGI
// 
// Version:   1.01.001 ()
// Date:      22-11-22
// 
// Module:        Program (Main)
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

using CGI.BusinessCards.Web.Api.Infrastructure.Configuration;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System;
using System.Collections.Generic;
using System.Data;

namespace CGI.BusinessCards.Web.Api.Infrastructure.Common
{
    public class BaseDataAccess
    {
        private readonly ILogger _logger;

        protected string ConnectionString { get; set; }

        public BaseDataAccess(IConfiguration configuration, ILogger<BaseDataAccess> logger)
        {
            _logger = logger;

            ConnectionString = configuration.GetConnectionString(ApplicationSettingsGlobal._appDatabaseConnectionString);
        }

        public BaseDataAccess(string connectionString)
        {
            //Initialize a Null Logger (No Logging)
            _logger = new NullLogger<BaseDataAccess>();

            this.ConnectionString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(this.ConnectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }

        public SqlCommand GetCommand(SqlConnection connection, string commandText, CommandType commandType)
        {
#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection)
            {
                CommandType = commandType
            };
#pragma warning restore CA2100 // Review SQL queries for security vulnerabilities

            return command;
        }

        public SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value);
            parameterObject.Direction = ParameterDirection.Input;

            return parameterObject;
        }

        public SqlParameter GetParameterIn(string parameter, SqlDbType type, object value = null)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = ParameterDirection.Input;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }

        public SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type); ;

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }

        public int ExecuteNonQuery(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Failed to ExecuteNonQuery for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        public int ExecuteNonQuery(string sql)
        {
            int returnValue = -1;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    SqlCommand cmd = this.GetCommand((SqlConnection)connection, sql, CommandType.Text);

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Failed to ExecuteNonQuery for sql string: " + sql, ex);
                throw;
            }

            return returnValue;
        }

        public object ExecuteScalar(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            object returnValue = null;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Failed to ExecuteScalar for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }

        public SqlDataReader GetDataReader(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            SqlDataReader ds;

            try
            {
                SqlConnection connection = this.GetConnection();
                {
                    SqlCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Failed to GetDataReader for " + procedureName, ex, parameters);
                throw;
            }

            return ds;
        }

        public DataSet GetDataSet(string procedureName, List<SqlParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    SqlCommand sqlComm = this.GetCommand(connection, procedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        sqlComm.Parameters.AddRange(parameters.ToArray());
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Failed to GetDataSet for " + procedureName, ex, parameters);
                throw;
            }

            return ds;
        }
    }
}
