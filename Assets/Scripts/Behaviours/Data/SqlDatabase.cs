using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface;

namespace Assets.Scripts.Behaviours.Data
{
    public sealed class SqlDatabase : IDatabase
    {
        #region Fields
        private readonly IDbConnection _connection;
        private readonly bool _takeOwnershipOfConnection;

        private bool _disposed;
        #endregion

        #region Constructors
        private SqlDatabase(IDbConnection connection, bool takeOwnershipOfConnection)
        {
            _takeOwnershipOfConnection = takeOwnershipOfConnection;
            _connection = connection;
        }

        ~SqlDatabase()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        public static IDatabase Create(IDbConnection connection, bool takeOwnershipOfConnection)
        {
            return new SqlDatabase(connection, takeOwnershipOfConnection);
        }

        public int Execute(string commandText)
        {
            return Execute(commandText, new Dictionary<string, object>());
        }

        public int Execute(string commandText, string parameterName, object parameterValue)
        {
            return Execute(
                commandText,
                new Dictionary<string, object>()
                {
                    { parameterName, parameterValue },
                });
        }

        public int Execute(string commandText, IDictionary<string, object> namedParameters)
        {
            using (var command = CreateCommand(commandText, namedParameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        public IDataReader Query(string queryText)
        {
            return Query(queryText, new Dictionary<string, object>());
        }

        public IDataReader Query(string queryText, string parameterName, object parameterValue)
        {
            return Query(
                queryText,
                new Dictionary<string, object>()
                {
                    { parameterName, parameterValue },
                });
        }

        public IDataReader Query(string queryText, IDictionary<string, object> namedParameters)
        {
            using (var command = CreateCommand(queryText, namedParameters))
            {
                return command.ExecuteReader();
            }
        }

        public IDbCommand CreateCommand(string commandText)
        {
            return CreateCommand(commandText, new Dictionary<string, object>());
        }

        public IDbCommand CreateCommand(string commandText, string parameterName, object parameterValue)
        {
            return CreateCommand(
                commandText,
                new Dictionary<string, object>()
                {
                    { parameterName, parameterValue },
                });
        }

        public IDbCommand CreateCommand(string commandText, IDictionary<string, object> namedParameters)
        {
            var command = _connection.CreateCommand();
            command.CommandText = commandText;

            foreach (var kvp in namedParameters)
            {
                var parameter = CreateParameter(
                    command,
                    kvp.Key,
                    kvp.Value);
                command.Parameters.Add(parameter);
            }

            return command;
        }

        public IDbDataParameter CreateParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;

            return parameter;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            try
            {
                if (disposing)
                {
                    if (_takeOwnershipOfConnection)
                    {
                        Close();
                        _connection.Dispose();
                    }
                }
            }
            finally
            {
                _disposed = true;
            }
        }
        #endregion
    }
}