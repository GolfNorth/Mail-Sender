using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities;
using Microsoft.Win32;

namespace MailSender.Infrastructure.Services
{
    public class OpenXmlRecipientsExportToExcel : IEntityExportToExcel<Recipient>
    {
        public void ExportToExcel(IEnumerable<Recipient> recipients)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel файл (*.xlsx)|*.xlsx",
                FileName = "Recipients"
            };

            if (saveFileDialog.ShowDialog() == false) return;

            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            var spreadsheetDocument = SpreadsheetDocument.Create(saveFileDialog.FileName, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Получатели" };
            sheets.Append(sheet);

            // Get the sheetData cell table.
            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            // Add a row to the cell table.
            var row = new Row();

            row.Append(new Cell()
            {
                CellValue = new CellValue("ID"),
                DataType =  new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell()
            {
                CellValue = new CellValue("Имя"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell()
            {
                CellValue = new CellValue("Адрес"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });

            sheetData.Append(row);

            foreach (var recipient in recipients)
            {
                var newRow = new Row();

                newRow.Append(new Cell()
                {
                    CellValue = new CellValue(recipient.Id.ToString()),
                    DataType = new EnumValue<CellValues>(CellValues.Number)
                });
                newRow.Append(new Cell()
                {
                    CellValue = new CellValue(recipient.Name),
                    DataType = new EnumValue<CellValues>(CellValues.String)
                });
                newRow.Append(new Cell()
                {
                    CellValue = new CellValue(recipient.Address),
                    DataType = new EnumValue<CellValues>(CellValues.String)
                });

                sheetData.Append(newRow);
            }

            // Close the document.
            spreadsheetDocument.Close();
        }
    }
}
