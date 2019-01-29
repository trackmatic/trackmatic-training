using System;
using System.Collections.Generic;
using Trackmatic.Excel;

namespace Trackmatic.ExcelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var headings = new string[] { "Entity Name", "Entity Reference" };
            var collection = new List<string[]>() { new string[] { "Jhon", "sdds621wd" } };
            var fileDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var fileName = "ExcelWirteTest";
            var Excel = new WriteToExcel();
            Excel.Open(fileName, fileDirectory, "Sheet1");
            Excel.AmmendToSheet(collection);
            Excel.Save(fileName, fileDirectory);

        }
    }
}
