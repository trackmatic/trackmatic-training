using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prj_VectorloadEntityContacts
{
    public class WriteToExcel
    {
        public WriteToExcel(List<EntityAndContactModel> entities, string fileName, List<string> headings)
        {
            Entities = entities;
            _fileName = fileName;
            Headings = headings;
        }

        private List<EntityAndContactModel> Entities { get; set; }
        private string _fileName { get; set; }
        private List<string> Headings { get; set; }

        public void Write(string directory)
        {
            try
            {
                var openExcelApp = new Application();
                openExcelApp.Visible = true;

                var openWorkBook = (_Workbook)(openExcelApp.Workbooks.Add(""));
                var openSheet = (_Worksheet)openWorkBook.ActiveSheet;
                var lastCollumn = 'A';
                for (int i = 1; i <= Headings.Count; i++)
                {
                    openSheet.Cells[1, i] = Headings.ElementAt(i - 1);
                    lastCollumn++;
                }
                lastCollumn--;

                openSheet.get_Range("A1", lastCollumn + "1").Font.Bold = true;
                openSheet.get_Range("A1", lastCollumn + "1").VerticalAlignment = XlVAlign.xlVAlignCenter;

                var Value = new List<string[]>();
                foreach (var Entity in Entities)
                {
                    if (Entity.Contact.Count != 0)
                    {
                        foreach (var Contact in Entity.Contact)
                        {
                            foreach (var AdnConfig in Contact.AdnConfiguration.Types)
                            {
                                Value.Add(new string[] { Contact.FirstName, Contact.LastName, Contact.TelNo, Contact.CellNo, Contact.Email, AdnConfig, Contact.AdnConfiguration.Email.ToString(), Contact.AdnConfiguration.Sms.ToString(), Entity.Name, Entity.Reference });
                            }
                        }
                    }
                }
                var arrValue = new string[Value.Count, Headings.Count];
                for (int i = 0; i < Value.Count; i++)
                {
                    for (int j = 0; j <= Headings.Count - 1; j++)
                    {
                        arrValue[i, j] = Value[i][j];
                    }
                }

                if (Value.Count != 0)
                {
                    openSheet.get_Range("A2", lastCollumn + "" + (Entities.Count - 1)).Value2 = arrValue;
                }
                openSheet.Cells.Replace("#N/A", "");


                var oRng = openSheet.get_Range("A1", lastCollumn + "1");
                oRng.EntireColumn.AutoFit();

                openExcelApp.Visible = false;
                openExcelApp.UserControl = false;
                openWorkBook.SaveAs($"{directory}/{_fileName}", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, XlSaveAsAccessMode.xlNoChange);
                openWorkBook.Close();

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
