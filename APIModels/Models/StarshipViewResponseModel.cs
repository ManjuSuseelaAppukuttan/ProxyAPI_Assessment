using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIModels.Models
{
    public class StarshipViewResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Model { get; set; }
        public string Manufacturer { get; set; } = default!;
        public string Consumables { get; set; } = default!;
        public List<string>? Films { get; set; }
    }
}
