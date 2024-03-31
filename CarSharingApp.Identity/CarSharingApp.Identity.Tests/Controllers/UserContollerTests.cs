using CarSharingApp.Identity.API.Controllers;
using CarSharingApp.Identity.BusinessLogic.Models.User;
using CarSharingApp.Identity.BusinessLogic.Services;
using CarSharingApp.Identity.Tests.TestInitialiers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarSharingApp.Identity.Tests.Controllers
{
    public class UserContollerTests
    {
        private Mock<IUserManageService> _userServiceMock;

        public UserContollerTests(Mock<IUserManageService> userServiceMock, TestDataInitializer testDataInitializer)
        {
            _userServiceMock = userServiceMock;
            
        }

        public async Task GetAsync_Authorized_ReturnsOkWithUserData()
        {
           
        }
    }
}
