namespace PetShop.Database.SharingModels
{
    public partial class TblOrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? PetId { get; set; }
        public int? Quantity { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblOrder? Order { get; set; }
        public virtual TblPet? Pet { get; set; }
        public virtual TblProduct? Product { get; set; }
    }
}
