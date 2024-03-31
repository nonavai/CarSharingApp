using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharingApp.Identity.Tests.TestData
{
    public static class DataExamples
    {
        private static User user = new()
        {
            Id = "newId",
            FirstName = "anton",
            LastName = "borodich",
            Email = "newEmail@gmail.com",
            PhoneNumber = "+375293322222",
            RecordNumber = "12313131231313",
            UserName = "nonavai"
        };

        private static UserInfo userInfo = new()
        {
            Id = "NewId",
            User = user,
            Birth = new DateTime(2004, 06, 13),
            Country = "Poland",
            Category = Shared.Enums.LicenceType.B,
            LicenceExpiry = new DateTime(2026, 05, 12),
            LicenceIssue = new DateTime(2022, 05, 12),
            LicenceId = "adad1231d13a1dad",
            PlaceOfIssue = "sjskfkjshshsjksjshhsfkskjsdfhskjfkshfksfjks",
            UserId = "newId"
        };

        public static User User
        {
            get
            {
                user.UserInfo = userInfo;
                return user;
            }

        }

        public static UserInfo UserInfo
        {
            get
            {
                return userInfo;
            }

        }

        public static List<User> UserList = new()
        {
            User,
            new User
            {
                Id = "newId2",
                FirstName = "anton",
                LastName = "borodich",
                Email = "newEmail2@gmail.com",
                PhoneNumber = "+375293322222",
                RecordNumber = "12313131231313",
                UserName = "nonavai"
            }
        };

        public static UserNecessaryDto UserNecessary = new()
        {
            FirstName = "anton",
            LastName = "borodich",
            Email = "newEmail@gmail.com",
            PhoneNumber = "+375293322222",
            RecordNumber = "12313131231313",
            UserName = "nonavai"
        };
    }
}
