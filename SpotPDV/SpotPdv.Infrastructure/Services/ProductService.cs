using SpotPdv.Application.Models;
using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Infrastructure.Services
{
    public class ProductService(IDataBaseService dataBaseService) : IProductService
    {
    }
}
