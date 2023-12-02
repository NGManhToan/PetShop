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
						Email = checkoutRequest.InfoUser.Email,
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
		public async Task Checkout(CheckoutRequestDto checkoutRequest, string userId)
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
						.Select(x => new
						{
							x.Email,
							x.UserId
						})
						.FirstOrDefaultAsync();

					TblCustomer customer = new TblCustomer
					{
						CustomerName = checkoutRequest.InfoUser.FullName,
						CustomerContactInfo = checkoutRequest.InfoUser.Customer_Contact_Info,
						PhoneNumber = checkoutRequest.InfoUser.PhoneNumber,
						Email = userID.Email,
						UserId = userID.UserId,
						CreatedBy = userID.UserId,
						LastModifiedBy = userID.UserId,
						IsActive = 1,
						IsDeleted = 0,
					};
					_context.TblCustomers.Add(customer);
					await _context.SaveChangesAsync();

					foreach (var item in checkoutRequest.CartItems)
					{
						var cartDetailId = await _context.TblCartDetails
							.Where(x => x.ProductId == item.Id)
							.Select(x => x.CartDetailId)
							.FirstOrDefaultAsync();

						var cartDetailInfo = await _context.TblCartDetails
							.Where(x => x.CartDetailId == cartDetailId)
							.Select(x => new
							{
								Name = x.Name,
								Image = x.Image
							})
							.FirstOrDefaultAsync();

						TblOrder tblOrder = new TblOrder
						{
							CustomerId = customer.CustomerId,
							TotalAmount = item.Price,
							OrderDate = Utils.DateNow(),
							PhoneNumber = checkoutRequest.InfoUser.PhoneNumber,
							OrderStatus = "Đang chờ xử lý",
							IsActive = 1,
							IsDeleted = 0,
							LastModifiedBy = customer.CustomerId,
							CartDetailId = cartDetailId,
							CreatedBy = customer.CustomerId,
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

                        var carts = await _context.TblCarts
						.Where(x => x.UserId == parseUserId && x.IsDeleted == 0 && x.IsActive == 1)
						.ToListAsync();

                        foreach (var cart in carts)
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
