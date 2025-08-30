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
    public class SpheroidController : ControllerBase
    {
        private readonly ILogger<SpheroidManager> _logger;
        private readonly SpheroidManager _spheroidManager;

        public SpheroidController(ILogger<SpheroidManager> logger, SqlConnectionManager connectionManager)
        {
            _logger = logger;
            _spheroidManager = SpheroidManager.GetInstance(_logger, connectionManager);
        }

        /// <summary>
        /// Returns the list of Guid of all Spheroid present in the microservice database at endpoint Spheroid/api/Spheroid
        /// </summary>
        /// <returns>the list of Guid of all Spheroid present in the microservice database at endpoint Spheroid/api/Spheroid</returns>
        [HttpGet(Name = "GetAllSpheroidId")]
        public ActionResult<IEnumerable<Guid>> GetAllSpheroidId()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllSpheroidIdPerDay();
            var ids = _spheroidManager.GetAllSpheroidId();
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
        /// Returns the list of MetaInfo of all Spheroid present in the microservice database, at endpoint Spheroid/api/Spheroid/MetaInfo
        /// </summary>
        /// <returns>the list of MetaInfo of all Spheroid present in the microservice database, at endpoint Spheroid/api/Spheroid/MetaInfo</returns>
        [HttpGet("MetaInfo", Name = "GetAllSpheroidMetaInfo")]
        public ActionResult<IEnumerable<MetaInfo>> GetAllSpheroidMetaInfo()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllSpheroidMetaInfoPerDay();
            var vals = _spheroidManager.GetAllSpheroidMetaInfo();
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
        /// Returns the Spheroid identified by its Guid from the microservice database, at endpoint Spheroid/api/Spheroid/MetaInfo/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>the Spheroid identified by its Guid from the microservice database, at endpoint Spheroid/api/Spheroid/MetaInfo/id</returns>
        [HttpGet("{id}", Name = "GetSpheroidById")]
        public ActionResult<Model.Spheroid?> GetSpheroidById(Guid id)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetSpheroidByIdPerDay();
            if (!id.Equals(Guid.Empty))
            {
                var val = _spheroidManager.GetSpheroidById(id);
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
        /// Returns the list of all Spheroid present in the microservice database, at endpoint Spheroid/api/Spheroid/HeavyData
        /// </summary>
        /// <returns>the list of all Spheroid present in the microservice database, at endpoint Spheroid/api/Spheroid/HeavyData</returns>
        [HttpGet("HeavyData", Name = "GetAllSpheroid")]
        public ActionResult<IEnumerable<Model.Spheroid?>> GetAllSpheroid()
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementGetAllSpheroidPerDay();
            var vals = _spheroidManager.GetAllSpheroid();
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
        /// Performs calculation on the given Spheroid and adds it to the microservice database, at the endpoint Spheroid/api/Spheroid
        /// </summary>
        /// <param name="spheroid"></param>
        /// <returns>true if the given Spheroid has been added successfully to the microservice database, at the endpoint Spheroid/api/Spheroid</returns>
        [HttpPost(Name = "PostSpheroid")]
        public ActionResult PostSpheroid([FromBody] Model.Spheroid? data)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementPostSpheroidPerDay();
            // Check if spheroid exists in the database through ID
            if (data != null && data.MetaInfo != null && data.MetaInfo.ID != Guid.Empty)
            {
                var existingData = _spheroidManager.GetSpheroidById(data.MetaInfo.ID);
                if (existingData == null)
                {   
                    //  If spheroid was not found, call AddSpheroid, where the spheroid.Calculate()
                    // method is called. 
                    if (_spheroidManager.AddSpheroid(data))
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
                    _logger.LogWarning("The given Spheroid already exists and will not be added");
                    return StatusCode(StatusCodes.Status409Conflict);
                }
            }
            else
            {
                _logger.LogWarning("The given Spheroid is null, badly formed, or its ID is empty");
                return BadRequest();
            }
        }

        /// <summary>
        /// Performs calculation on the given Spheroid and updates it in the microservice database, at the endpoint Spheroid/api/Spheroid/id
        /// </summary>
        /// <param name="spheroid"></param>
        /// <returns>true if the given Spheroid has been updated successfully to the microservice database, at the endpoint Spheroid/api/Spheroid/id</returns>
        [HttpPut("{id}", Name = "PutSpheroidById")]
        public ActionResult PutSpheroidById(Guid id, [FromBody] Model.Spheroid? data)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementPutSpheroidByIdPerDay();
            // Check if Spheroid is in the data base
            if (data != null && data.MetaInfo != null && data.MetaInfo.ID.Equals(id))
            {
                var existingData = _spheroidManager.GetSpheroidById(id);
                if (existingData != null)
                {
                    if (_spheroidManager.UpdateSpheroidById(id, data))
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
                    _logger.LogWarning("The given Spheroid has not been found in the database");
                    return NotFound();
                }
            }
            else
            {
                _logger.LogWarning("The given Spheroid is null, badly formed, or its does not match the ID to update");
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes the Spheroid of given ID from the microservice database, at the endpoint Spheroid/api/Spheroid/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>true if the Spheroid was deleted from the microservice database, at the endpoint Spheroid/api/Spheroid/id</returns>
        [HttpDelete("{id}", Name = "DeleteSpheroidById")]
        public ActionResult DeleteSpheroidById(Guid id)
        {
            UsageStatisticsGeodeticDatum.Instance.IncrementDeleteSpheroidByIdPerDay();
            if (_spheroidManager.GetSpheroidById(id) != null)
            {
                if (_spheroidManager.DeleteSpheroidById(id))
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
                _logger.LogWarning("The Spheroid of given ID does not exist");
                return NotFound();
            }
        }
    }
}
