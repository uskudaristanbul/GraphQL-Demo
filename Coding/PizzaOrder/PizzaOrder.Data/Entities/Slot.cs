using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrder.Data.Entities
{
    [Table("Slot", Schema = "dbo")]
    public partial class Slot
    {
        [Key]
        [Column("Slot_id")]
        public int SlotId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StotStart { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SlotEnd { get; set; }
        [Column("Store_id")]
        public int? StoreId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SlotDay { get; set; }
        public TimeSpan? SlotStartTime { get; set; }
        public TimeSpan? SlotEndTime { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Price { get; set; }

        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Slot")]
        public virtual Store Store { get; set; }
    }
}
