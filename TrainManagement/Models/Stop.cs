using System.ComponentModel.DataAnnotations;

namespace TrainManagement.Models;

public class Stop
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string? StationName { get; set; }
    
    public long ParentTrainId { get; set; }
    
    public Train ParentTrain { get; set; }
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}