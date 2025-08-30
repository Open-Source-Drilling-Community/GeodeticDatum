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
    public class GeodeticDatumUsageStatisticsController : ControllerBase
    {
        private readonly ILogger _logger;

        public GeodeticDatumUsageStatisticsController(ILogger<GeodeticDatumUsageStatisticsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns the usage statistics present in the microservice database at endpoint GeodeticConversion/api/UsageStatistics
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetUsageStatistics")]
        public ActionResult<UsageStatisticsGeodeticDatum> GetUsageStatistics()
        {
            if (UsageStatisticsGeodeticDatum.Instance != null)
            {
                return Ok(UsageStatisticsGeodeticDatum.Instance);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
