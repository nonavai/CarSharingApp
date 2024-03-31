using AutoMapper;
using CarSharingApp.Identity.BusinessLogic.Mapping;
using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Models.UserInfo;
using CarSharingApp.Identity.BusinessLogic.Services.Implementations;
using CarSharingApp.Identity.DataAccess.Entities;
using CarSharingApp.Identity.DataAccess.Repositories;
using CarSharingApp.Identity.DataAccess.Specifications;
using CarSharingApp.Identity.Shared.Exceptions;
using CarSharingApp.Identity.Tests.Generators;
using CarSharingApp.Identity.Tests.TestData;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharingApp.Identity.Tests.Services
{
    public class UserManageServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IUserInfoRepository> _userInfoRepositoryMock;
        private readonly UserManageService _userManageService;

        public UserManageServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfileApi>();
            });
            _mapper = configuration.CreateMapper();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userInfoRepositoryMock = new Mock<IUserInfoRepository>();
            _userManageService = new UserManageService(_userRepositoryMock.Object, _mapper, _userInfoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_UserExists_ReturnsUserDto()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(DataExamples.User.Id)).ReturnsAsync(DataExamples.User);

            // Act
            var result = await _userManageService.GetByIdAsync(DataExamples.User.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDto>(result);
            Assert.Equal(DataExamples.User.Id, result.Id);
        }

        [Fact]
        public async Task GetByNameAsync_UsersExist_ReturnsUserCleanDtos()
        {
            // Arrange
            var firstName = DataExamples.User.FirstName;
            var lastName = DataExamples.User.LastName;
            var users = new List<User> { DataExamples.User };
            _userRepositoryMock.Setup(repo => repo.GetBySpecAsync(It.IsAny<UserSpecification>(), default)).ReturnsAsync(users);

            // Act
            var result = await _userManageService.GetByNameAsync(firstName, lastName);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateAsync_UserExists_ReturnsUpdatedUserNecessaryDtoWithChangedName()
        {
            // Arrange
            var user = DataExamples.User;
            var name = user.FirstName;
            var userNecessaryDto = new UserNecessaryDto
            {
                FirstName = "blabla"
            };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(user.Id)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).ReturnsAsync(value: new IdentityResult());

            // Act
            var result = await _userManageService.UpdateAsync(user.Id, userNecessaryDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserNecessaryDto>(result);
            Assert.NotEqual(result.FirstName, name);
        }

        [Fact]
        public async Task DeleteAsync_UserExists_ReturnsUserCleanDto()
        {
            // Arrange
            var user = DataExamples.User;
            var userId = user.Id;
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<User>())).ReturnsAsync(new IdentityResult());

            // Act
            var result = await _userManageService.DeleteAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserCleanDto>(result);
        }

        [Fact]
        public async Task DeleteUserInfoAsync_UserInfoExists_ReturnsUserInfoCleanDto()
        {
            // Arrange
            
            var userInfo = DataExamples.UserInfo;
            _userInfoRepositoryMock.Setup(repo => repo.GetByUserIdAsync(userInfo.Id, default)).ReturnsAsync(userInfo);

            // Act
            var result = await _userManageService.DeleteUserInfoAsync(userInfo.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserInfoCleanDto>(result);
            Assert.Equal(userInfo.LicenceId, result.LicenceId);
        }

        [Fact]
        public async Task DeleteUserInfoAsync_UserInfoNotExists_ThrowsNotFoundException()
        {
            // Arrange
            var userInfoId = "nonExistingId";
            _userInfoRepositoryMock.Setup(repo => repo.GetByUserIdAsync(userInfoId, default)).ReturnsAsync((UserInfo)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _userManageService.DeleteUserInfoAsync(userInfoId));
        }
    }
}
