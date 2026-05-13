using Core.Abstracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Responses;

namespace Core.Abstracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICustomerRepository Customers { get; }

        Task<Reply> CommitAsync();
    }
}
