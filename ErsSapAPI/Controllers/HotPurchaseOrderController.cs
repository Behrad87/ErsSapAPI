using ErsSapAPI.Models;
using ErsSapAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErsSapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotPurchaseOrderController : ControllerBase
    {
        private readonly ILogger<HotPurchaseOrderController> _logger;
        private readonly IHotPoService _hotPoService;

        public HotPurchaseOrderController(ILogger<HotPurchaseOrderController> logger,IHotPoService hotPoService)
        {
            _logger = logger;
            _hotPoService = hotPoService;
        }

        [HttpGet]
        //public IEnumerable<HotPurchaseOrder> Get()


        //{
        //    HotPurchaseOrder hotPurchaseOrder = new HotPurchaseOrder();
        //    HotPurchaseOrder a = _hotPoService.TestCustomModel(hotPurchaseOrder);


        //}
    }
}
