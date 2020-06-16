using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaOrder.Data;
using PizzaOrder.Data.Entities;

namespace PizzaOrder.Business.Services
{
    public interface IStoreService
    {
        IEnumerable<Store> GetAllStoreForOrder(int orderId);
        Task<Store> GetStoreAsync(int StoreId);
        Task<IEnumerable<Store>> CreateBulkAsync(IEnumerable<Store> Store, int orderId);
        Task<int> DeleteStoreAsync(int StoreId);
        Store GetStoreOrError();
    }

    public class StoreService : IStoreService
    {
        private readonly DooryContext _dbContext;

        public StoreService(DooryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Store> GetStoreAsync(int StoreId)
        {
            return await _dbContext.Store
                .FindAsync(StoreId);
        }

        public IEnumerable<Store> GetAllStoreForOrder(int orderId)
        {
            return _dbContext.Store.Where(x => x.StoreId == orderId).ToList();
        }

        public async Task<IEnumerable<Store>> CreateBulkAsync(IEnumerable<Store> Store, int orderId)
        {
            await _dbContext.Store.AddRangeAsync(Store);
            await _dbContext.SaveChangesAsync();
            return _dbContext.Store.Where(x => x.StoreId == orderId);
        }

        public async Task<int> DeleteStoreAsync(int StoreId)
        {
            var Store = await _dbContext.Store.FindAsync(StoreId);

            if (Store != null)
            {
                int orderId = Store.StoreId;
                _dbContext.Store.Remove(Store);
                await _dbContext.SaveChangesAsync();
                return orderId;
            }

            return 0;
        }

        public Store GetStoreOrError()
        {
            //bool generateError = (DateTime.Now.Millisecond % 2 == 0);
            //if (generateError)
            //    throw new Exception("Specific Error Message will came here.");

            return new Store { StoreId = 1, StoreName = "Without Error" };
        }
    }
}
