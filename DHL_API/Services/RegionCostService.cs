using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DHL_API.Models;
using static DHL_API.MachineOptimization;

namespace DHL_API.Services
{
    public class RegionCostService : IRegionCostService
    {
        private readonly RegionCost _regionCostRates = new RegionCost();
        public RegionCostService()
        {
            List<Tuple<string, Dictionary<UnitRates, int>>> table = new List<Tuple<string, Dictionary<UnitRates, int>>>();

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

            _regionCostRates.Table = table;
            
        }
        public RegionCost Get() => _regionCostRates;
    }
}