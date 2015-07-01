using Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.ServiceContracts
{
    public interface IAuthenticationService
    {
        User GetUserByUsername(string username);

        bool ValidateUser(string username, string password);

        void RegisterUser(User user);
    }
}
