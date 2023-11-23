using System.ComponentModel.DataAnnotations;

namespace TrainManagement.Models;

public class Train
{
    [Key]
    public long Id { get; set; }
    [Required]
    public int TrainNumber { get; set; }
    public string? TrainIndexCombined { get; set; }
    [Required]
    public int TrainIndex { get; set; }
    [Required]
    public string? FromStationName { get; set; }
    [Required]
    public string? ToStationName { get; set; }
    public virtual ICollection<Stop> PassedStops { get; set; } = new List<Stop>();
}