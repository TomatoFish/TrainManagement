namespace TrainManagement.Dtos;

public class CarReport
{
    public int PositionInTrain { get; set; }
    public long CarNumber { get; set; }
    public string? InvoiceNum { get; set; }
    public DateTime Date { get; set; }
    public string? FreightEtsngName { get; set; }
    public float FreightTotalWeightTons { get; set; }
    public string? OperationName { get; set; }
}