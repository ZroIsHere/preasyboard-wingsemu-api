using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PreasyBoard.Api.Attributes;
using PreasyBoard.Api.RequestEntities;
using System;
using WingsAPI.Communication.DbServer.TimeSpaceService;

namespace PreasyBoard.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeSpaceController : Controller
    {
        private readonly IServiceProvider _container;

        private readonly ILogger<TimeSpaceController> _logger;

        public TimeSpaceController(ILogger<TimeSpaceController> logger, IServiceProvider container)
        {
            _logger = logger;
            _container = container;
        }

        [Authorize]
        [HttpPost("GetTimeSpaceRecord")]
        public TimeSpaceRecordResponse GetTimeSpaceRecord(OnlyAnLongRequest Req)
        {
            return _container.GetService<ITimeSpaceService>().GetTimeSpaceRecord(new()
            {
                TimeSpaceId = Req.Value
            }).Result;
        }
    }
}
