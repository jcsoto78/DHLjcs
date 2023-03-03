using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DHL_API.Models;

namespace DHL_API.Services
{
    public interface IRegionCostService
    {
        public RegionCost Get();
    }
}