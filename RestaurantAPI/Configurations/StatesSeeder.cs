using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Configurations
{
    public static class StatesSeeder
    {
        public static void SeedStates(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .HasData
                (
                new State { StateId = 1, Name = "Abia" },
                new State { StateId = 2, Name = "Adamawa" },
                new State { StateId = 3, Name = "Akwa Ibom" },
                new State { StateId = 4, Name = "Anambra" },
                new State { StateId = 5, Name = "Bauchi" },
                new State { StateId = 6, Name = "Bayelsa" },
                new State { StateId = 7, Name = "Benue" },
                new State { StateId = 8, Name = "Borno" },
                new State { StateId = 9, Name = "Cross River" },
                new State { StateId = 10, Name = "Delta" },
                new State { StateId = 11, Name = "Ebonyi" },
                new State { StateId = 12, Name = "Edo" },
                new State { StateId = 13, Name = "Ekiti" },
                new State { StateId = 14, Name = "Enugu" },
                new State { StateId = 15, Name = "FCT" },
                new State { StateId = 16, Name = "Gombe" },
                new State { StateId = 17, Name = "Imo" },
                new State { StateId = 18, Name = "Jigawa" },
                new State { StateId = 19, Name = "Kaduna" },
                new State { StateId = 20, Name = "Kano" },
                new State { StateId = 21, Name = "Katsina" },
                new State { StateId = 22, Name = "Kebbi" },
                new State { StateId = 23, Name = "Kogi" },
                new State { StateId = 24, Name = "Kwara" },
                new State { StateId = 25, Name = "Lagos" },
                new State { StateId = 26, Name = "Nasarawa" },
                new State { StateId = 27, Name = "Niger" },
                new State { StateId = 28, Name = "Ogun" },
                new State { StateId = 29, Name = "Ondo" },
                new State { StateId = 30, Name = "Osun" },
                new State { StateId = 31, Name = "Oyo" },
                new State { StateId = 32, Name = "Plateau" },
                new State { StateId = 33, Name = "Rivers" },
                new State { StateId = 34, Name = "Sokoto" },
                new State { StateId = 35, Name = "Taraba" },
                new State { StateId = 36, Name = "Yobe" },
                new State { StateId = 37, Name = "Zamfara" }

                );
        }
    }
}
