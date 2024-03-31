using Bogus;
using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;
using CarSharingApp.Identity.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarSharingApp.Identity.Tests.Generators
{
    public class UserGenerator
    {
        public UserDto GenerateUserDto()
        {
            var passwordRegex = @"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\da-zA-Z]).{10,20}$";

            var userDtoFaker = new Faker<UserDto>()
                .RuleFor(u => u.Id, f => f.Random.Guid().ToString())
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Password, f => f.Internet.Password(11, false, passwordRegex))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("+375#########"))
                .RuleFor(u => u.RecordNumber, f => f.Random.AlphaNumeric(8))
                .RuleFor(u => u.UserName, f => f.Person.UserName);

            return userDtoFaker.FinishWith((Faker f, UserDto userDto) =>
            {
                userDto.UserInfo = GenerateUserInfo(userDto.Id).Generate();
            }).Generate();
        }
        public static Faker<UserInfoDto> GenerateUserInfo(string id)
        {
            return new Faker<UserInfoDto>()
                .RuleFor(u => u.Id, f => f.Random.Guid().ToString())
                .RuleFor(u => u.UserId, id)
                .RuleFor(u => u.Birth, f => f.Date.Past())
                .RuleFor(u => u.Country, f => f.Address.Country())
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Category, f => f.PickRandom<LicenceType>())
                .RuleFor(u => u.LicenceExpiry, f => f.Date.Future())
                .RuleFor(u => u.LicenceIssue, f => f.Date.Past())
                .RuleFor(u => u.LicenceId, f => f.Random.AlphaNumeric(8))
                .RuleFor(u => u.PlaceOfIssue, f => f.Address.City());
        }
    }
}
