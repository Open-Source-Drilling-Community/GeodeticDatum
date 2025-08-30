using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using NORCE.Drilling.GeodeticDatum.Service.Managers;
using OSDC.DotnetLibraries.General.DataManagement;
using System;
using System.Collections.Generic;

namespace NORCE.Drilling.GeodeticDatum.Service.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class GeodeticDatumController : ControllerBase
    {
        private readonly ILogger<GeodeticDatumManager> _logger;
        private readonly GeodeticDatumManager _geodeticDatumManager;

        public GeodeticDatumController(ILogger<GeodeticDatumManager> logger, SqlConnectionManager connectionManager)
        {
            _logger = logger;
            _geodeticDatumManager = GeodeticDatumManager.GetInstance(_logger, connectionManager);
        }

        /// <summary>
        /// Returns the list of Guid of all GeodeticDatum present in the microservice database at endpoint GeodeticDatum/api/GeodeticDatum
        /// </summary>
        /// <returns>the list of Guid of all GeodeticDatum present in the microservice database at endpoint GeodeticDatum/api/GeodeticDatum</returns>
        [HttpGet(Name = "GetAllGeodeticDatumId")]
        public ActionResult<IEnumerable<Guid>> GetAllGeodeticDatumId()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticDatumIdPerDay();
            var ids = _geodeticDatumManager.GetAllGeodeticDatumId();
            if (ids != null)
            {
                return Ok(ids);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Returns the list of MetaInfo of all GeodeticDatum present in the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/MetaInfo
        /// </summary>
        /// <returns>the list of MetaInfo of all GeodeticDatum present in the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/MetaInfo</returns>
        [HttpGet("MetaInfo", Name = "GetAllGeodeticDatumMetaInfo")]
        public ActionResult<IEnumerable<MetaInfo>> GetAllGeodeticDatumMetaInfo()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticDatumMetaInfoPerDay();
            var vals = _geodeticDatumManager.GetAllGeodeticDatumMetaInfo();
            if (vals != null)
            {
                return Ok(vals);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Returns the GeodeticDatum identified by its Guid from the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/MetaInfo/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the GeodeticDatum identified by its Guid from the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/MetaInfo/id</returns>
        [HttpGet("{id}", Name = "GetGeodeticDatumById")]
        public ActionResult<Model.GeodeticDatum?> GetGeodeticDatumById(Guid id)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetGeodeticDatumByIdPerDay();
            if (!id.Equals(Guid.Empty))
            {
                var val = _geodeticDatumManager.GetGeodeticDatumById(id);
                if (val != null)
                {
                    return Ok(val);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Returns the list of all GeodeticDatumLight present in the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/LightData
        /// </summary>
        /// <returns>the list of all GeodeticDatumLight present in the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/LightData</returns>
        [HttpGet("LightData", Name = "GetAllGeodeticDatumLight")]
        public ActionResult<IEnumerable<Model.GeodeticDatumLight>> GetAllGeodeticDatumLight()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticDatumLightPerDay();
            var vals = _geodeticDatumManager.GetAllGeodeticDatumLight();
            if (vals != null)
            {
                return Ok(vals);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Returns the list of all GeodeticDatum present in the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/HeavyData
        /// </summary>
        /// <returns>the list of all GeodeticDatum present in the microservice database, at endpoint GeodeticDatum/api/GeodeticDatum/HeavyData</returns>
        [HttpGet("HeavyData", Name = "GetAllGeodeticDatum")]
        public ActionResult<IEnumerable<Model.GeodeticDatum?>> GetAllGeodeticDatum()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticDatumPerDay();
            var vals = _geodeticDatumManager.GetAllGeodeticDatum();
            if (vals != null)
            {
                return Ok(vals);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Performs calculation on the given GeodeticDatum and adds it to the microservice database, at the endpoint GeodeticDatum/api/GeodeticDatum
        /// </summary>
        /// <param name="geodeticDatum"></param>
        /// <returns>true if the given GeodeticDatum has been added successfully to the microservice database, at the endpoint GeodeticDatum/api/GeodeticDatum</returns>
        [HttpPost(Name = "PostGeodeticDatum")]
        public ActionResult PostGeodeticDatum([FromBody] Model.GeodeticDatum? data)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementPostGeodeticDatumPerDay();
            // Check if geodeticDatum exists in the database through ID
            if (data != null && data.MetaInfo != null && data.MetaInfo.ID != Guid.Empty)
            {
                var existingData = _geodeticDatumManager.GetGeodeticDatumById(data.MetaInfo.ID);
                if (existingData == null)
                {   
                    //  If geodeticDatum was not found, call AddGeodeticDatum, where the geodeticDatum.Calculate()
                    // method is called. 
                    if (_geodeticDatumManager.AddGeodeticDatum(data))
                    {
                        return Ok(); // status=OK is used rather than status=Created because NSwag auto-generated controllers use 200 (OK) rather than 201 (Created) as return codes
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
                else
                {
                    _logger.LogWarning("The given GeodeticDatum already exists and will not be added");
                    return StatusCode(StatusCodes.Status409Conflict);
                }
            }
            else
            {
                _logger.LogWarning("The given GeodeticDatum is null, badly formed, or its ID is empty");
                return BadRequest();
            }
        }

        /// <summary>
        /// Performs calculation on the given GeodeticDatum and updates it in the microservice database, at the endpoint GeodeticDatum/api/GeodeticDatum/id
        /// </summary>
        /// <param name="geodeticDatum"></param>
        /// <returns>true if the given GeodeticDatum has been updated successfully to the microservice database, at the endpoint GeodeticDatum/api/GeodeticDatum/id</returns>
        [HttpPut("{id}", Name = "PutGeodeticDatumById")]
        public ActionResult PutGeodeticDatumById(Guid id, [FromBody] Model.GeodeticDatum? data)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementPutGeodeticDatumByIdPerDay();
            // Check if GeodeticDatum is in the data base
            if (data != null && data.MetaInfo != null && data.MetaInfo.ID.Equals(id))
            {
                var existingData = _geodeticDatumManager.GetGeodeticDatumById(id);
                if (existingData != null)
                {
                    if (_geodeticDatumManager.UpdateGeodeticDatumById(id, data))
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                }
                else
                {
                    _logger.LogWarning("The given GeodeticDatum has not been found in the database");
                    return NotFound();
                }
            }
            else
            {
                _logger.LogWarning("The given GeodeticDatum is null, badly formed, or its does not match the ID to update");
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes the GeodeticDatum of given ID from the microservice database, at the endpoint GeodeticDatum/api/GeodeticDatum/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>true if the GeodeticDatum was deleted from the microservice database, at the endpoint GeodeticDatum/api/GeodeticDatum/id</returns>
        [HttpDelete("{id}", Name = "DeleteGeodeticDatumById")]
        public ActionResult DeleteGeodeticDatumById(Guid id)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementDeleteGeodeticDatumByIdPerDay();
            if (_geodeticDatumManager.GetGeodeticDatumById(id) != null)
            {
                if (_geodeticDatumManager.DeleteGeodeticDatumById(id))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                _logger.LogWarning("The GeodeticDatum of given ID does not exist");
                return NotFound();
            }
        }
    }
}
