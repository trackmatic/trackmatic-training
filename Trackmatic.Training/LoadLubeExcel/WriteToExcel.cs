using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoadLubeExcel
{
    public class WriteToExcel
    {
        public WriteToExcel(List<EntityModel> entities, string fileName, List<string> headings)
        {
            Entities = entities;
            _fileName = fileName;
            Headings = headings;
        }

        private List<EntityModel> Entities { get; set; }
        private string _fileName { get; set; }
        private List<string> Headings { get; set; }

        public void Write(string directory)
        {
            try
            {
                var openExcelApp = new Application();

                var openWorkBook = (_Workbook)(openExcelApp.Workbooks.Add(""));
                var openSheet = (_Worksheet)openWorkBook.ActiveSheet;
                var lastCollumn = 'A';
                for (int i = 1; i <= Headings.Count; i++)
                {
                    openSheet.Cells[1, i] = Headings.ElementAt(i - 1);
                    lastCollumn++;
                }
                lastCollumn--;

                openSheet.get_Range("A1", lastCollumn + "1").Font.Bold = true;
                openSheet.get_Range("A1", lastCollumn + "1").VerticalAlignment = XlVAlign.xlVAlignCenter;

                var Value = new string[Entities.Count, Headings.Count];
                for (int i = 0; i < Entities.Count - 1; i++)
                {
                    Value[i, 0] += Entities.ElementAt(i).Name;
                    Value[i, 1] += Entities.ElementAt(i).Reference;
                }

                openSheet.get_Range("A2", lastCollumn + "" + (Entities.Count - 1)).Value2 = Value;
                openSheet.Cells.Replace("#N/A", "");

                var oRng = openSheet.get_Range("A1", lastCollumn + "1");
                oRng.EntireColumn.AutoFit();

                openWorkBook.SaveAs($"{directory}/{_fileName}", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, XlSaveAsAccessMode.xlNoChange);
                openWorkBook.Close();
            }
            catch (Exception theException)
            {
                var errorMessage = string.Empty;
                errorMessage = $"Error: {theException.Message} Line: {theException.Source}";
                Console.WriteLine(errorMessage);
            }
        }
    }
}
