using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblCart
    {
        public TblCart()
        {
            TblCartDetails = new HashSet<TblCartDetail>();
        }

        public int CartId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblCartDetail> TblCartDetails { get; set; }
    }
}
