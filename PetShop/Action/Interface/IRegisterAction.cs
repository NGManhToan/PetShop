using PetShop.Database.SharingModels;
using PetShop.Models.Login;
using PetShop.Models.Register;

namespace PetShop.Action.Interface
{
    public interface IRegisterAction
    {
        Task<TblUser> Register(RegisterAccountModel account);
        Task ChangePassword(string userId, ChangePassModel changePasswordModel);
    }
}
