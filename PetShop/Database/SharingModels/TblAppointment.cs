using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblAppointment
    {
        public int AppointmentId { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblService Service { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
    }
}
