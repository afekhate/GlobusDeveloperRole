
using Globus.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using static Globus.Data.DTO.Location;

namespace Globus.Data.Seeds
{
    public static class ModelBuilderExtension
    {
      
        public static void Seed(this ModelBuilder modelBuilder)
        {

            // seeding states
            string filePath = Globus.Utilities.Common.GenericUtil.FetchStateLGA();
            var json = File.ReadAllText(filePath);
            var model = JsonConvert.DeserializeObject<RootObject>(json);

            List<State> StateList = new List<State>();
            List<LGA> LgaList = new List<LGA>();
            long lgaId = 1;

            for (long i = 0; i < model.states.Count; i++)
            {
                
                var state = new State
                {
                    ID = i + 1,
                    Name = model.states[Convert.ToInt32(i)].state                   
                };
                StateList.Add(state);

               
                    for (long j = 0; j < model.states[Convert.ToInt32(i)].lgas.Count; j++)
                    {
                        var lga = new LGA
                        {
                            ID = lgaId,
                            Name = model.states[Convert.ToInt32(i)].lgas[Convert.ToInt32(j)],
                            StateId = state.ID
                        };
                        LgaList.Add(lga);
                        lgaId++;
                    }

            

            }

            modelBuilder.Entity<State>().HasData(StateList);
            modelBuilder.Entity<LGA>().HasData(LgaList);
                           
           

        
        }
    }
}
