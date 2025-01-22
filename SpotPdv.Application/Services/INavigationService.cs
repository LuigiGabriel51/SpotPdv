using SpotPdv.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Application.Services
{
    public interface INavigationService
    {
        public Task PushNavigateAsync<TViewModel, TParameter>(OperationStateModel parameter);
        public Task PopNavigateAsync(OperationStateModel parameter);

        public Task PushModalNavigateAsync<TViewModel, TParameter>(OperationStateModel parameter);
        public Task PopModalNavigateAsync(OperationStateModel parameter);

        public Task PopRootNavigateAsync(OperationStateModel parameter);
    }
}
