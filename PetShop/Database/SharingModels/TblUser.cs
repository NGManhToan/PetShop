using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblAppointments = new HashSet<TblAppointment>();
            TblBlogs = new HashSet<TblBlog>();
            TblCarts = new HashSet<TblCart>();
        }

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? RoleId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public ulong? IsDeleted { get; set; }
        public ulong? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblRole? Role { get; set; }
        public virtual TblCustomer? TblCustomer { get; set; }
        public virtual ICollection<TblAppointment> TblAppointments { get; set; }
        public virtual ICollection<TblBlog> TblBlogs { get; set; }
        public virtual ICollection<TblCart> TblCarts { get; set; }
    }
}
