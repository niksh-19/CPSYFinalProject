using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSYFinalProject
{
    public class Rental
    {
        public int rentalId { get; set; }
        public DateTime date { get; set; }
        public int customerId { get; set; }
        public int equipmentId { get; set; }
        public DateTime rentalDate { get; set; }
        public DateTime returnDate { get; set; }
        public decimal cost { get; set; }
    }
}
