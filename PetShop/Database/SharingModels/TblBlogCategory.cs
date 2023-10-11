using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblBlogCategory
    {
        public TblBlogCategory()
        {
            TblBlogs = new HashSet<TblBlog>();
        }

        public int BlogCategoryId { get; set; }
        public string? BlogCategoryName { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual ICollection<TblBlog> TblBlogs { get; set; }
    }
}
