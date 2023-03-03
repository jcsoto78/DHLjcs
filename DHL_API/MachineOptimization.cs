using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DHL_API.DTOs;

namespace DHL_API
{
    public class MachineOptimization
    {
        public enum UnitRates
        {
            Large = 10,
            XLarge = 20,
            X2Large = 40,
            X4Large = 80,
            X8Large = 160,
            X10Large = 320,

        }

        public async Task<MachineResponseDTO> getRegionResult(int capacity, int hours, string region, int regionCode, List<Tuple<string, Dictionary<UnitRates, int>>> table)
        {

            var myTask = new Task<MachineResponseDTO>(() =>
            {
                var result = new MachineResponseDTO { Region = region };
                var machineUnits = table[regionCode].Item2.Keys.AsEnumerable().Cast<int>();
                var costs = table[regionCode].Item2;
                var machines = optimizeByHour(machineUnits, capacity);

                decimal total = 0;
                foreach (var machine in machines)
                {
                    var unit = machine.Item1;
                    var quantity = machine.Item2;
                    var cost = costs.FirstOrDefault(c => c.Key.ToString() == unit).Value;

                    total += cost * quantity * hours;

                    result.Machines.Add(new Machine
                    {
                        MachineName = unit.ToString(),
                        quantity = quantity
                    });
                }

                result.TotalCost = total;

                return result;

            });

            myTask.Start();
            
            return await myTask;
        }

        public async Task<List<MachineResponseDTO>> optimize(int capacity, List<Tuple<string, Dictionary<UnitRates, int>>> table, int hours = 1)
        {
            var myTask = new Task<List<MachineResponseDTO>>(() => {

                var response = new List<MachineResponseDTO>();

                response.Add(getRegionResult(capacity, hours, "India", 0, table).Result);
                response.Add(getRegionResult(capacity, hours, "China", 1, table).Result);
                response.Add(getRegionResult(capacity, hours, "NewYork", 2, table).Result);

                return response;

            });
           
            myTask.Start();

            return await myTask;
        }

        // Gets the amount of machines needed per 1 hour, totals by hours should be calculated elsewhere
        public List<Tuple<string, int>> optimizeByHour(IEnumerable<int> countryRates, int units = 1150)
        {
            var rates = countryRates.OrderByDescending(e => e).ToArray<int>();

            // find the best xlarge best rate for remainin units 
            List<Tuple<string, int>> machinesNeeded = new List<Tuple<string, int>>();

            int index = 0;
            while (units != 0 && (index + 1) < rates.Length)
            {
                int remainIndex = units % rates[index];
                int remainNext = units % rates[index + 1];
                int cheapestRate = (remainNext < remainIndex) ? rates[index + 1] : rates[index];

                if (cheapestRate <= units)
                {
                    int remain = units % cheapestRate;
                    int machines = (units - remain) / cheapestRate;
                    units = remain;

                    machinesNeeded
                    .Add(new Tuple<string, int>(((UnitRates)cheapestRate).ToString(), machines));
                }

                ++index;
            }

            // machinesNeeded.ForEach(t =>
            // {
            //     Console.WriteLine($"{t.Item1} , {t.Item2}");
            // });

            return machinesNeeded;
        }
    }
}