using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RailwayApp.Data;
using RailwayApp.Models;

namespace RailwayApp.Services
{
    public interface IOrderService
    {
      Task<IEnumerable<Order>>  GetAsync();
        Task<Order> GetAsync(Guid id);
       Task<IEnumerable<Order>> GetAsync(string email);
        Task Delete(Guid id);
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDataContext _appDataContext;

        public OrderService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<IEnumerable<Order>> GetAsync()
        {
            return await  _appDataContext.Orders.ToListAsync();
        }

        public async Task<Order> GetAsync(Guid id)
        {
            return await Task.Run(() => _appDataContext.Orders.FirstOrDefaultAsync(o => o.Id == id));
        } 
        public async Task<IEnumerable<Order>> GetAsync(string email)
        {
            return await Task.Run(() => _appDataContext.Orders.Where(o => o.OrderUser.EmailUser == email));
        }
        public Task Delete(Guid id)
        {
            return Task.Run(() => _appDataContext.Orders.Remove(_appDataContext.Orders.FirstOrDefault(o => o.Id == id))); 
        }

        public async Task CreateAsync(Order order)
        {
            await Task.Run(() => _appDataContext.Orders.AddAsync(order));
            await _appDataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
           _appDataContext.Orders.Update(order);
          await  _appDataContext.SaveChangesAsync();
        }
    }
}
