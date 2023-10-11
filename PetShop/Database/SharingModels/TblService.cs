using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblService
    {
        public TblService()
        {
            TblAppointments = new HashSet<TblAppointment>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }
        public decimal? ServicePrice { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual ICollection<TblAppointment> TblAppointments { get; set; }
    }
}
