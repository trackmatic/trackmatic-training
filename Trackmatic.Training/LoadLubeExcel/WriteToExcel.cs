using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoadLubeExcel
{
    public class WriteToExcel
    {
        public WriteToExcel(List<Model> entities, string fileName)
        {
            Entities = entities;
            _fileName = fileName;
        }

        private List<Model> Entities { get; set; }
        private string _fileName { get; set; }

        public void Write()
        {
            try
            {
                var oXL = new Application();

                var oWB = (_Workbook)(oXL.Workbooks.Add(""));
                var oSheet = (_Worksheet)oWB.ActiveSheet;

                oSheet.Cells[1, 1] = "Entity ID";
                oSheet.Cells[1, 2] = "Entity Reference";
                oSheet.Cells[1, 3] = "Entity Name";

                oSheet.get_Range("A1", "C1").Font.Bold = true;
                oSheet.get_Range("A1", "C1").VerticalAlignment = XlVAlign.xlVAlignCenter;

                var Value = new string[Entities.Count, 3];
                for(int i = 0; i<Entities.Count-1; i++)
                {
                    Value[i, 0] += Entities.ElementAt(i).ID;
                    Value[i, 1] += Entities.ElementAt(i).Ref;
                    Value[i, 2] += Entities.ElementAt(i).Name;
                }

                oSheet.get_Range("A2", "C"+(Entities.Count-1)).Value2 = Value;
                oSheet.Cells.Replace("#N/A", "");

                var oRng = oSheet.get_Range("A1", "C1");
                oRng.EntireColumn.AutoFit();

                oWB.SaveAs($"{Environment.CurrentDirectory}/{_fileName}.xls", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, XlSaveAsAccessMode.xlNoChange);
                oWB.Close();
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
