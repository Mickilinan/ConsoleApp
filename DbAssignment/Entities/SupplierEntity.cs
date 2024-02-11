

namespace DbAssignment.Entities;

public partial class SupplierEntity
{
    public int Id { get; set; }

    public string SupplierName { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;
}
