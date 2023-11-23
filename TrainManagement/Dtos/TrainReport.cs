namespace TrainManagement.Dtos;

public class TrainReport
{
    public int TrainNumber { get; set; }
    public int? TrainIndex { get; set; }
    public string? StationName { get; set; }
    public DateTime Date { get; set; }
    public int CarsCount { get; set; }
    public float DifferentFreightCount { get; set; }
    public float TotalWeightTons { get; set; }
    public ICollection<CarReport> Cars { get; set; } = new List<CarReport>();
    public IDictionary<string, FreightReport> Freights { get; set; } = new Dictionary<string, FreightReport>();
}