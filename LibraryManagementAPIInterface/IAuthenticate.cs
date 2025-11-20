using System;
using LibraryManagementAPIModel.DTO;

namespace LibraryManagementAPIInterface
{
	public interface IAuthenticate
	{
        public string AutneticateUser(LoginViewModel model);
        bool CheckUserExist(string EmailId);
        (string AccessToken, string RefreshToken) GenerateJsonWebToken(Guid UserId, string Username, Guid ClientId);

    }
}

