﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrder.Data.Entities
{
    [Table("PostCodeSector", Schema = "tekyerco_kozmi")]
    public partial class PostCodeSector
    {
        public PostCodeSector()
        {
            Postcode = new HashSet<Postcode>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("PostCodeSector")]
        [StringLength(10)]
        public string PostCodeSector1 { get; set; }
        [Column("PostCodeArea_id")]
        public int? PostCodeAreaId { get; set; }

        [InverseProperty("Sector")]
        public virtual ICollection<Postcode> Postcode { get; set; }
    }
}
