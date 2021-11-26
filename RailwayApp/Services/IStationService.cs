using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RailwayApp.Data;
using RailwayApp.Models;

namespace RailwayApp.Services
{
   public interface IStationService
    {
        Task<IEnumerable<Station>> GetAsync();
        Task<Station> GetAsync(Guid id);
        void Delete(Guid id);
        Task CreateAsync(Station station);
        Task UpdateAsync(Station station);
    }

   public class StationService:IStationService
   {
       private readonly AppDataContext _appDataContext;

       public StationService(AppDataContext appDataContext)
       {
           _appDataContext = appDataContext;
       }
       public async Task<IEnumerable<Station>> GetAsync()
       {
           return await Task.Run(() => _appDataContext.Stations);
       }

       public async Task<Station> GetAsync(Guid id)
       {
           return await Task.Run(() => _appDataContext.Stations.FirstOrDefaultAsync(o => o.Id == id));
       }

       public void Delete(Guid id)
       {
            _appDataContext.Stations.Remove(_appDataContext.Stations.FirstOrDefault(o => o.Id == id));
           _appDataContext.SaveChanges(); 
       }

       public async Task CreateAsync(Station station)
       {
           await Task.Run(() => _appDataContext.Stations.AddAsync(station));
           await _appDataContext.SaveChangesAsync();
       }

       public async Task UpdateAsync(Station station)
       {
           _appDataContext.Stations.Update(station);
           await _appDataContext.SaveChangesAsync();
       }
    }
}
