using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trackmatic.Excel
{
    public class WriteToExcel : ExcelFile
    {
        public WriteToExcel(){}

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
    }
}
