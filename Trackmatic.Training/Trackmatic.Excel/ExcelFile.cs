using OfficeOpenXml;
using System;
using System.IO;

namespace Trackmatic.Excel
{
    public class ExcelFile
    {
        public ExcelPackage Excel { get; set; }
        public ExcelWorksheet WorkSheet { get; set; }
        public ExcelPackage CreateDocument()
        {
            return Excel = new ExcelPackage();
        }

        public ExcelWorksheet CreateWorkSheet(string sheetName)
        {
            if (Excel == null) CreateDocument();
            return WorkSheet = Excel.Workbook.Worksheets.Add(sheetName);
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
                    ChangeSheet(sheetName);
                }
            }
        }

        public void ChangeSheet(string sheetName)
        {
            if (Excel == null || Excel.Workbook.Worksheets == null) Console.WriteLine("No Excel package or sheets");
            else
            {
                WorkSheet = Excel.Workbook.Worksheets[sheetName];
            }
        }

        public void dispose()
        {
            Excel.Dispose();
            WorkSheet.Dispose();
        }
    }
}
