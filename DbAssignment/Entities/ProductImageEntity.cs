using System.ComponentModel.DataAnnotations.Schema;

namespace DbAssignment.Entities;

public partial class ProductImageEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; } = null!;

    public string ImagePath { get; set; } = null!;
}
