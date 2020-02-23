using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MailSender.Infrastructure.Services.Interfaces;
using MailSender.Library.Entities.Base;

namespace MailSender.Infrastructure.Services
{
    public abstract class OpenXmlEntityExport<T> : IEntityExport<T> where T : BaseEntity
    {
        public void Export(IEnumerable<T> items)
        {
            var filePath = GetFilePath();

            if (string.IsNullOrWhiteSpace(filePath)) return;

            // Создание документа электронной таблицы по указанному пути файла
            // По умолчанию, AutoSave = true, Editable = true, и Type = xlsx
            var spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

            // Добавление части книги в документ
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            // Добавление части листа в часть книги
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Добавление листов в книгу
            var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            // Добавление листа и связывание его с книгой
            var sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Получатели" };
            sheets.Append(sheet);

            // Получене листа данных таблицы ячеек
            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            FillRows(sheetData, items);

            // Закрытие документа
            spreadsheetDocument.Close();
        }

        /// <summary>
        ///     Заполнение данными
        /// </summary>
        /// <param name="sheetData">Лист данных</param>
        /// <param name="items">Коллекция данных</param>
        public abstract void FillRows(SheetData sheetData, IEnumerable<T> items);

        /// <summary>
        ///     Получение полного пути файла
        /// </summary>
        /// <returns></returns>
        public abstract string GetFilePath();
    }
}
