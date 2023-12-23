using PetShop.Database.SharingModels;
using PetShop.Models.Login;
using PetShop.Models.Register;

namespace PetShop.Service.Interface
{
    public interface IRegisterAccountService
    {
        Task<TblUser> RegisterAccount(RegisterAccountModel accountRegister);
       Task ChangePassword(string userId, ChangePassModel changePasswordModel);
    }
}
