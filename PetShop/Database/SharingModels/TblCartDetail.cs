using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblCartDetail
    {
        public TblCartDetail()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public decimal Quanlity { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal? Total { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblCart Cart { get; set; } = null!;
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
