using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OSDC.DotnetLibraries.General.DataManagement;
using Microsoft.Data.Sqlite;
using System.Text.Json;
using System.Reflection;

namespace NORCE.Drilling.GeodeticDatum.Service.Managers
{
    /// <summary>
    /// A manager for GeodeticDatum. The manager implements the singleton pattern as defined by 
    /// Gamma, Erich, et al. "Design patterns: Abstraction and reuse of object-oriented design." 
    /// European Conference on Object-Oriented Programming. Springer, Berlin, Heidelberg, 1993.
    /// </summary>
    public class GeodeticDatumManager
    {
        private static GeodeticDatumManager? _instance = null;
        private readonly ILogger<GeodeticDatumManager> _logger;
        private readonly object _lock = new();
        private readonly SqlConnectionManager _connectionManager;

        private GeodeticDatumManager(ILogger<GeodeticDatumManager> logger, SqlConnectionManager connectionManager)
        {
            _logger = logger;
            _connectionManager = connectionManager;

            // make sure database contains default GeodeticDatums
            List<Guid>? ids = GetAllGeodeticDatumId();
            if (ids == null || ids.Count == 0)
            {
                FillDefault();
            }
            else
            {
                foreach (Guid id in ids)
                {
                    try
                    {
                        Model.GeodeticDatum? datum = GetGeodeticDatumById(id);
                        if (datum == null)
                        {
                            _logger.LogWarning($"Geodetic datum with ID {id} is missing from the database");
                            _logger.LogWarning("Clearing GeodeticDatumTable due to error");
                            Clear();
                            _logger.LogWarning($"Filling default Geodetic datums");
                            FillDefault();
                            break; // no need to continue if we found a missing spheroid
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error while checking Geodetic datum with ID {id} in the database");
                        _logger.LogWarning("Clearing GeodeticDatumTable due to error");
                        Clear();
                        _logger.LogWarning($"Filling default Geodetic datum");
                        FillDefault();
                        break; // no need to continue if we found an error
                    }
                }
            }
        }

        public static GeodeticDatumManager GetInstance(ILogger<GeodeticDatumManager> logger, SqlConnectionManager connectionManager)
        {
            _instance ??= new GeodeticDatumManager(logger, connectionManager);
            return _instance;
        }

        public int Count
        {
            get
            {
                int count = 0;
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT COUNT(*) FROM GeodeticDatumTable";
                    try
                    {
                        using SqliteDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            count = (int)reader.GetInt64(0);
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to count records in the GeodeticDatumTable");
                    }
                }
                else
                {
                    _logger.LogWarning("Impossible to access the SQLite database");
                }
                return count;
            }
        }

        public bool Clear()
        {
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                bool success = false;
                lock (_lock)
                {
                    using var transaction = connection.BeginTransaction();
                    try
                    {
                        //empty GeodeticDatumTable
                        var command = connection.CreateCommand();
                        command.CommandText = "DELETE FROM GeodeticDatumTable";
                        command.ExecuteNonQuery();

                        transaction.Commit();
                        success = true;
                    }
                    catch (SqliteException ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Impossible to clear the GeodeticDatumTable");
                    }
                }
                return success;
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
                return false;
            }
        }

        public bool Contains(Guid guid)
        {
            int count = 0;
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT COUNT(*) FROM GeodeticDatumTable WHERE ID = '{guid}'";
                try
                {
                    using SqliteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        count = (int)reader.GetInt64(0);
                    }
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to count rows from GeodeticDatumTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return count >= 1;
        }

        /// <summary>
        /// Returns the list of Guid of all GeodeticDatum present in the microservice database 
        /// </summary>
        /// <returns>the list of Guid of all GeodeticDatum present in the microservice database</returns>
        public List<Guid>? GetAllGeodeticDatumId()
        {
            List<Guid> ids = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID FROM GeodeticDatumTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        Guid id = reader.GetGuid(0);
                        ids.Add(id);
                    }
                    _logger.LogInformation("Returning the list of ID of existing records from GeodeticDatumTable");
                    return ids;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get IDs from GeodeticDatumTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of MetaInfo of all GeodeticDatum present in the microservice database 
        /// </summary>
        /// <returns>the list of MetaInfo of all GeodeticDatum present in the microservice database</returns>
        public List<MetaInfo?>? GetAllGeodeticDatumMetaInfo()
        {
            List<MetaInfo?> metaInfos = new();
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT MetaInfo FROM GeodeticDatumTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        string mInfo = reader.GetString(0);
                        MetaInfo? metaInfo = JsonSerializer.Deserialize<MetaInfo>(mInfo, JsonSettings.Options);
                        metaInfos.Add(metaInfo);
                    }
                    _logger.LogInformation("Returning the list of MetaInfo of existing records from GeodeticDatumTable");
                    return metaInfos;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get IDs from GeodeticDatumTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the GeodeticDatum identified by its Guid from the microservice database 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the GeodeticDatum identified by its Guid from the microservice database</returns>
        public Model.GeodeticDatum? GetGeodeticDatumById(Guid guid)
        {
            if (!guid.Equals(Guid.Empty))
            {
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    Model.GeodeticDatum? geodeticDatum;
                    var command = connection.CreateCommand();
                    command.CommandText = $"SELECT GeodeticDatum FROM GeodeticDatumTable WHERE ID = '{guid}'";
                    try
                    {
                        using var reader = command.ExecuteReader();
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            string data = reader.GetString(0);
                            geodeticDatum = JsonSerializer.Deserialize<Model.GeodeticDatum>(data, JsonSettings.Options);
                            if (geodeticDatum != null && geodeticDatum.MetaInfo != null && !geodeticDatum.MetaInfo.ID.Equals(guid))
                                throw new SqliteException("SQLite database corrupted: returned GeodeticDatum is null or has been jsonified with the wrong ID.", 1);
                        }
                        else
                        {
                            _logger.LogInformation("No GeodeticDatum of given ID in the database");
                            return null;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to get the GeodeticDatum with the given ID from GeodeticDatumTable");
                        return null;
                    }
                    _logger.LogInformation("Returning the GeodeticDatum of given ID from GeodeticDatumTable");
                    return geodeticDatum;
                }
                else
                {
                    _logger.LogWarning("Impossible to access the SQLite database");
                }
            }
            else
            {
                _logger.LogWarning("The given GeodeticDatum ID is null or empty");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of all GeodeticDatum present in the microservice database 
        /// </summary>
        /// <returns>the list of all GeodeticDatum present in the microservice database</returns>
        public List<Model.GeodeticDatum?>? GetAllGeodeticDatum()
        {
            List<Model.GeodeticDatum?> vals = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT GeodeticDatum FROM GeodeticDatumTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        string data = reader.GetString(0);
                        Model.GeodeticDatum? geodeticDatum = JsonSerializer.Deserialize<Model.GeodeticDatum>(data, JsonSettings.Options);
                        vals.Add(geodeticDatum);
                    }
                    _logger.LogInformation("Returning the list of existing GeodeticDatum from GeodeticDatumTable");
                    return vals;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get GeodeticDatum from GeodeticDatumTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of all GeodeticDatumLight present in the microservice database 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the list of GeodeticDatumLight present in the microservice database</returns>
        public List<Model.GeodeticDatumLight>? GetAllGeodeticDatumLight()
        {
            List<Model.GeodeticDatumLight>? geodeticDatumLightList = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT MetaInfo, Name, Description, CreationDate, LastModificationDate, IsDefault FROM GeodeticDatumTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        string metaInfoStr = reader.GetString(0);
                        MetaInfo? metaInfo = JsonSerializer.Deserialize<MetaInfo>(metaInfoStr, JsonSettings.Options);
                        string name = reader.GetString(1);
                        string descr = reader.GetString(2);
                        // make sure DateTimeOffset are properly instantiated when stored values are null (and parsed as empty string)
                        DateTimeOffset? creationDate = null;
                        if (DateTimeOffset.TryParse(reader.GetString(3), out DateTimeOffset cDate))
                            creationDate = cDate;
                        DateTimeOffset? lastModificationDate = null;
                        if (DateTimeOffset.TryParse(reader.GetString(4), out DateTimeOffset lDate))
                            lastModificationDate = lDate;
                        bool isDefault = reader.GetBoolean(5);
                        geodeticDatumLightList.Add(new Model.GeodeticDatumLight(
                                metaInfo,
                                string.IsNullOrEmpty(name) ? null : name,
                                string.IsNullOrEmpty(descr) ? null : descr,
                                creationDate,
                                lastModificationDate,
                                isDefault));
                    }
                    _logger.LogInformation("Returning the list of existing GeodeticDatumLight from GeodeticDatumTable");
                    return geodeticDatumLightList;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get light datas from GeodeticDatumTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Performs calculation on the given GeodeticDatum and adds it to the microservice database
        /// </summary>
        /// <param name="geodeticDatum"></param>
        /// <returns>true if the given GeodeticDatum has been added successfully to the microservice database</returns>
        public bool AddGeodeticDatum(Model.GeodeticDatum? geodeticDatum)
        {
            if (geodeticDatum != null && geodeticDatum.MetaInfo != null && geodeticDatum.MetaInfo.ID != Guid.Empty)
            {
                //update GeodeticDatumTable
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    using SqliteTransaction transaction = connection.BeginTransaction();
                    bool success = true;
                    try
                    {
                        //add the GeodeticDatum to the GeodeticDatumTable
                        string metaInfo = JsonSerializer.Serialize(geodeticDatum.MetaInfo, JsonSettings.Options);
                        string? cDate = null;
                        if (geodeticDatum.CreationDate != null)
                            cDate = ((DateTimeOffset)geodeticDatum.CreationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                        string? lDate = null;
                        if (geodeticDatum.LastModificationDate != null)
                            lDate = ((DateTimeOffset)geodeticDatum.LastModificationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                        string data = JsonSerializer.Serialize(geodeticDatum, JsonSettings.Options);
                        var command = connection.CreateCommand();
                        command.CommandText = "INSERT INTO GeodeticDatumTable (" +
                            "ID, " +
                            "MetaInfo, " +
                            "Name, " +
                            "Description, " +
                            "CreationDate, " +
                            "LastModificationDate, " +
                            "IsDefault, " +
                            "GeodeticDatum" +
                            ") VALUES (" +
                            $"'{geodeticDatum.MetaInfo.ID}', " +
                            $"'{metaInfo}', " +
                            $"'{geodeticDatum.Name}', " +
                            $"'{geodeticDatum.Description}', " +
                            $"'{cDate}', " +
                            $"'{lDate}', " +
                            $"'{geodeticDatum.IsDefault}', " +
                            $"'{data}'" +
                            ")";
                        int count = command.ExecuteNonQuery();
                        if (count != 1)
                        {
                            _logger.LogWarning("Impossible to insert the given GeodeticDatum into the GeodeticDatumTable");
                            success = false;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to add the given GeodeticDatum into GeodeticDatumTable");
                        success = false;
                    }
                    //finalizing SQL transaction
                    if (success)
                    {
                        transaction.Commit();
                        _logger.LogInformation("Added the given GeodeticDatum of given ID into the GeodeticDatumTable successfully");
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                    return success;
                }
                else
                {
                    _logger.LogWarning("Impossible to access the SQLite database");
                }
            }
            else
            {
                _logger.LogWarning("The GeodeticDatum ID or the ID of its input are null or empty");
            }
            return false;
        }

        /// <summary>
        /// Performs calculation on the given GeodeticDatum and updates it in the microservice database
        /// </summary>
        /// <param name="geodeticDatum"></param>
        /// <returns>true if the given GeodeticDatum has been updated successfully</returns>
        public bool UpdateGeodeticDatumById(Guid guid, Model.GeodeticDatum? geodeticDatum)
        {
            bool success = true;
            if (guid != Guid.Empty && geodeticDatum != null && geodeticDatum.MetaInfo != null && geodeticDatum.MetaInfo.ID == guid)
            {
                //update GeodeticDatumTable
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    using SqliteTransaction transaction = connection.BeginTransaction();
                    //update fields in GeodeticDatumTable
                    try
                    {
                        string metaInfo = JsonSerializer.Serialize(geodeticDatum.MetaInfo, JsonSettings.Options);
                        string? cDate = null;
                        if (geodeticDatum.CreationDate != null)
                            cDate = ((DateTimeOffset)geodeticDatum.CreationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                        geodeticDatum.LastModificationDate = DateTimeOffset.UtcNow; 
                        string? lDate = ((DateTimeOffset)geodeticDatum.LastModificationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                        string data = JsonSerializer.Serialize(geodeticDatum, JsonSettings.Options);
                        var command = connection.CreateCommand();
                        command.CommandText = $"UPDATE GeodeticDatumTable SET " +
                            $"MetaInfo = '{metaInfo}', " +
                            $"Name = '{geodeticDatum.Name}', " +
                            $"Description = '{geodeticDatum.Description}', " +
                            $"CreationDate = '{cDate}', " +
                            $"LastModificationDate = '{lDate}', " +
                            $"IsDefault = '{geodeticDatum.IsDefault}', " +
                            $"GeodeticDatum = '{data}' " +
                            $"WHERE ID = '{guid}'";
                        int count = command.ExecuteNonQuery();
                        if (count != 1)
                        {
                            _logger.LogWarning("Impossible to update the GeodeticDatum");
                            success = false;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to update the GeodeticDatum");
                        success = false;
                    }

                    // Finalizing
                    if (success)
                    {
                        transaction.Commit();
                        _logger.LogInformation("Updated the given GeodeticDatum successfully");
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                else
                {
                    _logger.LogWarning("Impossible to access the SQLite database");
                }
            }
            else
            {
                _logger.LogWarning("The GeodeticDatum ID or the ID of some of its attributes are null or empty");
            }
            return false;
        }

        /// <summary>
        /// Deletes the GeodeticDatum of given ID from the microservice database
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>true if the GeodeticDatum was deleted from the microservice database</returns>
        public bool DeleteGeodeticDatumById(Guid guid)
        {
            if (!guid.Equals(Guid.Empty))
            {
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    using var transaction = connection.BeginTransaction();
                    bool success = true;
                    //delete GeodeticDatum from GeodeticDatumTable
                    try
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = $"DELETE FROM GeodeticDatumTable WHERE ID = '{guid}'";
                        int count = command.ExecuteNonQuery();
                        if (count < 0)
                        {
                            _logger.LogWarning("Impossible to delete the GeodeticDatum of given ID from the GeodeticDatumTable");
                            success = false;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to delete the GeodeticDatum of given ID from GeodeticDatumTable");
                        success = false;
                    }
                    if (success)
                    {
                        transaction.Commit();
                        _logger.LogInformation("Removed the GeodeticDatum of given ID from the GeodeticDatumTable successfully");
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                    return success;
                }
                else
                {
                    _logger.LogWarning("Impossible to access the SQLite database");
                }
            }
            else
            {
                _logger.LogWarning("The GeodeticDatum ID is null or empty");
            }
            return false;
        }

        /// <summary>
        /// populate database with default GeodeticDatum
        /// </summary>
        private void FillDefault()
        {
            List<Model.GeodeticDatum?> geodeticDatums = typeof(Model.GeodeticDatum)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(Model.GeodeticDatum))     // only real datum props
                .Select(p => (Model.GeodeticDatum?)p.GetValue(null))           // invoke each getter
                .Where(d => d is not null)                                     // drop any nulls
                .ToList();

            foreach (Model.GeodeticDatum? gdt in geodeticDatums)
            {
                AddGeodeticDatum(gdt);
            }
        }
    }
}