using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DHL_API.MachineOptimization;
using DHL_API.DTOs;
using Microsoft.AspNetCore.Mvc;
using DHL_API.Services;
using DHL_API.Models;

namespace DHL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly IRegionCostService _regionCostService;
        // ctr
        public MachinesController(IRegionCostService service)
        {
            _regionCostService = service;
        }

        // defaults: capacity 1150, hours 1
        [HttpGet]
        public async Task<ActionResult<List<MachineResponseDTO>>> GetMachines()
        {
            var table = _regionCostService.Get().Table;
            return Ok(await new MachineOptimization().optimize(1150, table, 1));
        }
        
        [HttpGet("{capacity}/{hours}")]
        public async Task<ActionResult<List<MachineResponseDTO>>> GetMachines(int capacity, int hours)
        {
            var table = _regionCostService.Get().Table;
            return Ok(await new MachineOptimization().optimize(capacity, table, hours));
        }
        


    }
}