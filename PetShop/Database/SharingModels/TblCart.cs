using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblCart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public virtual TblProduct? Product { get; set; }
        public virtual TblUser? User { get; set; }
    }
}
