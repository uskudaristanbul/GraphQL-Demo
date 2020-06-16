using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Business.Helpers;
using PizzaOrder.Business.Models;
using PizzaOrder.Data;
using PizzaOrder.Data.Entities;
using PizzaOrder.Data.Enums;

namespace PizzaOrder.Business.Services
{
    public interface IProductService
    {
        Task<Product> CreateAsync(Product Product);
        Task<PageResponse<Product>> GetCompletedOrdersAsync(PageRequest pageRequest);
        Task<Product> GetProductAsync(int orderId);
        Task<IEnumerable<Product>> GettAllNewOrdersAsync();
        Task<Product> UpdateStatusAsync(int orderId, OrderStatus orderStatus);
    }

    public class ProductService : IProductService
    {
        private readonly DooryContext _dbContext;
        private readonly IEventService _eventService;

        public ProductService(DooryContext dbContext, IEventService eventService)
        {
            _dbContext = dbContext;
            _eventService = eventService;
        }

        public async Task<IEnumerable<Product>> GettAllNewOrdersAsync()
        {
            return await _dbContext.Product
                .Where(x => x.CategoryId == 1)
                .ToListAsync();
        }

        public async Task<Product> GetProductAsync(int orderId)
        {
            return await _dbContext.Product.FindAsync(orderId);
        }

        public async Task<Product> CreateAsync(Product Product)
        {
            _dbContext.Product.Add(Product);
            await _dbContext.SaveChangesAsync();
            _eventService.CreateOrderEvent(new Models.EventDataModel(Product.ProductId));
            return Product;
        }

        public async Task<Product> UpdateStatusAsync(int orderId, OrderStatus orderStatus)
        {
            var Product = await _dbContext.Product.FindAsync(orderId);

            if (Product != null)
            {
                //Product.OrderStatus = orderStatus;
                await _dbContext.SaveChangesAsync();
                _eventService.StatusUpdateEvent(new Models.EventDataModel(orderId, orderStatus));
            }

            return Product;
        }

        public async Task<PageResponse<Product>> GetCompletedOrdersAsync(PageRequest pageRequest)
        {
            //var filterQuery = _dbContext.Product
            //    .Where(x => x.OrderStatus == OrderStatus.Delivered);

            //#region Obtain Nodes

            //var dataQuery = filterQuery;
            //if (pageRequest.First.HasValue)
            //{
            //    if (!string.IsNullOrEmpty(pageRequest.After))
            //    {
            //        int lastId = CursorHelper.FromCursor(pageRequest.After);
            //        dataQuery = dataQuery.Where(x => x.Id > lastId);
            //    }

            //    dataQuery = dataQuery.Take(pageRequest.First.Value);
            //}

            //if (pageRequest.OrderBy?.Field == Enums.CompletedOrdersSortingFields.Address)
            //{
            //    dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
            //        ? dataQuery.OrderByDescending(x => x.AddressLine1)
            //        : dataQuery.OrderBy(x => x.AddressLine1);
            //}
            //else if (pageRequest.OrderBy?.Field == Enums.CompletedOrdersSortingFields.Amount)
            //{
            //    dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
            //        ? dataQuery.OrderByDescending(x => x.Amount)
            //        : dataQuery.OrderBy(x => x.Amount);
            //}
            //else
            //{
            //    dataQuery = (pageRequest.OrderBy.Direction == Enums.SortingDirection.DESC)
            //        ? dataQuery.OrderByDescending(x => x.Id)
            //        : dataQuery.OrderBy(x => x.Id);
            //}

            //List<Product> nodes = await dataQuery.ToListAsync();

            //#endregion

            #region Obtain Flags

            //int maxId = nodes.Max(x => x.Id);
            //int minId = nodes.Min(x => x.Id);
            //bool hasNextPage = await filterQuery.AnyAsync(x => x.Id > maxId);
            //bool hasPrevPage = await filterQuery.AnyAsync(x => x.Id < minId);
            //int totalCount = await filterQuery.CountAsync();

            #endregion

            return new PageResponse<Product>
            {
                //Nodes = nodes,
                //HasNextPage = hasNextPage,
                //HasPreviousPage = hasPrevPage,
                //TotalCount = totalCount
            };
        }
    }
}
