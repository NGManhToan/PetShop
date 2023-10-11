using PetShop.Models.Login;

namespace PetShop.Query.Interface
{
    public interface ILoginQuery
    {
        Task<LoginSuccessModel> Login(LoginModel loginModel, string password);
    }
}
