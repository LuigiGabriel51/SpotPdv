using SpotPdv.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Services
{
    public interface IOperationStateService
    {
        Task SaveOperationState(OperationStateModel operationState);
        Task<OperationStateModel> LoadOperationState();
    }
}
