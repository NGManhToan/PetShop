using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblProductCategory
    {
        public TblProductCategory()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
