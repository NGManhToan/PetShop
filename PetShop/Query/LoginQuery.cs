using PetShop.Models.Login;
using PetShop.Query.Interface;
using PetShop.UtilsService.Interface;


namespace PetShop.Query
{
    public class LoginQuery : ILoginQuery
    {
        private readonly ISharingDapper _sharingDapper;
        public LoginQuery(ISharingDapper sharingDapper)
        {
            _sharingDapper = sharingDapper;
        }

        public async Task<LoginSuccessModel> Login(LoginModel loginModel, string password)
        {

            var query = @"SELECT u.user_id as Id,group_concat(r.role_id) Roles
                            FROM tbl_user u 
                            left join tbl_role r on r.role_id = u.role_id
                            WHERE u.email = @Email AND u.password = @password
                            GROUP BY Id";

            return await _sharingDapper.QuerySingleAsync<LoginSuccessModel>(query, new
            {
                email = loginModel.Email.Trim(),
                Password = password.Trim(),
            });
        }
    }
}
