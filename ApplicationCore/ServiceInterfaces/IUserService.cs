using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);

        Task<UserLoginResponseModel> ValidateUser(string email, string password);

        Task<User> GetUser(string email);
        Task<IEnumerable<MovieCardResponseModel>> GetPurchaseByUserId(int userId);

        Task<IEnumerable<MovieCardResponseModel>> GetFavoriteByUserId(int userId);

        Task<UserDetailResponseModel> UserDetailById(int id);

        //Task<UserEditRequestModel> UpdateUser(UserEditRequestModel model);

    }
}
