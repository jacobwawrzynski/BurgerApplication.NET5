using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using RandomDataToDataBase.Entities;

namespace RandomDataToDataBase.random
{
    public static class RandomRestaurant
    {
        static public Entities.Restaurant Generate()
        {
            var faker = new Faker("pl");
            var restaurant = new Entities.Restaurant();
            Address address = RandomAddress.Generate();
            if (DataBase.AddAddressToDataBase(address))
            {
                restaurant.Id_Address = address.Id;
            }
            else
            {
                throw new Exception("failed to add new address for restaurant");
            }
            return restaurant;
        }
    }
}
