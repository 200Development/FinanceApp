using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }

        public int? ParentId { get;set; }
    }
}
