using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Trackmatic.Excel
{
    public class WriteToExcel
    {
        public ExcelPackage Excel { get; set; }
        public ExcelWorksheet WorkSheet { get; set; }
        public WriteToExcel() { }
        public ExcelPackage CreateDocument()
        {
            return Excel = new ExcelPackage();
        }

        public ExcelWorksheet CreateWorkSheet(string sheetName)
        {
            return WorkSheet = Excel.Workbook.Worksheets.Add(sheetName);
        }

        public void WriteToSheet(List<string> headings, List<string[]> collection, string sheetName = "Sheet1")
        {
            if (Excel == null) CreateDocument();
            if (WorkSheet == null) CreateWorkSheet(sheetName);
            WorkSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            WorkSheet.Row(1).Style.Font.Bold = true;

            for (int i = 1; i <= headings.Count; i++)
            {
                WorkSheet.Cells[1, i].Value = headings.ElementAt(i - 1);
                WorkSheet.Column(i).AutoFit();
            }

            for (int i = 2; i <= collection.Count + 1; i++)
            {
                var count = 1;
                foreach (var item in collection.ElementAt(i - 2))
                {
                    WorkSheet.Cells[i, count].Value = item;
                    count++;
                }
            }
        }
        public void AmmendToSheet(List<string[]> collection)
        {
            if (Excel == null) Console.WriteLine("No File Found");
            else
            {
                var end = WorkSheet.Dimension.End.Row+1;
                for (int i = end; i <= collection.Count + end-1; i++)
                {
                    var count = 1;
                    foreach (var item in collection.ElementAt(i - end))
                    {
                        WorkSheet.Cells[i, count].Value = item;
                        count++;
                    }
                }
            }
        }

        public void SaveAs(string fileName, string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (var stream = File.Create($"{path}/{fileName}.xlsx"))
            {
                Excel.SaveAs(stream);
            }
            Console.WriteLine($"{fileName} saved to {path}/{fileName}");
        }
        public void Save(string fileName, string path)
        {
            if (!Directory.Exists(path)) SaveAs(fileName, path);

            else
            {
                Excel.Save();
                SaveAs(fileName, path);
            }
        }

        public void Open(string fileName, string path, string sheetName)
        {
            if (!File.Exists($"{path}/{fileName}.xlsx")) Console.WriteLine("File does not exsist");

            else
            {
                using (var stream = File.Open($"{path}/{fileName}.xlsx", FileMode.Open))
                {
                    Excel = new ExcelPackage(stream);
                    WorkSheet = Excel.Workbook.Worksheets[sheetName];
                }
            }
        }

        public void dispose()
        {
            Excel.Dispose();
            WorkSheet.Dispose();
        }
    }
}
