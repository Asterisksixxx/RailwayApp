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
        IEnumerable<Route> GetRoute(Guid firstGuid, Guid secondGuid);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDataContext _appDataContext;
        private readonly IStationService _stationService;
        private readonly IUserService _userService;

        public OrderService(AppDataContext appDataContext, IStationService stationService, IUserService userService)
        {
            _appDataContext = appDataContext;
            _stationService = stationService;
            _userService = userService;
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
            Guid oui =await _userService.GetAsyncId(email);
            return await Task.Run(() => _appDataContext.Orders.Include(o=>o.OrderRoute).Include(o=>o.ChangeTrain).Where(o => o.OrderUserId==oui));
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

        public IEnumerable<Route> GetRoute(Guid firstGuid, Guid secondGuid)
        {
            var spisok= _appDataContext.Routes.Include(r => r.StationList);
               var spisachek= spisok.Where(r => (
                r.StationList.FirstOrDefault(station => station.Id == firstGuid) != null && r.StationList.FirstOrDefault(station => station.Id == secondGuid) != null));
            return spisachek;
        }
    }   
}
