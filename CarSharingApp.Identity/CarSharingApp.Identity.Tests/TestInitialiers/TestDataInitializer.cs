using CarSharingApp.Identity.BusinessLogic.Services;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Repositories;
using CarSharingApp.Identity.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CarSharingApp.Identity.Tests.TestInitialiers
{
    public class TestDataInitializer
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserInfoRepository _userInfoRepository;

        public TestDataInitializer(IUserRepository userRepository, IUserInfoRepository userInfoRepository)
        {
            _userRepository = userRepository;
            _userInfoRepository = userInfoRepository;
        }
        public async Task<User> InitializeUserWithRoles()
        {
            var cancellationToken = new CancellationToken();
            await _userRepository.AddAsync(new DataAccess.Entities.User
            {
                FirstName = "anton",
                LastName = "borodich",
                Email = "newEmail@gmail.com",
                PhoneNumber = "+375293322222",
                RecordNumber = "12313131231313",
                UserName = "nonavai"
            },
            "123qweQWE#");
            var user = await _userRepository.GetByEmailAsync("newEmail@gmail.com");
            var userInfo = await _userInfoRepository.AddAsync(new DataAccess.Entities.UserInfo {
                User = user,
                Birth = new DateTime(2004, 06, 13),
                Country = "Poland",
                Category = Shared.Enums.LicenceType.B,
                LicenceExpiry = new DateTime(2026, 05, 12),
                LicenceIssue = new DateTime(2022, 05, 12),
                LicenceId = "adad1231d13a1dad",
                PlaceOfIssue = "sjskfkjshshsjksjshhsfkskjsdfhskjfkshfksfjks",
            });
            await _userRepository.AddToRoleAsync(user, RoleNames.Borrower);
            await _userRepository.AddToRoleAsync(user, RoleNames.Lender);
            await _userRepository.AddToRoleAsync(user, RoleNames.Admin);
            var updatedUser = await _userRepository.GetByIdAsync(user.Id);

            return updatedUser!;
        }
    }
}
