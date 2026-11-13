using Microsoft.AspNetCore.Mvc;
using ZooSimulator.API.Models;
using ZooSimulator.API.Services;
using ZooSimulator.Core.Services.Implementation;

namespace ZooSimulator.API.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class ZooController : ControllerBase
    {
        private readonly IZooService _zooService;
        private readonly IZooApiService _zooApiService;
        public ZooController(IZooService zooService, IZooApiService zooApiService)
        {
            _zooService = zooService;
            _zooApiService = zooApiService;
        }

        [HttpGet]
        public ActionResult<ZooStatusResponse> GetStatus()
        {
            return Ok(_zooApiService.GetStatus());
        }

        [HttpPost("feed")]
        public ActionResult<FeedResponse> Feed()
        {
            return Ok(_zooApiService.Feed());
        }
    }
}
