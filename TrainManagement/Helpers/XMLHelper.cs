using System.Globalization;
using System.Xml.Linq;
using TrainManagement.Dtos;

namespace TrainManagement.Helpers;

public static class XMLHelper
{
    private static readonly CultureInfo Culture = new CultureInfo("ru-RU");

    public static IEnumerable<CarReceive> ParseStream(Stream stream)
    {
        var xml = XDocument.Load(stream, LoadOptions.None);
        var query = xml.Root?.Elements();
        if (query != null)
        {
            var cars = new List<CarReceive>();
            foreach (var item in query)
            {
                var newCarReceive = GetCarReceive(item);
                cars.Add(newCarReceive);
            }

            return cars;
        }

        return new List<CarReceive>();
    }

    private static CarReceive GetCarReceive(XElement doc)
    {
        try
        {
            var trainNumber = int.Parse(doc.Element("TrainNumber").Value);
            var trainIndexCombined = doc.Element("TrainIndexCombined").Value;
            var trainIndex = int.Parse(trainIndexCombined.Split('-')[1]);
            var fromStationName = doc.Element("FromStationName").Value;
            var toStationName = doc.Element("ToStationName").Value;
            var lastStationName = doc.Element("LastStationName").Value;
            var whenLastOperation = DateTime.SpecifyKind(DateTime.Parse(doc.Element("WhenLastOperation").Value, Culture), DateTimeKind.Utc);
            var lastOperationName = doc.Element("LastOperationName").Value;
            var invoiceNum = doc.Element("InvoiceNum").Value;
            var positionInTrain = int.Parse(doc.Element("PositionInTrain").Value);
            var carNumber = long.Parse(doc.Element("CarNumber").Value);
            var freightEtsngName = doc.Element("FreightEtsngName").Value;
            var freightTotalWeightKg = int.Parse(doc.Element("FreightTotalWeightKg").Value);
            var newCarReceive = new CarReceive
            {
                TrainNumber = trainNumber, TrainIndex = trainIndex, TrainIndexCombined = trainIndexCombined,
                FromStationName = fromStationName, ToStationName = toStationName, LastStationName = lastStationName,
                WhenLastOperation = whenLastOperation, LastOperationName = lastOperationName, InvoiceNum = invoiceNum,
                PositionInTrain = positionInTrain, CarNumber = carNumber, FreightEtsngName = freightEtsngName,
                FreightTotalWeightKg = freightTotalWeightKg
            };
            return newCarReceive;
        }
        catch (FormatException e)
        {
            Console.WriteLine($"--> Can't parse document: {e}");
            throw;
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine($"--> Can't find field: {e}");
            throw;
        }
    }
}