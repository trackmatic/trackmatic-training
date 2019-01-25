using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prj_VectorloadEntityContacts
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
                oXL.Visible = true;
                
                var oWB = (_Workbook)(oXL.Workbooks.Add(""));
                var oSheet = (_Worksheet)oWB.ActiveSheet;

                oSheet.Cells[1, 1] = "FirstName";
                oSheet.Cells[1, 2] = "LastName";
                oSheet.Cells[1, 3] = "TelNo";
                oSheet.Cells[1, 4] = "CellNo";
                oSheet.Cells[1, 5] = "Email";
                oSheet.Cells[1, 6] = "PositionHeld";
                oSheet.Cells[1, 7] = "Entity ID";
                
                oSheet.get_Range("A1", "G1").Font.Bold = true;
                oSheet.get_Range("A1", "G1").VerticalAlignment = XlVAlign.xlVAlignCenter;

                var Value = new List<string[]>();
                foreach (var Entity in Entities)
                {
                    if (Entity.Contact.Count != 0)
                    {
                        foreach (var Contact in Entity.Contact)
                        {
                            Value.Add(new string[] { Contact.FirstName, Contact.LastName, Contact.TelNo, Contact.CellNo, Contact.Email, Contact.PositionHeld, Entity.ID });
                        }
                    }
                }
                var arrValue = new string[Value.Count, 7];
                for(int i = 0; i<Value.Count; i++)
                {
                    arrValue[i, 0] = Value[i][0];
                    arrValue[i, 1] = Value[i][1];
                    arrValue[i, 2] = Value[i][2];
                    arrValue[i, 3] = Value[i][3];
                    arrValue[i, 4] = Value[i][4];
                    arrValue[i, 5] = Value[i][5];
                    arrValue[i, 6] = Value[i][6];
                }

                if (Value.Count != 0)
                {
                    oSheet.get_Range("A2", "G" + (Entities.Count - 1) + Entities.Count).Value2 = arrValue;
                }
                oSheet.Cells.Replace("#N/A", "");

                
                var oRng = oSheet.get_Range("A1", "G1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp/{_fileName}.xls", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
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
