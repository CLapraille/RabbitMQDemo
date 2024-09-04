using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace Model.Data;

public class DataGenerator
{
    private readonly Faker<UserInfoModel> _userModelFake;

    public DataGenerator()
    {
        Randomizer.Seed = new Random(123);

        _userModelFake = new Faker<UserInfoModel>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.Age, f => f.Random.Int(10,80))
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.CreditRating, f => f.PickRandom<CreditRating>());
    }

    public UserInfoModel GeneteUserInfo()
    {
        return _userModelFake.Generate();
    }
}
