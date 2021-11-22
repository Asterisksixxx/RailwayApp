using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RailwayApp.Data;
using RailwayApp.Models;

namespace RailwayApp.Services
{
   public interface ITrainService
    {
        Task<IEnumerable<Train>> GetAsync();
        Task<Train> GetAsync(Guid id);
        Task Delete(Guid id);
        Task CreateAsync(Train train);
        Task UpdateAsync(Train train);
    }

   public class TrainService:ITrainService
   {
       private readonly AppDataContext _appDataContext;

       public TrainService(AppDataContext appDataContext)
       {
           _appDataContext = appDataContext;
       }
       public async Task<IEnumerable<Train>> GetAsync()
       {
           return await Task.Run(() => _appDataContext.Trains);
       }

       public async Task<Train> GetAsync(Guid id)
       {
           return await Task.Run(() => _appDataContext.Trains.FirstOrDefaultAsync(o => o.Id == id));
       }

       public Task Delete(Guid id)
       {
           return Task.Run(() => _appDataContext.Trains.Remove(_appDataContext.Trains.FirstOrDefault(o => o.Id == id)));
       }

       public async Task CreateAsync(Train train)
       {
           await Task.Run(() => _appDataContext.Trains.AddAsync(train));
           await _appDataContext.SaveChangesAsync();
       }

       public async Task UpdateAsync(Train train)
       {
           _appDataContext.Trains.Update(train);
           await _appDataContext.SaveChangesAsync();
       }
    }
}
