using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblOrder
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public int? CartDetailId { get; set; }

        public virtual TblCartDetail? CartDetail { get; set; }
        public virtual TblCustomer? Customer { get; set; }
    }
}
