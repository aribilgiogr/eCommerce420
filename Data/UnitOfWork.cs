using Core.Abstracts;
using Core.Abstracts.IRepositories;
using Data.Contexts;
using Data.Repositories;
using Tools.Responses;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext context;

        public UnitOfWork(ShopContext context)
        {
            this.context = context;
        }

        private ICustomerRepository? customers;
        public ICustomerRepository Customers => customers ??= new CustomerRepository(context);

        public async Task<Reply> CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                return Reply.Success();
            }
            catch (Exception ex)
            {
                return Reply.Fail(ex.Message);
            }
        }

        public async ValueTask DisposeAsync() => await context.DisposeAsync();
    }
}
