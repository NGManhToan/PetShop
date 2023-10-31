namespace PetShop.Database.SharingModels
{
    public partial class TblPetCategory
    {
        public TblPetCategory()
        {
            TblPets = new HashSet<TblPet>();
        }

        public int PetCategoryId { get; set; }
        public string PetCategoryName { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual ICollection<TblPet> TblPets { get; set; }
    }
}
