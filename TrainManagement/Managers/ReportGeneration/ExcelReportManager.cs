using OfficeOpenXml;
using OfficeOpenXml.Style;
using TrainManagement.Dtos;

namespace TrainManagement.Managers;

public class ExcelReportManager : IReportFileGenerator
{
    private const string TEMPLATE_PATH = "./InitialData/NL_Template.xlsx";
    private const int CAR_ROW_OFFSET = 7;
    private const string DATA_FORMAT = "dd.MM.yyyy";
    private const string WEIGHT_FORMAT = "0.00";

    public async Task<byte[]> GetAsByteArray(TrainReport trainReport)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        
        using (var pkg = new ExcelPackage(TEMPLATE_PATH))
        {
            var worksheet = pkg.Workbook.Worksheets[0];
            
            // info on train
            worksheet.Cells[3, 3].Value = trainReport.TrainNumber;
            worksheet.Cells[4, 3].Value = trainReport.TrainIndex;
            worksheet.Cells[3, 5].Value = trainReport.StationName;
            worksheet.Cells[4, 5].Value = trainReport.Date;
            worksheet.Cells[4, 5].Style.Numberformat.Format = DATA_FORMAT;
            var row = CAR_ROW_OFFSET;
            
            // info on cars
            foreach (var car in trainReport.Cars)
            {
                worksheet.Rows[row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 1].Value = row - CAR_ROW_OFFSET + 1;
                worksheet.Cells[row, 2].Value = car.CarNumber;
                worksheet.Cells[row, 3].Value = car.InvoiceNum;
                worksheet.Cells[row, 4].Value = car.Date;
                worksheet.Cells[row, 4].Style.Numberformat.Format = DATA_FORMAT;
                worksheet.Cells[row, 5].Value = car.FreightEtsngName;
                worksheet.Cells[row, 6].Value = car.FreightTotalWeightTons;
                worksheet.Cells[row, 6].Style.Numberformat.Format = WEIGHT_FORMAT;
                worksheet.Cells[row, 7].Value = car.OperationName;
                row++;
            }

            // summary on distinct cargo
            foreach (var freight in trainReport.Freights)
            {
                worksheet.Rows[row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Rows[row].Style.Font.Bold = true;
                worksheet.Cells[row, 2].Value = freight.Value.Count;
                worksheet.Cells[row, 5].Value = freight.Key;
                worksheet.Cells[row, 6].Value = freight.Value.TotalWeightTons;
                worksheet.Cells[row, 6].Style.Numberformat.Format = WEIGHT_FORMAT;
                row++;
            }

            // summary on all cargo
            worksheet.Rows[row].Style.Font.Bold = true;
            worksheet.Rows[row].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[row, 1, row, 2].Merge = true;
            worksheet.Cells[row, 1].Value = $"Всего: {trainReport.CarsCount}";
            worksheet.Cells[row, 5].Value = trainReport.DifferentFreightCount;
            worksheet.Cells[row, 6].Value = trainReport.TotalWeightTons;
            worksheet.Cells[row, 6].Style.Numberformat.Format = WEIGHT_FORMAT;

            worksheet.Columns[5, 7].AutoFit();
            
            return await pkg.GetAsByteArrayAsync();
        }
    }
}