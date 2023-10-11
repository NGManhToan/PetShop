
using PetShop.Query;
using WebApiTutorialHE.Service.Interface;
using PetShop.Models.UtilsProject;
using PetShop.Query.Interface;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using PetShop.Models.Login;
using PetShop.Manager.Token.Interface;
using PetShop.Manager.Token;


namespace PetShop.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginQuery _loginQuery;
        public LoginService(ILoginQuery loginQuery)
        {
            _loginQuery = loginQuery;
        }
        public async Task<LoginSuccessModel> Login(LoginModel loginModel)
        {
            loginModel.Password = Encryptor.SHA256Encode(loginModel.Password);
            var login = await _loginQuery.Login(loginModel, loginModel.Password);

            if (login == null)
            {
                return null;
            }
            
            IAuthContainerModel model = Signature.GetJWTContainerModel(login.Id.ToString(), loginModel.Email, loginModel.Password, login.Roles.ToString());
            IAuthService authService = new JWTService(model.SecretKey);

            var token = authService.GenerateToken(model);

            return await _loginQuery.Login(loginModel, loginModel.Password);
        }
    }
}
