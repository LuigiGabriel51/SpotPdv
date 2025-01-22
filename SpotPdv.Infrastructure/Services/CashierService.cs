using SpotPdv.Application.Models;
using SpotPdv.Application.Services;

namespace SpotPdv.Infrastructure.Services
{
    public class CashierService(IDataBaseService dataBaseService, OperationStateService operationStateService) : ICashierService
    {
    }
}
