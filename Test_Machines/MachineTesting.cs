using System;
using Xunit;
using DHL_API;
using DHL_API.Controllers;
using DHL_API.Services;
using DHL_API.DTOs;

namespace Test_Machines
{
    public class MachineTesting
    {
        // private readonly MachinesController _controller;
        private readonly IRegionCostService _service;

        MachineOptimization SUT = new MachineOptimization();

        public MachineTesting()
        {
            _service = new RegionCostService();
        }

        [Fact]
        public void IndiaTest()
        {
            // Arrange
            var table = _service.Get().Table;
            int hours = 1;
            int capacity = 1150;
            string region = "India";
            int regionCode = 0; // india
            int totalCostShouldBe = 9520;

            var indiaResult = SUT.getRegionResult(capacity, hours, region, regionCode, table).Result;

            Assert.IsType<MachineResponseDTO>(indiaResult);
            Assert.Equal(indiaResult.TotalCost, totalCostShouldBe);
            Assert.Equal(indiaResult.Region, region);


        }
        [Fact]
        public void ChinaTest()
        {
            // Arrange
            var table = _service.Get().Table;
            int hours = 1;
            int capacity = 1150;
            string region = "China";
            int regionCode = 1; // China
            int totalCostShouldBe = 8570;

            var chinaResult = SUT.getRegionResult(capacity, hours, region, regionCode, table).Result;

            Assert.IsType<MachineResponseDTO>(chinaResult);
            Assert.Equal(chinaResult.TotalCost, totalCostShouldBe);
            Assert.Equal(chinaResult.Region, region);

        }
    }
}