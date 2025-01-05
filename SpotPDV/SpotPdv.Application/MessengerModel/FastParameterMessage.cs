using SpotPdv.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.MessengerModel
{
    public class FastParameterMessage
    {
        public OperationStateModel OperationState { get; set; }
        public string Route { get; set; }
        public FastParameterMessage(OperationStateModel operationState, string route)
        {
            OperationState = operationState;
            Route = route;
        }
    }
}
