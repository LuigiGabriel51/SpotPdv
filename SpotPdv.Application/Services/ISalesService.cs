
using SpotPdv.Application.Models;

namespace SpotPdv.Application.Services
{
    public interface ISalesService
    {
        public Task<IEnumerable<Sale>> GetSaleHistoric();
        public Task<bool> PostSale(Sale sale);
    }
}
