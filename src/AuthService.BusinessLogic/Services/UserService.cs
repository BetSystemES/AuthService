using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }

        public Task<UserSimpleModel> CreateUser(CreateUserModel model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserSimpleModel> GetUserSimpleModel(Guid userId, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
