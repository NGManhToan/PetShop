namespace PetShop.Database.SharingModels
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int CustomerId { get; set; }
        public int? UserId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContactInfo { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
