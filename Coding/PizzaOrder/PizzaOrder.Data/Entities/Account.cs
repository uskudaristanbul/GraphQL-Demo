using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrder.Data.Entities
{
    [Table("Account", Schema = "dbo")]
    public partial class Account
    {
        public Account()
        {
            Career = new HashSet<Career>();
        }

        [Key]
        [Column("Account_id")]
        public int AccountId { get; set; }
        public int? AccountType { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Balance { get; set; }

        [InverseProperty("Account")]
        public virtual ICollection<Career> Career { get; set; }
    }
}
