﻿using PetShop.Action.Interface;
using PetShop.Database;
using PetShop.Database.SharingModels;
using PetShop.Models.Login;
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
            if(account == null)
            {

            }
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



        public async Task ChangePassword(string userId, ChangePassModel changePasswordModel)
        {
            var oldPass = Encryptor.SHA256Encode(changePasswordModel.CurrentPassword);
            // Chuyển đổi userId từ chuỗi sang số nguyên
            if (!int.TryParse(userId, out int userIdInt))
            {
                // Xử lý trường hợp không thể chuyển đổi userId
                return;
            }
            var user = await _context.TblUsers.FindAsync(userIdInt);

            if (user == null)
            {
                return;
            }

            // Thực hiện kiểm tra mật khẩu cũ và cập nhật mật khẩu mới
            
            if(user!=null && user.Password == oldPass && changePasswordModel.NewPassword == changePasswordModel.ConfirmNewPassword)
            {
                user.Password =Encryptor.SHA256Encode(changePasswordModel.NewPassword);
                _context.Update(user);
                await _context.SaveChangesAsync();

            }

            
        }
    }
}
