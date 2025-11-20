using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using LibraryManagementAPIInterface;
using LibraryManagementAPIModel;
using LibraryManagementAPIModel.DTO;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace API_NexusConcrete
{
    public class AuthConcrete : IAuthenticate
    {
        private readonly LibraryDbContext _context;

        private readonly IConfiguration _config;
        public AuthConcrete(LibraryDbContext context, IConfiguration config)
        {
            this._context = context;
            _config = config;
        }

        public bool AddNewUser(LoginViewModel model)
        {
            try
            {
                MstUserModel mstUser = new MstUserModel();

                if (model.Password != null)
                {
                    var password = HashPassword(model.Password);
                    model.Password = password;
                }

                mstUser.UserName = model.UserName;
                mstUser.Password = model.Password;
                mstUser.EmailId = model.UserName;

                mstUser.PhoneNumber = model.PhoneNumber;

                mstUser.IsActive = true;
                mstUser.IsDeleted = false;
                mstUser.AddedOn = DateTime.UtcNow;
                var save = _context.Add(mstUser);
                var res = _context.SaveChanges();
                if (res > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AutneticateUser(LoginViewModel model)
        {
            ResponseModel response = new ResponseModel();
            string result = string.Empty;

            try
            {
                var user = (from log in _context.MstUsers
                            where log.EmailId.ToLower() == model.UserName.ToLower()
                            select log).FirstOrDefault();

                if (user != null)
                {
                    bool isPasswordValid = VerifyPassword(model.Password, user.Password);
                    if (isPasswordValid)
                    {
                        (string token, string refresh) = this.GenerateJsonWebToken(user.UserId, user.UserName, user.ClientId);

                        var Dt = new
                        {
                            AccessToken = token,
                            RefreshToken = refresh
                        };

                        response.Message = "Login Successful";
                        response.StatusCode = 200;
                        response.IsSuccess = true;
                        response.Data = Dt;
                    }
                    else
                    {
                        response.Message = "Unauthorized - Incorrect password!";
                        response.IsSuccess = false;
                        response.StatusCode = 401;
                    }
                }
                else
                {
                    response.Message = "Unauthorized - User not found!";
                    response.IsSuccess = false;
                    response.StatusCode = 401;
                }
            }
            catch (Exception ex)
            {
                response.Message = "An error occurred: " + ex.Message;
                response.IsSuccess = false;
                response.StatusCode = 500;
            }
            finally
            {
                result = JsonConvert.SerializeObject(response);
            }

            return result;
        }

        public (string AccessToken, string RefreshToken) GenerateJsonWebToken(Guid UserId, string Username, Guid ClientId)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
                var Credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                      new Claim(JwtRegisteredClaimNames.Name,Username),
                      new Claim(JwtRegisteredClaimNames.Email,Username),
                      new Claim("UserId",UserId.ToString()),
                      new Claim("ClientId",ClientId.ToString()),
                      new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };


                var token = new JwtSecurityToken(
                               _config["Jwt:Issuer"],
                               _config["Jwt:Audience"],
                               claims,
                               expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpirationTime"])),
                               signingCredentials: Credential
                             );

                var refreshtoken = new JwtSecurityToken(
                              _config["Jwt:Issuer"],
                              _config["Jwt:Audience"],
                              claims: null,
                              expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:RefeshTime"])),
                              signingCredentials: Credential
                            );

                return (new JwtSecurityTokenHandler().WriteToken(token), new JwtSecurityTokenHandler().WriteToken(refreshtoken));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CheckUserExist(string EmailId)
        {
            try
            {
                var checkUser = (from chk in _context.MstUsers where chk.UserName.ToLower() == EmailId.ToLower() && chk.IsActive == true && chk.IsDeleted == false select chk).Any();

                return checkUser;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHashedPassword;
        }
    }
}


