using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

namespace Trackmatic.Excel
{
    public class ExcelFile
    {
        public ExcelPackage Excel { get; set; }
        public ExcelWorksheet WorkSheet { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public ExcelPackage CreateDocument()
        {
            return Excel = new ExcelPackage();
        }

        public ExcelWorksheet CreateWorkSheet(string sheetName)
        {
            if (Excel == null) CreateDocument();
            return WorkSheet = Excel.Workbook.Worksheets.Add(sheetName);
        }


        public void Save(string fileName, string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            FileName = fileName + DateTime.Now.ToString("_yyyy-MM-dd_HH-mm-ss");
            Path = path;
            using (var stream = File.Create($"{Path}/{FileName}.xlsx"))
            {
                Excel.SaveAs(stream);
            }
            Console.WriteLine($"{FileName} saved to {Path}/{FileName}");
        }
        public void Save()
        {
            if (!File.Exists($"{Path}/{FileName}.xlsx")) Console.WriteLine("File does not exsist");
            else
            {

                using (var stream = File.Create($"{Path}/{FileName}.xlsx"))
                {
                    Excel.SaveAs(stream);
                }
                Console.WriteLine($"{FileName} saved to {Path}/{FileName}");
            }
        }

        public void Open(string fileName, string path, string sheetName)
        {
            if (!File.Exists($"{path}/{fileName}.xlsx")) Console.WriteLine("File does not exsist");
            else
            {
                FileName = fileName;
                Path = path;
                using (var stream = File.Open($"{path}/{fileName}.xlsx", FileMode.Open))
                {
                    Excel = new ExcelPackage(stream);
                    ChangeSheet(sheetName);
                }
            }
        }

        public void ChangeSheet(string sheetName)
        {
            if (Excel == null || Excel.Workbook.Worksheets == null || !Excel.Workbook.Worksheets.Contains(Excel.Workbook.Worksheets[sheetName]))
                Console.WriteLine("No Excel package or sheet found");
            else
            {
                WorkSheet = Excel.Workbook.Worksheets[sheetName];
            }
        }

        public void dispose()
        {
            if (Excel != null) Excel.Dispose();
            if (WorkSheet != null) WorkSheet.Dispose();
        }
    }
}
