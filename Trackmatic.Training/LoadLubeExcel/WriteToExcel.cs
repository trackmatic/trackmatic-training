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
            Write();
        }

        private List<Model> Entities { get; set; }
        private string _fileName { get; set; }

        private void Write()
        {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                //oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Entity ID";
                oSheet.Cells[1, 2] = "Entity Reference";
                oSheet.Cells[1, 3] = "Entity Name";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "C1").Font.Bold = true;
                oSheet.get_Range("A1", "C1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                var Value = new string[Entities.Count, 3];
                for(int i = 0; i<Entities.Count-1; i++)
                {
                    Value[i, 0] += Entities.ElementAt(i).ID;
                    Value[i, 1] += Entities.ElementAt(i).Ref;
                    Value[i, 2] += Entities.ElementAt(i).Name;
                }

                oSheet.get_Range("A2", "C"+(Entities.Count-1) + Entities.Count).Value2 = Value;
                oSheet.Cells.Replace("#N/A", "");


                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "C1");
                oRng.EntireColumn.AutoFit();

                //oXL.Visible = false;
                //oXL.UserControl = false;
                oWB.SaveAs($"{Environment.CurrentDirectory}/{_fileName}.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange);
                oWB.Close();

            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                Console.WriteLine(errorMessage, "Error");
            }
        }
    }
}
