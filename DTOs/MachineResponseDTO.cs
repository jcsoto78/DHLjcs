using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DHL_API.DTOs
{
    public class MachineResponseDTO
    {
        public string Region { get; set; }
        public decimal TotalCost { get; set; }

        public List<Machine> Machines { get; set; } = new List<Machine>();
    }
}