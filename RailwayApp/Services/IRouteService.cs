using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RailwayApp.Data;
using RailwayApp.Models;
using RailwayApp.ViewModels;

namespace RailwayApp.Services
{
    public interface IRouteService
    {
        Task<IEnumerable<Route>> GetAsync();
        Task<Route> GetAsync(Guid id);
        Task Delete(Guid id);
        Task CreateAsync(Route route);
        Task CreateAsync(Route_View routeView);
        Task UpdateAsync(Route route);

    }

    public class RouteService : IRouteService
    {
        private readonly AppDataContext _appDataContext;
        private readonly ITrainService _trainService;
        private readonly IStationService _stationService;

        public RouteService(AppDataContext appDataContext, ITrainService trainService, IStationService stationService)
        {
            _appDataContext = appDataContext;
            _trainService = trainService;
            _stationService = stationService;
        }
        public async Task<IEnumerable<Route>> GetAsync()
        {
            return await Task.Run(() => _appDataContext.Routes);
        }

        public async Task<Route> GetAsync(Guid id)
        {
            return await Task.Run(() => _appDataContext.Routes.FirstOrDefaultAsync(o => o.Id == id));
        }

        public Task Delete(Guid id)
        {
            return Task.Run(() => _appDataContext.Routes.Remove(_appDataContext.Routes.FirstOrDefault(o => o.Id == id)));
        }

        public async Task CreateAsync(Route route)
        {
            await Task.Run(() => _appDataContext.Routes.AddAsync(route));
            await _appDataContext.SaveChangesAsync();
        }
        public async Task CreateAsync(Route_View routeView)
        {
            Route route = new Route()
            {
                Id = routeView.Id,
                Name = routeView.Name,
                StartRouteDateTime = routeView.StartRouteDateTime,
                EndRouteDateTime = routeView.EndRouteDateTime,
                StationList = new List<Station>(),
                TrainsList = new List<Train>(),
            };
            foreach (var trainid in routeView.TrainsList)
            {
                route.TrainsList.Add(await _trainService.GetAsync(trainid));
            }   
            foreach (var stationid in routeView.StationList)
            {
                route.StationList.Add(await _stationService.GetAsync(stationid));
            }
            await Task.Run(() => _appDataContext.Routes.AddAsync(route));
            await _appDataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Route route)
        {
            _appDataContext.Routes.Update(route);
            await _appDataContext.SaveChangesAsync();
        }
    }
}
