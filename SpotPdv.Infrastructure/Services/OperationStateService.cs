using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Infrastructure.Services
{
    public class OperationStateService(IDataBaseService dataBaseService) : IOperationStateService
    {
        public async Task<OperationStateModel> LoadOperationState()
        {
            var operationStateFromCache = await dataBaseService.GetFromCache<OperationStateModel>("operationState");

            if (operationStateFromCache == null)
            {
                return new OperationStateModel();
            }
            return operationStateFromCache;
        }
        public async Task SaveOperationState(OperationStateModel operationState)
        {
            await dataBaseService.InsertObjectToCache("operationState", operationState);
        }
    }
}
