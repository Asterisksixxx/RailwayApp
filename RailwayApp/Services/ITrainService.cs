﻿using System;
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
        void Delete(Guid id);
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

       public void Delete(Guid id)
       {
           _appDataContext.Trains.Remove(_appDataContext.Trains.FirstOrDefault(o => o.Id == id));
           _appDataContext.SaveChanges();
       }

       public async Task CreateAsync(Train train)
       {
           for (var i = 1; i <= train.SeatsCount; i++)
           {
               train.AvailableSeats += $"{i} ";
           }
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
