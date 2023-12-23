using PetShop.Action.Interface;
using PetShop.Database.SharingModels;
using PetShop.Models.Login;
using PetShop.Models.Register;
using PetShop.Service.Interface;

namespace PetShop.Service
{
    public class RegisterAccountService : IRegisterAccountService
    {
        private readonly IRegisterAction _registerAction;
        public RegisterAccountService(IRegisterAction registerAction)
        {
            _registerAction = registerAction;
        }

        public async Task<TblUser> RegisterAccount(RegisterAccountModel registerAccount)
        {
            var account = await _registerAction.Register(registerAccount);
            return account;
        }

        public async Task ChangePassword(string userId, ChangePassModel changePasswordModel)
        {
             await _registerAction.ChangePassword(userId, changePasswordModel);
        }
    }
}
