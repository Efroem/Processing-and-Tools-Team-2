using System;
using System.ComponentModel.DataAnnotations;

public class ItemLine
{
    [Key]
    public int LineId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
