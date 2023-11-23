namespace TrainManagement.Dtos;

public class CarReceive
{
    public int TrainNumber { get; set; }
    public int TrainIndex { get; set; }
    public string? TrainIndexCombined { get; set; }
    public string? FromStationName { get; set; }
    public string? ToStationName { get; set; }
    public string? LastStationName { get; set; }
    public DateTime WhenLastOperation { get; set; }
    public string? LastOperationName { get; set; }
    public string? InvoiceNum { get; set; }
    public int PositionInTrain { get; set; }
    public long CarNumber { get; set; }
    public string? FreightEtsngName { get; set; }
    public int FreightTotalWeightKg { get; set; }
}