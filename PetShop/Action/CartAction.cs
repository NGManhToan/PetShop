using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Action.Interface;
using PetShop.Database;
using PetShop.Database.SharingModels;
using PetShop.Models.Cart;
using PetShop.Models.UtilsProject;

namespace PetShop.Action
{
    public class CartAction : ICartAction
    {
        private readonly SharingContext _context;

        public CartAction(SharingContext context)
        {
            _context = context;
        }

                public async Task CheckoutNoAcc(CheckoutRequestDto checkoutRequest)
                {
                    var ramdomPass = RamdomPass.RandomPassword();
                    try
                    {
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            // Truy cập dữ liệu từ checkoutRequest thay vì các biến global
                            int maxUserId = _context.TblUsers.Max(u => u.UserId); // Lấy ID lớn nhất hiện tại

                            TblUser user = new TblUser
                            {
                                UserId = maxUserId + 1,
                                Email = checkoutRequest.InfoUser.Email,
                                Password = Encryptor.SHA256Encode(ramdomPass),
                                FullName = checkoutRequest.InfoUser.FullName,
                            };

                            _context.TblUsers.Add(user);
                            await _context.SaveChangesAsync();

                            TblCustomer customer = new TblCustomer
                            {
                                UserId = user.UserId,
                                CustomerName = user.FullName,
                                CustomerContactInfo = checkoutRequest.InfoUser.Customer_Contact_Info,
                                PhoneNumber = checkoutRequest.InfoUser.PhoneNumber,
                                CreatedBy = user.UserId,
                                IsActive = 1,
                                IsDeleted = 0,
                                LastModifiedBy = user.UserId,
                            };

                            // Lưu thông tin người dùng và khách hàng vào cơ sở dữ liệu

                            _context.TblCustomers.Add(customer);
                            await _context.SaveChangesAsync();

                            foreach (var item in checkoutRequest.CartItems)
                            {
                                TblOrder tblOrder = new TblOrder
                                {
                                    TotalAmount = item.Price,
                                    CustomerId = customer.CustomerId,
                                    PhoneNumber = customer.PhoneNumber,
                                    CreatedBy = customer.CustomerId,
                                    OrderDate = Utils.DateNow(),
                                    OrderStatus = "Đang chờ xử lý",
                                    IsActive = 1,
                                    IsDeleted = 0,
                                    LastModifiedBy = customer.CustomerId,

                                };

                                _context.TblOrders.Add(tblOrder);
                                await _context.SaveChangesAsync();

                                TblOrderDetail tblOrderDetail = new TblOrderDetail
                                {
                                    OrderId = tblOrder.OrderId,
                                    Quantity = item.Quantity,
                                    ProductId = item.Id,
                                    CreatedBy = tblOrder.CreatedBy,
                                    IsActive = 1,
                                    IsDeleted = 0,
                                    LastModifiedBy = tblOrder.CreatedBy,
                                };
                                _context.TblOrderDetails.Add(tblOrderDetail);
                                await _context.SaveChangesAsync();
                            }

                            transaction.Commit();
                    
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.ToString());
                    }
                }
    }

}
