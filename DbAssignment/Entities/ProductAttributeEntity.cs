using System.ComponentModel.DataAnnotations.Schema;

namespace DbAssignment.Entities;

public partial class ProductAttributeEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; } = null!;

    public string AttributeName { get; set; } = null!;
}
