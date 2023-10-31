namespace PetShop.Database.SharingModels
{
    public partial class TblPet
    {
        public TblPet()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        public int PetId { get; set; }
        public string PetName { get; set; } = null!;
        public int PetCategoryId { get; set; }
        public int VendorId { get; set; }
        public string? PetImages { get; set; }
        public short? PetStatus { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public decimal? PetPrice { get; set; }

        public virtual TblPetCategory PetCategory { get; set; } = null!;
        public virtual TblVendor Vendor { get; set; } = null!;
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
