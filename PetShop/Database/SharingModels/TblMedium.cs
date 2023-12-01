using System;
using System.Collections.Generic;

namespace PetShop.Database.SharingModels
{
    public partial class TblMedium
    {
        public int IdMedia { get; set; }
        public int? PetId { get; set; }
        public int? ProductId { get; set; }
        public string? ImageMedia { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }

        public virtual TblPet? Pet { get; set; }
        public virtual TblProduct? Product { get; set; }
    }
}
