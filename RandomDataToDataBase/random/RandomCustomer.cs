using Bogus;
using Bogus.DataSets;
using RandomDataToDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDataToDataBase.random
{
    public static class RandomCustomer
    {
        static public Entities.Customer Generate()
        {
            var faker = new Faker("pl");
            var customer = new Entities.Customer();

            customer.Email = faker.Person.Email;
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7','8','9',
                'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M' };
            for (int i = 0; i < 8; i++)
            {
                customer.Code += faker.PickRandom(chars);
            }

            return customer;
        }

    }
}
