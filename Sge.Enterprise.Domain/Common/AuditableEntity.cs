namespace Sge.Enterprise.Domain.Common
{
    public abstract class AuditableEntity
    {
        public int StatusId { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}