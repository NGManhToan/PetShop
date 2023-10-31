namespace PetShop.Database.SharingModels
{
    public partial class TblBlog
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public string BlogContent { get; set; } = null!;
        public int? BlogCategoryId { get; set; }
        public int UserId { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblBlogCategory? BlogCategory { get; set; }
        public virtual TblUser User { get; set; } = null!;
    }
}
