using System.ComponentModel.DataAnnotations.Schema;

namespace DbAssignment.Entities;

public partial class ManufacturerEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public ProductEntity Product { get; set; } = null!;

    public string ManufacturerName { get; set; } = null!;
}
