namespace Domain.Common;

public class BaseAuditableEntity : BaseEntity
{
    public string CreateUser { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public string UpdateUser { get; set; } = string.Empty;
    public DateTime? UpdateDate { get; set; }
}
