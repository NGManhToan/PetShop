using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }
        public string? ProductImageUrl { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Discount { get; set; }

        public virtual TblProductCategory? Category { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
