using PetShop.Database.SharingModels;
using PetShop.Models.Register;

namespace PetShop.Action.Interface
{
    public interface IRegisterAction
    {
        Task<TblUser> Register(RegisterAccountModel account);
    }
}
