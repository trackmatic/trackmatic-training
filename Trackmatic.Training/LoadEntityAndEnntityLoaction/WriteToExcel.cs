using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoadEntityAndEntityLoaction
{
    public class WriteToExcel
    {
        public WriteToExcel(List<EntityAndLocationModel> entities, string filename, List<string> headings, string path)
        {
            Entities = entities;
            Filename = filename;
            Headings = headings;
            Path = path;
            Excel = new ExcelPackage();
        }

        private List<EntityAndLocationModel> Entities { get; set; }
        private string Filename { get; set; }
        private List<string> Headings { get; set; }
        private string Path { get; set; }
        private ExcelPackage Excel { get; set; }

        public void Write()
        {
            var workSheet = Excel.Workbook.Worksheets.Add("Sheet1");
            
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

            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);

            using (var stream = File.Create($"{Path}/{Filename}.xlsx"))
            {
                Excel.SaveAs(stream);
            }
        }
    }
}
