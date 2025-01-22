using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Services
{
    public interface IDataBaseService
    {
        Task<T> GetFromCache<T>(string key);
        Task InsertObjectToCache<T>(string key, T originalObject);
    }
}
