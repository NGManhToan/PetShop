using PetShop.Action.Interface;
using PetShop.Database;
using PetShop.Database.SharingModels;
using PetShop.Models.Register;
using PetShop.Models.UtilsProject;

namespace PetShop.Action
{
    public class RegisterAction : IRegisterAction
    {
        private readonly SharingContext _context;
        public RegisterAction(SharingContext context)
        {
            _context = context;
        }

        public async Task<TblUser> Register(RegisterAccountModel account)
        {
            var repeat = Encryptor.SHA256Encode(account.RepeatPassword.Trim());

            var user = new TblUser
            {
                Email = account.Email,
                Password = Encryptor.SHA256Encode(account.Password.Trim()),
                FullName = account.FullName,
                RoleId = 2,
                CreatedDate = Utils.DateNow(),
                LastModifiedDate = Utils.DateNow(),
            };
            if (user.Password.CompareTo(repeat) != 0)
            {
                return null;

            }
            _context.TblUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
