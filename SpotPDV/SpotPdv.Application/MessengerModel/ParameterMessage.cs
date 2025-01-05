using SpotPdv.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.MessengerModel
{
    public class ParameterMessage
    {
        public OperationStateModel OperationState { get; set; }

        public ParameterMessage(OperationStateModel operationState)
        {
            OperationState = operationState;
        }
    }
}
