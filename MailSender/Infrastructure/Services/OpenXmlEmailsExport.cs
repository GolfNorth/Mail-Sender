using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using MailSender.Library.Entities;
using Microsoft.Win32;

namespace MailSender.Infrastructure.Services
{
    public class OpenXmlEmailsExport : OpenXmlEntityExport<Email>
    {
        public override void FillRows(SheetData sheetData, IEnumerable<Email> emails)
        {
            // Добавление строки в таблицу ячеек
            var row = new Row();

            row.Append(new Cell()
            {
                CellValue = new CellValue("ID"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell()
            {
                CellValue = new CellValue("Заголовок"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell()
            {
                CellValue = new CellValue("Текст"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });

            sheetData.Append(row);

            foreach (var email in emails)
            {
                var newRow = new Row();

                newRow.Append(new Cell()
                {
                    CellValue = new CellValue(email.Id.ToString()),
                    DataType = new EnumValue<CellValues>(CellValues.Number)
                });
                newRow.Append(new Cell()
                {
                    CellValue = new CellValue(email.Subject),
                    DataType = new EnumValue<CellValues>(CellValues.String)
                });
                newRow.Append(new Cell()
                {
                    CellValue = new CellValue(email.Body),
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
                FileName = "Emails"
            };

            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : string.Empty;
        }
    }
}
