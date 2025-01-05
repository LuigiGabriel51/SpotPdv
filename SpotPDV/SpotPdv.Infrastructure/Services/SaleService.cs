using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Infrastructure.Services
{
    public class SaleService(IDataBaseService dataBaseService) : ISalesService
    {
        public async Task<IEnumerable<Sale>> GetSaleHistoric()
        {
            try
            {
                var sales = await dataBaseService.GetFromCache<List<Sale>>("sales");
                return sales != null ? sales : new List<Sale>();
            }
            catch(Exception e)
            {
                Console.WriteLine($"##Error {GetType().Name}: {e}##");
                return new List<Sale>();
            }
            
        }

        public async Task<bool> PostSale(Sale sale)
        {
            try
            {
                await dataBaseService.InsertObjectToCache("sales", sale);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"##Error {GetType().Name}: {e}##");
                return false;
            }
        }
    }
}
