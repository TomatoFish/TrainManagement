using System.ComponentModel.DataAnnotations;

namespace TrainManagement.Models;

public class Car
{
    [Key]
    public long Id { get; set; }
    [Required]
    public string? InvoiceNum { get; set; }
    [Required]
    public int PositionInTrain { get; set; }
    [Required]
    public long CarNumber { get; set; }
    [Required]
    public DateTime WhenLastOperation { get; set; }
    [Required]
    public string? LastOperationName { get; set; }
    [Required]
    public string? FreightEtsngName { get; set; }
    [Required]
    public int FreightTotalWeightKg { get; set; }
    [Required]
    public long ParentStopId { get; set; }
    [Required]
    public Stop? ParentStop { get; set; }
    
    public float FreightTotalWeightTons => (float) FreightTotalWeightKg / 1000;
}