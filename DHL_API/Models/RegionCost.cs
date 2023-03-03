using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DHL_API.MachineOptimization;

namespace DHL_API.Models
{
    public class RegionCost
    {
        public List<Tuple<string, Dictionary<UnitRates, int>>> Table { get; set; }

    }
}