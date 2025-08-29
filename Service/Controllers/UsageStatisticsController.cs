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
    public class UsageStatisticsController : ControllerBase
    {
        private readonly ILogger _logger;

        public UsageStatisticsController(ILogger<UsageStatisticsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns the usage statistics present in the microservice database at endpoint UnitConversion/api/UsageStatistics
        /// </summary>
        /// <returns>the list of Guid of all PhysicalQuantity present in the microservice database at endpoint UnitConversion/api/PhysicalQuantity</returns>
        [HttpGet(Name = "GetUsageStatistics")]
        public ActionResult<UsageStatistics> GetUsageStatistics()
        {
            if (UsageStatistics.Instance != null)
            {
                return Ok(UsageStatistics.Instance);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
