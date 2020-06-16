using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaOrder.Data.Entities
{
    [Table("Category", Schema = "production")]
    public partial class Category
    {
        public Category()
        {
            FavoredStore = new HashSet<FavoredStore>();
            InverseUpNavigation = new HashSet<Category>();
            Product = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int? Up { get; set; }
        public string Image { get; set; }
        [Column("StoreCategory_id")]
        public int? StoreCategoryId { get; set; }

        [ForeignKey(nameof(StoreCategoryId))]
        [InverseProperty("Category")]
        public virtual StoreCategory StoreCategory { get; set; }
        [ForeignKey(nameof(Up))]
        [InverseProperty(nameof(Category.InverseUpNavigation))]
        public virtual Category UpNavigation { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<FavoredStore> FavoredStore { get; set; }
        [InverseProperty(nameof(Category.UpNavigation))]
        public virtual ICollection<Category> InverseUpNavigation { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
