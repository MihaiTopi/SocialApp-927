namespace ServerLibraryProject.Interfaces
{
    using System.Data;
    using Microsoft.Data.SqlClient;

    public interface IDataLink
    {
        [Obsolete]
        T? ExecuteScalar<T>(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);

        [Obsolete]
        int ExecuteQuery(string query, SqlParameter[]? sqlParameters, bool isStoredProcedure);

        [Obsolete]
        int ExecuteNonQuery(string storedProcedure, SqlParameter[]? sqlParameters);

        [Obsolete]
        DataTable ExecuteSqlQuery(string query, SqlParameter[]? sqlParameters);

        [Obsolete]
        DataTable ExecuteReader(string storedProcedure, SqlParameter[]? sqlParameters = null);
    }
}