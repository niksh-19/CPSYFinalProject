using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSYFinalProject
{
    public class RentalEquipment
    {
        public int equipmentId { get; set; }
        public int categoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal dailyRate { get; set; }
    }
}
