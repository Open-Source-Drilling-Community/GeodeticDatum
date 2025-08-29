using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OSDC.DotnetLibraries.General.DataManagement;
using Microsoft.Data.Sqlite;
using System.Text.Json;

namespace NORCE.Drilling.GeodeticDatum.Service.Managers
{
    /// <summary>
    /// A manager for GeodeticConversionSet. The manager implements the singleton pattern as defined by 
    /// Gamma, Erich, et al. "Design patterns: Abstraction and reuse of object-oriented design." 
    /// European Conference on Object-Oriented Programming. Springer, Berlin, Heidelberg, 1993.
    /// </summary>
    public class GeodeticConversionSetManager
    {
        private static GeodeticConversionSetManager? _instance = null;
        private readonly ILogger<GeodeticConversionSetManager> _logger;
        private readonly object _lock = new();
        private readonly SqlConnectionManager _connectionManager;

        private GeodeticConversionSetManager(ILogger<GeodeticConversionSetManager> logger, SqlConnectionManager connectionManager)
        {
            _logger = logger;
            _connectionManager = connectionManager;
        }

        public static GeodeticConversionSetManager GetInstance(ILogger<GeodeticConversionSetManager> logger, SqlConnectionManager connectionManager)
        {
            _instance ??= new GeodeticConversionSetManager(logger, connectionManager);
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
                    command.CommandText = "SELECT COUNT(*) FROM GeodeticConversionSetTable";
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
                        _logger.LogError(ex, "Impossible to count records in the GeodeticConversionSetTable");
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
                        //empty GeodeticConversionSetTable
                        var command = connection.CreateCommand();
                        command.CommandText = "DELETE FROM GeodeticConversionSetTable";
                        command.ExecuteNonQuery();

                        transaction.Commit();
                        success = true;
                    }
                    catch (SqliteException ex)
                    {
                        transaction.Rollback();
                        _logger.LogError(ex, "Impossible to clear the GeodeticConversionSetTable");
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
                command.CommandText = $"SELECT COUNT(*) FROM GeodeticConversionSetTable WHERE ID = '{guid}'";
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
                    _logger.LogError(ex, "Impossible to count rows from GeodeticConversionSetTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return count >= 1;
        }

        /// <summary>
        /// Returns the list of Guid of all GeodeticConversionSet present in the microservice database 
        /// </summary>
        /// <returns>the list of Guid of all GeodeticConversionSet present in the microservice database</returns>
        public List<Guid>? GetAllGeodeticConversionSetId()
        {
            List<Guid> ids = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID FROM GeodeticConversionSetTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        Guid id = reader.GetGuid(0);
                        ids.Add(id);
                    }
                    _logger.LogInformation("Returning the list of ID of existing records from GeodeticConversionSetTable");
                    return ids;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get IDs from GeodeticConversionSetTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of MetaInfo of all GeodeticConversionSet present in the microservice database 
        /// </summary>
        /// <returns>the list of MetaInfo of all GeodeticConversionSet present in the microservice database</returns>
        public List<MetaInfo?>? GetAllGeodeticConversionSetMetaInfo()
        {
            List<MetaInfo?> metaInfos = new();
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT MetaInfo FROM GeodeticConversionSetTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        string mInfo = reader.GetString(0);
                        MetaInfo? metaInfo = JsonSerializer.Deserialize<MetaInfo>(mInfo, JsonSettings.Options);
                        metaInfos.Add(metaInfo);
                    }
                    _logger.LogInformation("Returning the list of MetaInfo of existing records from GeodeticConversionSetTable");
                    return metaInfos;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get IDs from GeodeticConversionSetTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the GeodeticConversionSet identified by its Guid from the microservice database 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the GeodeticConversionSet identified by its Guid from the microservice database</returns>
        public Model.GeodeticConversionSet? GetGeodeticConversionSetById(Guid guid)
        {
            if (!guid.Equals(Guid.Empty))
            {
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    Model.GeodeticConversionSet? geodeticConversionSet;
                    var command = connection.CreateCommand();
                    command.CommandText = $"SELECT GeodeticConversionSet FROM GeodeticConversionSetTable WHERE ID = '{guid}'";
                    try
                    {
                        using var reader = command.ExecuteReader();
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            string data = reader.GetString(0);
                            geodeticConversionSet = JsonSerializer.Deserialize<Model.GeodeticConversionSet>(data, JsonSettings.Options);
                            if (geodeticConversionSet != null && geodeticConversionSet.MetaInfo != null && !geodeticConversionSet.MetaInfo.ID.Equals(guid))
                                throw new SqliteException("SQLite database corrupted: returned GeodeticConversionSet is null or has been jsonified with the wrong ID.", 1);
                        }
                        else
                        {
                            _logger.LogInformation("No GeodeticConversionSet of given ID in the database");
                            return null;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to get the GeodeticConversionSet with the given ID from GeodeticConversionSetTable");
                        return null;
                    }
                    _logger.LogInformation("Returning the GeodeticConversionSet of given ID from GeodeticConversionSetTable");
                    return geodeticConversionSet;
                }
                else
                {
                    _logger.LogWarning("Impossible to access the SQLite database");
                }
            }
            else
            {
                _logger.LogWarning("The given GeodeticConversionSet ID is null or empty");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of all GeodeticConversionSet present in the microservice database 
        /// </summary>
        /// <returns>the list of all GeodeticConversionSet present in the microservice database</returns>
        public List<Model.GeodeticConversionSet?>? GetAllGeodeticConversionSet()
        {
            List<Model.GeodeticConversionSet?> vals = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT GeodeticConversionSet FROM GeodeticConversionSetTable";
                try
                {
                    using var reader = command.ExecuteReader();
                    while (reader.Read() && !reader.IsDBNull(0))
                    {
                        string data = reader.GetString(0);
                        Model.GeodeticConversionSet? geodeticConversionSet = JsonSerializer.Deserialize<Model.GeodeticConversionSet>(data, JsonSettings.Options);
                        vals.Add(geodeticConversionSet);
                    }
                    _logger.LogInformation("Returning the list of existing GeodeticConversionSet from GeodeticConversionSetTable");
                    return vals;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get GeodeticConversionSet from GeodeticConversionSetTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Returns the list of all GeodeticConversionSetLight present in the microservice database 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the list of GeodeticConversionSetLight present in the microservice database</returns>
        public List<Model.GeodeticConversionSetLight>? GetAllGeodeticConversionSetLight()
        {
            List<Model.GeodeticConversionSetLight>? geodeticConversionSetLightList = [];
            var connection = _connectionManager.GetConnection();
            if (connection != null)
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT MetaInfo, Name, Description, CreationDate, LastModificationDate, GeodeticDatumName, GeodeticDatumDescription FROM GeodeticConversionSetTable";
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
                        string geodeticDatumName = reader.GetString(5);
                        string geodeticDatumDescr = reader.GetString(6);
                        geodeticConversionSetLightList.Add(new Model.GeodeticConversionSetLight(
                                metaInfo,
                                string.IsNullOrEmpty(name) ? null : name,
                                string.IsNullOrEmpty(descr) ? null : descr,
                                creationDate,
                                lastModificationDate,
                                geodeticDatumName,
                                geodeticDatumDescr));
                    }
                    _logger.LogInformation("Returning the list of existing GeodeticConversionSetLight from GeodeticConversionSetTable");
                    return geodeticConversionSetLightList;
                }
                catch (SqliteException ex)
                {
                    _logger.LogError(ex, "Impossible to get light datas from GeodeticConversionSetTable");
                }
            }
            else
            {
                _logger.LogWarning("Impossible to access the SQLite database");
            }
            return null;
        }

        /// <summary>
        /// Performs calculation on the given GeodeticConversionSet and adds it to the microservice database
        /// </summary>
        /// <param name="geodeticConversionSet"></param>
        /// <returns>true if the given GeodeticConversionSet has been added successfully to the microservice database</returns>
        public bool AddGeodeticConversionSet(Model.GeodeticConversionSet? geodeticConversionSet)
        {
            if (geodeticConversionSet != null && geodeticConversionSet.MetaInfo != null && geodeticConversionSet.MetaInfo.ID != Guid.Empty)
            {
                //calculate outputs
                if (!geodeticConversionSet.Calculate())
                {
                    _logger.LogWarning("Impossible to calculate outputs for the given GeodeticConversionSet");
                    return false;
                }

                //if successful, check if another parent data with the same ID was calculated/added during the calculation time
                Model.GeodeticConversionSet? newGeodeticConversionSet = GetGeodeticConversionSetById(geodeticConversionSet.MetaInfo.ID);
                if (newGeodeticConversionSet == null)
                {
                    //update GeodeticConversionSetTable
                    var connection = _connectionManager.GetConnection();
                    if (connection != null)
                    {
                        using SqliteTransaction transaction = connection.BeginTransaction();
                        bool success = true;
                        try
                        {
                            //add the GeodeticConversionSet to the GeodeticConversionSetTable
                            string metaInfo = JsonSerializer.Serialize(geodeticConversionSet.MetaInfo, JsonSettings.Options);
                            string? cDate = null;
                            if (geodeticConversionSet.CreationDate != null)
                                cDate = ((DateTimeOffset)geodeticConversionSet.CreationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                            string? lDate = null;
                            if (geodeticConversionSet.LastModificationDate != null)
                                lDate = ((DateTimeOffset)geodeticConversionSet.LastModificationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                            string data = JsonSerializer.Serialize(geodeticConversionSet, JsonSettings.Options);
                            var command = connection.CreateCommand();
                            command.CommandText = "INSERT INTO GeodeticConversionSetTable (" +
                                "ID, " +
                                "MetaInfo, " +
                                "Name, " +
                                "Description, " +
                                "CreationDate, " +
                                "LastModificationDate, " +
                                "GeodeticDatumName, " +
                                "GeodeticDatumDescription, " +
                                "GeodeticConversionSet" +
                                ") VALUES (" +
                                $"'{geodeticConversionSet.MetaInfo.ID}', " +
                                $"'{metaInfo}', " +
                                $"'{geodeticConversionSet.Name}', " +
                                $"'{geodeticConversionSet.Description}', " +
                                $"'{cDate}', " +
                                $"'{lDate}', " +
                                $"'{geodeticConversionSet.GeodeticDatum!.Name}', " +
                                $"'{geodeticConversionSet.GeodeticDatum!.Description}', " +
                                $"'{data}'" +
                                ")";
                            int count = command.ExecuteNonQuery();
                            if (count != 1)
                            {
                                _logger.LogWarning("Impossible to insert the given GeodeticConversionSet into the GeodeticConversionSetTable");
                                success = false;
                            }
                        }
                        catch (SqliteException ex)
                        {
                            _logger.LogError(ex, "Impossible to add the given GeodeticConversionSet into GeodeticConversionSetTable");
                            success = false;
                        }
                        //finalizing SQL transaction
                        if (success)
                        {
                            transaction.Commit();
                            _logger.LogInformation("Added the given GeodeticConversionSet of given ID into the GeodeticConversionSetTable successfully");
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
                    _logger.LogWarning("Impossible to post GeodeticConversionSet. ID already found in database.");
                    return false;
                }

            }
            else
            {
                _logger.LogWarning("The GeodeticConversionSet ID or the ID of its input are null or empty");
            }
            return false;
        }

        /// <summary>
        /// Performs calculation on the given GeodeticConversionSet and updates it in the microservice database
        /// </summary>
        /// <param name="geodeticConversionSet"></param>
        /// <returns>true if the given GeodeticConversionSet has been updated successfully</returns>
        public bool UpdateGeodeticConversionSetById(Guid guid, Model.GeodeticConversionSet? geodeticConversionSet)
        {
            bool success = true;
            if (guid != Guid.Empty && geodeticConversionSet != null && geodeticConversionSet.MetaInfo != null && geodeticConversionSet.MetaInfo.ID == guid)
            {
                //calculate outputs
                if (!geodeticConversionSet.Calculate())
                {
                    _logger.LogWarning("Impossible to calculate outputs of the given GeodeticConversionSet");
                    return false;
                }
                //update GeodeticConversionSetTable
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    using SqliteTransaction transaction = connection.BeginTransaction();
                    //update fields in GeodeticConversionSetTable
                    try
                    {
                        string metaInfo = JsonSerializer.Serialize(geodeticConversionSet.MetaInfo, JsonSettings.Options);
                        string? cDate = null;
                        if (geodeticConversionSet.CreationDate != null)
                            cDate = ((DateTimeOffset)geodeticConversionSet.CreationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                        geodeticConversionSet.LastModificationDate = DateTimeOffset.UtcNow;
                        string? lDate = ((DateTimeOffset)geodeticConversionSet.LastModificationDate).ToString(SqlConnectionManager.DATE_TIME_FORMAT);
                        string data = JsonSerializer.Serialize(geodeticConversionSet, JsonSettings.Options);
                        var command = connection.CreateCommand();
                        command.CommandText = $"UPDATE GeodeticConversionSetTable SET " +
                                $"MetaInfo = '{metaInfo}', " +
                                $"Name = '{geodeticConversionSet.Name}', " +
                                $"Description = '{geodeticConversionSet.Description}', " +
                                $"CreationDate = '{cDate}', " +
                                $"LastModificationDate = '{lDate}', " +
                                $"GeodeticDatumName = '{geodeticConversionSet.GeodeticDatum!.Name}', " +
                                $"GeodeticDatumDescription = '{geodeticConversionSet.GeodeticDatum!.Description}', " +
                                $"GeodeticConversionSet = '{data}' " +
                                $"WHERE ID = '{guid}'";
                        int count = command.ExecuteNonQuery();
                        if (count != 1)
                        {
                            _logger.LogWarning("Impossible to update the GeodeticConversionSet");
                            success = false;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to update the GeodeticConversionSet");
                        success = false;
                    }

                    // Finalizing
                    if (success)
                    {
                        transaction.Commit();
                        _logger.LogInformation("Updated the given GeodeticConversionSet successfully");
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
                _logger.LogWarning("The GeodeticConversionSet ID or the ID of some of its attributes are null or empty");
            }
            return false;
        }

        /// <summary>
        /// Deletes the GeodeticConversionSet of given ID from the microservice database
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>true if the GeodeticConversionSet was deleted from the microservice database</returns>
        public bool DeleteGeodeticConversionSetById(Guid guid)
        {
            if (!guid.Equals(Guid.Empty))
            {
                var connection = _connectionManager.GetConnection();
                if (connection != null)
                {
                    using var transaction = connection.BeginTransaction();
                    bool success = true;
                    //delete GeodeticConversionSet from GeodeticConversionSetTable
                    try
                    {
                        var command = connection.CreateCommand();
                        command.CommandText = $"DELETE FROM GeodeticConversionSetTable WHERE ID = '{guid}'";
                        int count = command.ExecuteNonQuery();
                        if (count < 0)
                        {
                            _logger.LogWarning("Impossible to delete the GeodeticConversionSet of given ID from the GeodeticConversionSetTable");
                            success = false;
                        }
                    }
                    catch (SqliteException ex)
                    {
                        _logger.LogError(ex, "Impossible to delete the GeodeticConversionSet of given ID from GeodeticConversionSetTable");
                        success = false;
                    }
                    if (success)
                    {
                        transaction.Commit();
                        _logger.LogInformation("Removed the GeodeticConversionSet of given ID from the GeodeticConversionSetTable successfully");
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
                _logger.LogWarning("The GeodeticConversionSet ID is null or empty");
            }
            return false;
        }
    }
}