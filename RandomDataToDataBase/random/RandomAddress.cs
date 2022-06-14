using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace RandomDataToDataBase.random
{
    public static class RandomAddress
    {
        static public Entities.Address Generate()
        {
            var faker = new Faker("pl");
            var add = new Entities.Address();
            add.City = faker.Address.City();
            add.Zip_Code = faker.Random.Replace("##-###").ToString();
            add.Street = faker.Address.StreetName().OrNull(faker,.1f);
            add.House_Number = faker.Random.Number(0, 150).ToString();
            add.Apartment_Number = faker.Random.Number(0, 50).ToString().OrNull(faker, .7f);
            string[] House_NumberPostFixs = { "a", "b", "c", "d" };
            if (faker.Random.Number(0, 10) < 2) add.House_Number += faker.PickRandom(House_NumberPostFixs);
            return add;
        }

    }
}
