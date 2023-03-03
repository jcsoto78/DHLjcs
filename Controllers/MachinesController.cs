using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DHL_API.MachineOptimization;
using DHL_API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DHL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly List<Tuple<string, Dictionary<UnitRates, int>>> table = new List<Tuple<string, Dictionary<UnitRates, int>>>();

        // ctr
        public MachinesController()
        {
            // table[0]
            var indiaRates = new Dictionary<UnitRates, int>();
            indiaRates.Add(UnitRates.X10Large, 2970);
            indiaRates.Add(UnitRates.X8Large, 1300);
            indiaRates.Add(UnitRates.X4Large, 890);
            indiaRates.Add(UnitRates.X2Large, 413);
            indiaRates.Add(UnitRates.Large, 140);
            // table[1]
            var chinaRates = new Dictionary<UnitRates, int>();
            chinaRates.Add(UnitRates.X8Large, 1180);
            chinaRates.Add(UnitRates.X4Large, 670);
            chinaRates.Add(UnitRates.XLarge, 200);
            chinaRates.Add(UnitRates.Large, 110);
            // table[2]
            var NYRates = new Dictionary<UnitRates, int>();
            NYRates.Add(UnitRates.X10Large, 2820);
            NYRates.Add(UnitRates.X8Large, 1400);
            NYRates.Add(UnitRates.X4Large, 774);
            NYRates.Add(UnitRates.X2Large, 450);
            NYRates.Add(UnitRates.XLarge, 230);
            NYRates.Add(UnitRates.Large, 120);

            table
            .Add(new Tuple<string, Dictionary<UnitRates, int>>("India", indiaRates));
            table
            .Add(new Tuple<string, Dictionary<UnitRates, int>>("China", chinaRates));
            table
            .Add(new Tuple<string, Dictionary<UnitRates, int>>("NewYork", NYRates));

        }


        // defaults: capacity 1150, hours 1
        [HttpGet]
        public async Task<ActionResult<List<MachineResponseDTO>>> GetMachines()
        {
            return await new MachineOptimization().optimize(1150, table, 1);
        }
        
        [HttpGet("{capacity}/{hours}")]
        public async Task<ActionResult<List<MachineResponseDTO>>> GetMachines(int capacity, int hours)
        {
            return await new MachineOptimization().optimize(capacity, table, hours);
        }
        


    }
}