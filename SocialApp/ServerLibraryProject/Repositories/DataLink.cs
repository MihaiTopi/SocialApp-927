namespace ServerLibraryProject.Repositories
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using ServerLibraryProject.Interfaces;

    public sealed partial class DataLink : IDisposable, IDataLink
    {
        [Obsolete]
        private static readonly Lazy<DataLink> InstanceValue = new(() => new DataLink());
        private readonly string connectionString;
        [Obsolete]
        private SqlConnection? sqlConnection;
        private bool disposedValue;
        private string loginString = "Server=DESKTOP-S99JALT;Database=SocialApp;Trusted_Connection=True;TrustServerCertificate=True;";

        [Obsolete]
        public DataLink()
        {

            connectionString = loginString;

            using var connection = new SqlConnection(connectionString);
            connection.Open();

        }

        [Obsolete]
        public static DataLink Instance => InstanceValue.Value;

        [Obsolete]
        public T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteScalar operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteScalar operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public int ExecuteQuery(string query, SqlParameter[]? sqlParameters = null, bool isStoredProcedure = true)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();
                using var command = new SqlCommand(query, connection)
                {
                    CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public T? ExecuteBool<T>(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();
                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteBool operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteBool operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public DataTable ExecuteReader(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteReader operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteReader operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteNonQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public async Task<int> ExecuteNonQueryAsync(string storedProcedure, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                await connection.OpenAsync();

                using var command = new SqlCommand(storedProcedure, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                return await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteNonQueryAsync operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Obsolete]
        public DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters = null)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                using var command = new SqlCommand(query, connection)
                {
                    CommandType = CommandType.Text,  // Always a raw SQL query
                };

                if (sqlParameters != null)
                {
                    command.Parameters.AddRange(sqlParameters);
                }

                using var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during ExecuteSqlQuery operation: {ex.Message}", ex);
            }
        }

        [Obsolete]
        private SqlConnection GetConnection()
        {
            ObjectDisposedException.ThrowIf(disposedValue, new ObjectDisposedException("DataLink"));

            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection = new SqlConnection(connectionString);
            }

            return sqlConnection;
        }

        [Obsolete]
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (sqlConnection != null)
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }

                        sqlConnection.Dispose();
                        sqlConnection = null;
                    }
                }

                disposedValue = true;
            }
        }
    }
}