using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoadEntityAndEntityLoaction
{
    public class WriteToExcel
    {
        public WriteToExcel(List<EntityAndLocationModel> entities, string _fileName, List<string> headings)
        {
            Entities = entities;
            this._fileName = _fileName;
            Headings = headings;
        }

        private List<EntityAndLocationModel> Entities { get; set; }
        private string _fileName { get; set; }
        private List<string> Headings { get; set; }

        public void Write(string directory)
        {
            var excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            for (int i = 1; i <= Headings.Count; i++)
            {
                workSheet.Cells[1, i].Value = Headings.ElementAt(i-1);
                workSheet.Column(i).AutoFit();
            }

            for (int i = 2; i <= Entities.Count+1; i++)
            {
                workSheet.Cells[i, 1].Value = Entities.ElementAt(i - 2).EntityName;
                workSheet.Cells[i, 2].Value = Entities.ElementAt(i - 2).EntityRefernce;
                workSheet.Cells[i, 3].Value = Entities.ElementAt(i - 2).LocationName;
                workSheet.Cells[i, 4].Value = Entities.ElementAt(i - 2).LocationReference;
            }

            Stream stream = File.Create($"{directory}/{_fileName}.xlsx");
            excel.SaveAs(stream);
            stream.Close();
        }
    }
}
