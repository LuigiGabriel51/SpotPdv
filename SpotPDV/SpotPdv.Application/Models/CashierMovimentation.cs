using SpotPdv.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Models
{
    public class CashierMovimentation
    {
        public DateTime MovementDate { get; set; }
        public float Value { get; set; }
        public CashierMovimentType MovementType { get; set; }
    }
}
