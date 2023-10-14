using PetShop.Database.SharingModels;
using PetShop.Models.Register;

namespace PetShop.Service.Interface
{
    public interface IRegisterAccountService
    {
        Task<TblUser> RegisterAccount(RegisterAccountModel accountRegister);
    }
}
