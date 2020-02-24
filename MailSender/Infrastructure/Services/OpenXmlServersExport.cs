using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using MailSender.Library.Entities;
using Microsoft.Win32;

namespace MailSender.Infrastructure.Services
{
    internal class OpenXmlServersExport : OpenXmlEntityExport<Server>
    {
        public override void FillRows(SheetData sheetData, IEnumerable<Server> servers)
        {
            var row = new Row();

            row.Append(new Cell
            {
                CellValue = new CellValue("ID"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell
            {
                CellValue = new CellValue("Имя"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell
            {
                CellValue = new CellValue("Хост"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell
            {
                CellValue = new CellValue("Порт"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell
            {
                CellValue = new CellValue("SSL"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell
            {
                CellValue = new CellValue("Логин"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });
            row.Append(new Cell
            {
                CellValue = new CellValue("Пароль"),
                DataType = new EnumValue<CellValues>(CellValues.String)
            });

            sheetData.Append(row);

            foreach (var server in servers)
            {
                var newRow = new Row();

                newRow.Append(new Cell
                {
                    CellValue = new CellValue(server.Id.ToString()),
                    DataType = new EnumValue<CellValues>(CellValues.Number)
                });
                newRow.Append(new Cell
                {
                    CellValue = new CellValue(server.Name),
                    DataType = new EnumValue<CellValues>(CellValues.String)
                });
                newRow.Append(new Cell
                {
                    CellValue = new CellValue(server.Host),
                    DataType = new EnumValue<CellValues>(CellValues.String)
                });
                newRow.Append(new Cell
                {
                    CellValue = new CellValue(server.Port.ToString()),
                    DataType = new EnumValue<CellValues>(CellValues.Number)
                });
                newRow.Append(new Cell
                {
                    CellValue = new CellValue(server.EnableSsl.ToString()),
                    DataType = new EnumValue<CellValues>(CellValues.Boolean)
                });
                newRow.Append(new Cell
                {
                    CellValue = new CellValue(server.Login),
                    DataType = new EnumValue<CellValues>(CellValues.String)
                });
                newRow.Append(new Cell
                {
                    CellValue = new CellValue(server.Password),
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
                FileName = "Servers"
            };

            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : string.Empty;
        }
    }
}