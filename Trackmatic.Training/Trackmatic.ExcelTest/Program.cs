using System;
using System.Collections.Generic;
using Trackmatic.Excel;

namespace Trackmatic.ExcelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var headings = new List<string> { "Entity Name", "Entity Reference" };
            var collection = new List<string[]>() { new string[] { "Jhon", "sdds621wd" } };
            var Amend = new List<string[]>() { new string[] { "Jane", "fnshbsfkjdf55" } };
            var fileDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var fileName = "ExcelWirteTest";
            var Excel = new WriteToExcel();
            Excel.WriteToSheet(headings, collection, "Sheet1");
            Excel.Save(fileName, fileDirectory);
            Excel.AmmendToSheet(Amend);
            Excel.Save();
            Excel.ChangeSheet("Nooooo");
            Excel.dispose();
            Console.Read();
        }
    }
}
