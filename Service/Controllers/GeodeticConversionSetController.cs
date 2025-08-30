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
    public class GeodeticConversionSetController : ControllerBase
    {
        private readonly ILogger<GeodeticConversionSetManager> _logger;
        private readonly GeodeticConversionSetManager _geodeticConversionSetManager;

        public GeodeticConversionSetController(ILogger<GeodeticConversionSetManager> logger, SqlConnectionManager connectionManager)
        {
            _logger = logger;
            _geodeticConversionSetManager = GeodeticConversionSetManager.GetInstance(_logger, connectionManager);
        }

        /// <summary>
        /// Returns the list of Guid of all GeodeticConversionSet present in the microservice database at endpoint GeodeticConversionSet/api/GeodeticConversionSet
        /// </summary>
        /// <returns>the list of Guid of all GeodeticConversionSet present in the microservice database at endpoint GeodeticConversionSet/api/GeodeticConversionSet</returns>
        [HttpGet(Name = "GetAllGeodeticConversionSetId")]
        public ActionResult<IEnumerable<Guid>> GetAllGeodeticConversionSetId()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticConversionSetIdPerDay();
            var ids = _geodeticConversionSetManager.GetAllGeodeticConversionSetId();
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
        /// Returns the list of MetaInfo of all GeodeticConversionSet present in the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/MetaInfo
        /// </summary>
        /// <returns>the list of MetaInfo of all GeodeticConversionSet present in the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/MetaInfo</returns>
        [HttpGet("MetaInfo", Name = "GetAllGeodeticConversionSetMetaInfo")]
        public ActionResult<IEnumerable<MetaInfo>> GetAllGeodeticConversionSetMetaInfo()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticConversionSetMetaInfoPerDay();
            var vals = _geodeticConversionSetManager.GetAllGeodeticConversionSetMetaInfo();
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
        /// Returns the GeodeticConversionSet identified by its Guid from the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/MetaInfo/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the GeodeticConversionSet identified by its Guid from the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/MetaInfo/id</returns>
        [HttpGet("{id}", Name = "GetGeodeticConversionSetById")]
        public ActionResult<Model.GeodeticConversionSet?> GetGeodeticConversionSetById(Guid id)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetGeodeticConversionSetByIdPerDay();
            if (!id.Equals(Guid.Empty))
            {
                var val = _geodeticConversionSetManager.GetGeodeticConversionSetById(id);
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
        /// Returns the list of all GeodeticConversionSetLight present in the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/LightData
        /// </summary>
        /// <returns>the list of all GeodeticConversionSetLight present in the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/LightData</returns>
        [HttpGet("LightData", Name = "GetAllGeodeticConversionSetLight")]
        public ActionResult<IEnumerable<Model.GeodeticConversionSetLight>> GetAllGeodeticConversionSetLight()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticConversionSetLightPerDay();
            var vals = _geodeticConversionSetManager.GetAllGeodeticConversionSetLight();
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
        /// Returns the list of all GeodeticConversionSet present in the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/HeavyData
        /// </summary>
        /// <returns>the list of all GeodeticConversionSet present in the microservice database, at endpoint GeodeticConversionSet/api/GeodeticConversionSet/HeavyData</returns>
        [HttpGet("HeavyData", Name = "GetAllGeodeticConversionSet")]
        public ActionResult<IEnumerable<Model.GeodeticConversionSet?>> GetAllGeodeticConversionSet()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllGeodeticConversionSetPerDay();
            var vals = _geodeticConversionSetManager.GetAllGeodeticConversionSet();
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
        /// Performs calculation on the given GeodeticConversionSet and adds it to the microservice database, at the endpoint GeodeticConversionSet/api/GeodeticConversionSet
        /// </summary>
        /// <param name="geodeticConversionSet"></param>
        /// <returns>true if the given GeodeticConversionSet has been added successfully to the microservice database, at the endpoint GeodeticConversionSet/api/GeodeticConversionSet</returns>
        [HttpPost(Name = "PostGeodeticConversionSet")]
        public ActionResult PostGeodeticConversionSet([FromBody] Model.GeodeticConversionSet? data)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementPostGeodeticConversionSetPerDay();
            // Check if geodeticConversionSet exists in the database through ID
            if (data != null && data.MetaInfo != null && data.MetaInfo.ID != Guid.Empty)
            {
                var existingData = _geodeticConversionSetManager.GetGeodeticConversionSetById(data.MetaInfo.ID);
                if (existingData == null)
                {   
                    //  If geodeticConversionSet was not found, call AddGeodeticConversionSet, where the geodeticConversionSet.Calculate()
                    // method is called. 
                    if (_geodeticConversionSetManager.AddGeodeticConversionSet(data))
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
                    _logger.LogWarning("The given GeodeticConversionSet already exists and will not be added");
                    return StatusCode(StatusCodes.Status409Conflict);
                }
            }
            else
            {
                _logger.LogWarning("The given GeodeticConversionSet is null, badly formed, or its ID is empty");
                return BadRequest();
            }
        }

        /// <summary>
        /// Performs calculation on the given GeodeticConversionSet and updates it in the microservice database, at the endpoint GeodeticConversionSet/api/GeodeticConversionSet/id
        /// </summary>
        /// <param name="geodeticConversionSet"></param>
        /// <returns>true if the given GeodeticConversionSet has been updated successfully to the microservice database, at the endpoint GeodeticConversionSet/api/GeodeticConversionSet/id</returns>
        [HttpPut("{id}", Name = "PutGeodeticConversionSetById")]
        public ActionResult PutGeodeticConversionSetById(Guid id, [FromBody] Model.GeodeticConversionSet? data)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementPutGeodeticConversionSetByIdPerDay();
            // Check if GeodeticConversionSet is in the data base
            if (data != null && data.MetaInfo != null && data.MetaInfo.ID.Equals(id))
            {
                var existingData = _geodeticConversionSetManager.GetGeodeticConversionSetById(id);
                if (existingData != null)
                {
                    if (_geodeticConversionSetManager.UpdateGeodeticConversionSetById(id, data))
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
                    _logger.LogWarning("The given GeodeticConversionSet has not been found in the database");
                    return NotFound();
                }
            }
            else
            {
                _logger.LogWarning("The given GeodeticConversionSet is null, badly formed, or its does not match the ID to update");
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes the GeodeticConversionSet of given ID from the microservice database, at the endpoint GeodeticConversionSet/api/GeodeticConversionSet/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>true if the GeodeticConversionSet was deleted from the microservice database, at the endpoint GeodeticConversionSet/api/GeodeticConversionSet/id</returns>
        [HttpDelete("{id}", Name = "DeleteGeodeticConversionSetById")]
        public ActionResult DeleteGeodeticConversionSetById(Guid id)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementDeleteGeodeticConversionSetByIdPerDay();
            if (_geodeticConversionSetManager.GetGeodeticConversionSetById(id) != null)
            {
                if (_geodeticConversionSetManager.DeleteGeodeticConversionSetById(id))
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
                _logger.LogWarning("The GeodeticConversionSet of given ID does not exist");
                return NotFound();
            }
        }
    }
}
