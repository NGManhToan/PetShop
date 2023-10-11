using PetShop.Models.Login;
using PetShop.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface ILoginService
    {
        Task<LoginSuccessModel> Login(LoginModel loginModel);
    }
}
