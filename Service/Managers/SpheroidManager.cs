using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OSDC.DotnetLibraries.General.DataManagement;
using Microsoft.Data.Sqlite;
using System.Text.Json;
using NORCE.Drilling.GeodeticDatum.Model;
using System.Reflection;

namespace NORCE.Drilling.GeodeticDatum.Service.Managers
{
    /// <summary>
    /// A manager for Spheroid. The manager implements the singleton pattern as defined by 
    /// Gamma, Erich, et al. "Design patterns: Abstraction and reuse of object-oriented design." 
    /// European Conference on Object-Oriented Programming. Springer, Berlin, Heidelberg, 1993.
    /// </summary>
    public class SpheroidManager
    {
        private static SpheroidManager? _instance = null;
        private readonly ILogger<SpheroidManager> _logger;
        private readonly object _lock = new();
        private readonly SqlConnectionManager _connectionManager;

        private SpheroidManager(ILogger<SpheroidManager> logger, SqlConnectionManager connectionManager)
        {
            _logger = logger;
            _connectionManager = connectionManager;

            // make sure database contains default Spheroids
            List<Guid>? ids = GetAllSpheroidId();
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
                        Model.Spheroid? spheroid = GetSpheroidById(id);
                        if (spheroid == null)
                        {
                            _logger.LogWarning($"Spheroid with ID {id} is missing from the database");
                            _logger.LogWarning("Clearing SpheroidTable due to error");
                            Clear();
                            _logger.LogWarning($"Filling default Spheroids");
                            FillDefault();
                            break; // no need to continue if we found a missing spheroid
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error while checking Spheroid with ID {id} in the database");
                        _logger.LogWarning("Clearing SpheroidTable due to error");
                        Clear();
                        _logger.LogWarning($"Filling default Spheroids");
                        FillDefault();
                        break; // no need to continue if we found an error
                    }
                }
            }
        }

        public static SpheroidManager GetInstance(ILogger<SpheroidManager> logger, SqlConnectionManager connectionManager)
        {
            _instance ??= new SpheroidManager(logger, connectionManager);
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
                    command.CommandText = "SELECT COUNT(*) FROM SpheroidTable";
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
                        _logger.LogError(ex, "Impossible to count records in the SpheroidTable");
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
                        //empty SpheroidTable
                        var command = connection.CreateCommand();
                        command.CommandText = "DELETE FROM SpheroidTable";
                        command.ExecuteNonQuery();

                        transaction.Commit();
                        success = true;
                    }
                    catch (SqliteException ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Impossible to clear the SpheroidTable");
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
                command.CommandText = $"SELECT COUNT(*) FROM SpheroidTable WHERE ID = '{guid}'";
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
                    _logger.LogError(ex, "Impossible to count rows from SpheroidTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return count >= 1;
        }

        /// <summary>
        /// Returns the list of Guid of all Spheroid present in the microservice database 
        /// </summary>
        /// <returns>the list of Guid of all Spheroid present in the microservice database</returns>
        public List<Guid>? GetAllSpheroidId()
        {
            List<Guid> ids = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID FROM SpheroidTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        Guid id = reader.GetGuid(0);
                        ids.Add(id);
                    }
                    _logger.LogInformation("Returning the list of ID of existing records from SpheroidTable");
                    return ids;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get IDs from SpheroidTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of MetaInfo of all Spheroid present in the microservice database 
        /// </summary>
        /// <returns>the list of MetaInfo of all Spheroid present in the microservice database</returns>
        public List<MetaInfo?>? GetAllSpheroidMetaInfo()
        {
            List<MetaInfo?> metaInfos = new();
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT MetaInfo FROM SpheroidTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        string mInfo = reader.GetString(0);
                        MetaInfo? metaInfo = JsonSerializer.Deserialize<MetaInfo>(mInfo, JsonSettings.Options);
                        metaInfos.Add(metaInfo);
                    }
                    _logger.LogInformation("Returning the list of MetaInfo of existing records from SpheroidTable");
                    return metaInfos;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get IDs from SpheroidTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the Spheroid identified by its Guid from the microservice database 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the Spheroid identified by its Guid from the microservice database</returns>
        public Model.Spheroid? GetSpheroidById(Guid guid)
        {
            if (!guid.Equals(Guid.Empty))
            {
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    Model.Spheroid? spheroid;
                    var command = connection.CreateCommand();
                    command.CommandText = $"SELECT Spheroid FROM SpheroidTable WHERE ID = '{guid}'";
                    try
                    {
                        using var reader = command.ExecuteReader();
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            string data = reader.GetString(0);
                            spheroid = JsonSerializer.Deserialize<Model.Spheroid>(data, JsonSettings.Options);
                            if (spheroid != null && spheroid.MetaInfo != null && !spheroid.MetaInfo.ID.Equals(guid))
                                throw new SqliteException("SQLite database corrupted: returned Spheroid is null or has been jsonified with the wrong ID.", 1);
                        }
                        else
                        {
                            _logger.LogInformation("No Spheroid of given ID in the database");
                            return null;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to get the Spheroid with the given ID from SpheroidTable");
                        return null;
                    }
                    _logger.LogInformation("Returning the Spheroid of given ID from SpheroidTable");
                    return spheroid;
                }
                else
                {
                    _logger.LogWarning("Impossible to access the SQLite database");
                }
            }
            else
            {
                _logger.LogWarning("The given Spheroid ID is null or empty");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of all Spheroid present in the microservice database 
        /// </summary>
        /// <returns>the list of all Spheroid present in the microservice database</returns>
        public List<Model.Spheroid?>? GetAllSpheroid()
        {
            List<Model.Spheroid?> vals = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Spheroid FROM SpheroidTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        string data = reader.GetString(0);
                        Model.Spheroid? spheroid = JsonSerializer.Deserialize<Model.Spheroid>(data, JsonSettings.Options);
                        vals.Add(spheroid);
                    }
                    _logger.LogInformation("Returning the list of existing Spheroid from SpheroidTable");
                    return vals;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get Spheroid from SpheroidTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Performs calculation on the given Spheroid and adds it to the microservice database
        /// </summary>
        /// <param name="spheroid"></param>
        /// <returns>true if the given Spheroid has been added successfully to the microservice database</returns>
        public bool AddSpheroid(Model.Spheroid? spheroid)
        {
            if (spheroid != null && spheroid.MetaInfo != null && spheroid.MetaInfo.ID != Guid.Empty)
            {
                //calculate outputs
                if (!spheroid.Calculate())
                {
                    _logger.LogWarning("Impossible to calculate outputs for the given Spheroid");
                    return false;
                }

                //if successful, check if another parent data with the same ID was calculated/added during the calculation time
                Model.Spheroid? newSpheroid = GetSpheroidById(spheroid.MetaInfo.ID);
                if (newSpheroid == null)
                {
                    //update SpheroidTable
                    var connection = _connectionManager.GetConnection();
                    if (connection != null)
                    {
                        using SqliteTransaction transaction = connection.BeginTransaction();
                        bool success = true;
                        try
                        {
                            //add the Spheroid to the SpheroidTable
                            string metaInfo = JsonSerializer.Serialize(spheroid.MetaInfo, JsonSettings.Options);
                            string data = JsonSerializer.Serialize(spheroid, JsonSettings.Options);
                            var command = connection.CreateCommand();
                            command.CommandText = "INSERT INTO SpheroidTable (" +
                                "ID, " +
                                "MetaInfo, " +
                                "Spheroid" +
                                ") VALUES (" +
                                $"'{spheroid.MetaInfo.ID}', " +
                                $"'{metaInfo}', " +
                                $"'{data}'" +
                                ")";
                            int count = command.ExecuteNonQuery();
                            if (count != 1)
                            {
                                _logger.LogWarning("Impossible to insert the given Spheroid into the SpheroidTable");
                                success = false;
                            }
                        }
                        catch (SqliteException ex)
                        {
                            _logger.LogError(ex, "Impossible to add the given Spheroid into SpheroidTable");
                            success = false;
                        }
                        //finalizing SQL transaction
                        if (success)
                        {
                            transaction.Commit();
                            _logger.LogInformation("Added the given Spheroid of given ID into the SpheroidTable successfully");
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
                    _logger.LogWarning("Impossible to post Spheroid. ID already found in database.");
                    return false;
                }

            }
            else
            {
                _logger.LogWarning("The Spheroid ID or the ID of its input are null or empty");
            }
            return false;
        }

        /// <summary>
        /// Performs calculation on the given Spheroid and updates it in the microservice database
        /// </summary>
        /// <param name="spheroid"></param>
        /// <returns>true if the given Spheroid has been updated successfully</returns>
        public bool UpdateSpheroidById(Guid guid, Model.Spheroid? spheroid)
        {
            bool success = true;
            if (guid != Guid.Empty && spheroid != null && spheroid.MetaInfo != null && spheroid.MetaInfo.ID == guid)
            {
                //calculate outputs
                if (!spheroid.Calculate())
                {
                    _logger.LogWarning("Impossible to calculate outputs of the given Spheroid");
                    return false;
                }
                //update SpheroidTable
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    using SqliteTransaction transaction = connection.BeginTransaction();
                    //update fields in SpheroidTable
                    try
                    {
                        string metaInfo = JsonSerializer.Serialize(spheroid.MetaInfo, JsonSettings.Options);
                        spheroid.LastModificationDate = DateTimeOffset.UtcNow;
                        string data = JsonSerializer.Serialize(spheroid, JsonSettings.Options);
                        var command = connection.CreateCommand();
                        command.CommandText = $"UPDATE SpheroidTable SET " +
                            $"MetaInfo = '{metaInfo}', " +
                            $"Spheroid = '{data}' " +
                            $"WHERE ID = '{guid}'";
                        int count = command.ExecuteNonQuery();
                        if (count != 1)
                        {
                            _logger.LogWarning("Impossible to update the Spheroid");
                            success = false;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to update the Spheroid");
                        success = false;
                    }

                    // Finalizing
                    if (success)
                    {
                        transaction.Commit();
                        _logger.LogInformation("Updated the given Spheroid successfully");
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
                _logger.LogWarning("The Spheroid ID or the ID of some of its attributes are null or empty");
            }
            return false;
        }

        /// <summary>
        /// Deletes the Spheroid of given ID from the microservice database
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>true if the Spheroid was deleted from the microservice database</returns>
        public bool DeleteSpheroidById(Guid guid)
        {
            if (!guid.Equals(Guid.Empty))
            {
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    using var transaction = connection.BeginTransaction();
                    bool success = true;
                    //delete Spheroid from SpheroidTable
                    try
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = $"DELETE FROM SpheroidTable WHERE ID = '{guid}'";
                        int count = command.ExecuteNonQuery();
                        if (count < 0)
                        {
                            _logger.LogWarning("Impossible to delete the Spheroid of given ID from the SpheroidTable");
                            success = false;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to delete the Spheroid of given ID from SpheroidTable");
                        success = false;
                    }
                    if (success)
                    {
                        transaction.Commit();
                        _logger.LogInformation("Removed the Spheroid of given ID from the SpheroidTable successfully");
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
                _logger.LogWarning("The Spheroid ID is null or empty");
            }
            return false;
        }

        /// <summary>
        /// populate database with default Spheroids
        /// </summary>
        private void FillDefault()
        {
            List<Model.Spheroid?> spheroids = typeof(Model.Spheroid)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(Model.Spheroid))     // only real datum props
                .Select(p => (Model.Spheroid?)p.GetValue(null))           // invoke each getter
                .Where(d => d is not null)                                     // drop any nulls
                .ToList();

            foreach (Model.Spheroid? sph in spheroids)
            {
                AddSpheroid(sph);
            }
        }
    }
}