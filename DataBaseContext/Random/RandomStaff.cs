using Bogus;
using Bogus.DataSets;
using DataBaseContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseContext.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.random
{
   public static class RandomStaff
   {
      static public Entities.Staff Generate()
      {
         var faker = new Faker("pl");
         var staff = new Entities.Staff();
         staff.Login = faker.Person.UserName;
         staff.Password = Encryption.ComputeHash(faker.Random.String(minLength: 8, maxLength: 15, minChar: '9', maxChar: 'z'), "SHA512", null);
         string[] roles = { "Employee", "Manager", "Owner" };
         staff.Role = faker.PickRandom(roles);
         staff.Name = faker.Person.FirstName;
         staff.Last_Name = faker.Person.LastName;
         staff.Pesel = faker.Random.Replace("###########").ToString();
         staff.Email = faker.Person.Email;
         staff.Creation_Date = DateTime.Now;
         staff.Deletion_Date = null;
         Entities.Address address = RandomAddress.Generate();
         if (DataBaseQuery.AddAddressToDataBase(address))
         {
            staff.Id_Address = address.Id;
         }
         else
         {
            throw new Exception("failed to add new address for restaurant");
         }
         if (staff.Role == "Owner")
         {
            Entities.Restaurant Restaurant = RandomRestaurant.Generate();
            if (DataBaseQuery.AddRestaurantToDataBase(Restaurant))
               staff.Id_Restaurant = Restaurant.Id;

            else
               throw new Exception("failed to add new restaurant for owner");
         }
         else
         {
            using (var db = new AppDbContext())
            {
               staff.Id_Restaurant = (from r in db.Restaurants
                                      orderby r.Id
                                      select r.Id).Last();
            }
         }
         return staff;
      }

   }
}
