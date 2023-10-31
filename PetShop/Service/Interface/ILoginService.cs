using PetShop.Models.Login;

namespace WebApiTutorialHE.Service.Interface
{
    public interface ILoginService
    {
        Task<LoginSuccessModel> Login(LoginModel loginModel);
    }
}
