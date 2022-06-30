using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime Created { get; set; }

    public DateTime? LastModified { get; set; }
}