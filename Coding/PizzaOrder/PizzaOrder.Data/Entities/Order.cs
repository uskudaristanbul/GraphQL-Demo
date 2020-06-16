using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrder.Data.Entities
{
    [Table("Order", Schema = "sales")]
    public partial class Order
    {
        public Order()
        {
            CareerOrder = new HashSet<CareerOrder>();
            Comment = new HashSet<Comment>();
            OrderItem = new HashSet<OrderItem>();
            Supplier = new HashSet<Supplier>();
        }

        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("order_status")]
        public int? OrderStatus { get; set; }
        [Column("order_date", TypeName = "date")]
        public DateTime? OrderDate { get; set; }
        [Column("required_date", TypeName = "date")]
        public DateTime? RequiredDate { get; set; }
        [Column("shipped_date", TypeName = "date")]
        public DateTime? ShippedDate { get; set; }
        [Column("store_id")]
        public int? StoreId { get; set; }
        [Column("staff_id")]
        public int? StaffId { get; set; }
        [Column("Slot_id")]
        public int? SlotId { get; set; }
        public string SignImageLink { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DeliveryTotalCost { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DeliveryFeeTaken { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FirstTimeVisited { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SecondTimeVisited { get; set; }
        public int? FirstVisitResult { get; set; }
        public int? SecondVisitResult { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DeliveryCareerCost { get; set; }
        public int? OrderType { get; set; }
        [StringLength(250)]
        public string VisitResultDescription { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? SubTotal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Total { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty("Order")]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(StaffId))]
        [InverseProperty("Order")]
        public virtual Staff Staff { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Order")]
        public virtual Store Store { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<CareerOrder> CareerOrder { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<Comment> Comment { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        [InverseProperty("RelatedOrder")]
        public virtual ICollection<Supplier> Supplier { get; set; }
    }
}
