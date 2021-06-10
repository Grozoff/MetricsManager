﻿using System.Data.SQLite;

namespace MetricsManager.DAL
{
    public class SQLiteConnectionFactory
    {
        private readonly string _connectionString;
        public SQLiteConnectionFactory()
        {

        }
        public SQLiteConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SQLiteConnection Connect()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}
