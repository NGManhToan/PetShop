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
			try
			{
				using (var transaction = _context.Database.BeginTransaction())
				{

					TblCustomer customer = new TblCustomer
					{
						CustomerName = checkoutRequest.InfoUser.FullName,
                        CustomerContactInfo = checkoutRequest.InfoUser.Customer_Contact_Info,
						PhoneNumber = checkoutRequest.InfoUser.PhoneNumber,
						IsActive = 1,
						IsDeleted = 0,
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
							OrderItemname = item.Name,
							OrderImg = item.Image,

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
		public async Task Checkout(CheckoutRequestDto checkoutRequest,string userId)
		{
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }
			int parseUserId = int.Parse(userId);
            try
			{
				using (var transaction = _context.Database.BeginTransaction())
				{
					var userID = await _context.TblUsers
						.Where(x => x.UserId == parseUserId)
						.Select(x => x.UserId).FirstOrDefaultAsync();


                    var cartId = await _context.TblCarts
						.Where(x=> x.UserId == parseUserId && x.IsDeleted == 0 && x.IsActive == 1)
						.Select(x=>x.CartId).ToListAsync();


                    var cartDetailIds = await _context.TblCartDetails
						 .Where(x => cartId.Contains(x.CartId)) // Check if CartId is in the list
						 .Select(x => x.CartDetailId)
						 .ToListAsync();


                    var cartDetailInfo = await _context.TblCartDetails
						.Where(x => cartDetailIds.Contains(x.CartDetailId))
							.Select(x => new
							{
								Name = x.Name,
								Image = x.Image
							})
							.FirstOrDefaultAsync();


                    var updateCarts = await _context.TblCarts
						.Where(x => cartId.Contains(x.CartId))
						.ToListAsync();
                    foreach (var item in checkoutRequest.CartItems)
					{
						TblOrder tblOrder = new TblOrder
						{
                            TotalAmount = item.Price,
							OrderDate = Utils.DateNow(),
							PhoneNumber = checkoutRequest.InfoUser.PhoneNumber,
                            OrderStatus = "Đang chờ xử lý",
                            IsActive = 1,
                            IsDeleted = 0,
                            LastModifiedBy = userID,
							CartDetailId = cartDetailIds.FirstOrDefault(),
							CreatedBy = userID,
                            OrderImg = cartDetailInfo.Image,
							OrderItemname = cartDetailInfo.Name
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

                        foreach (var cart in updateCarts)
                        {
                            cart.IsDeleted = 1;
                            _context.TblCarts.Update(cart);
                        }
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
