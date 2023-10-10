using Domain.Common;

namespace Domain.Entities;

public class Building : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public int Status { get; set; }
}
