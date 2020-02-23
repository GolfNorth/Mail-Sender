using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using MailSender.Library.Entities;
using Microsoft.Win32;

namespace MailSender.Infrastructure.Services
{
    public class OpenXmlSendersExport : OpenXmlEntityExport<Sender>
    {
        public override void FillRows(SheetData sheetData, IEnumerable<Sender> senders)
        {
            var row = new Row();

            row.Append(new Cell()
            {
                CellValue = new CellValue("ID"),
                DataType = new EnumValue<CellValues>(CellValues.String)
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

            foreach (var recipient in senders)
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
        }

        public override string GetFilePath()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel файл (*.xlsx)|*.xlsx",
                FileName = "Senders"
            };

            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : string.Empty;
        }
    }
}
