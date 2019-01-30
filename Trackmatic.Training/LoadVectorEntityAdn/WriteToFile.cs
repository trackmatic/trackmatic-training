using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Excel;

namespace LoadVectorEntityAdn
{
    class WriteToFile
    {
        public WriteToFile(List<string> headings, string fileName, string fileDirectory, List<EntityAndContactModel> collection)
        {
            Headings = headings;
            FileName = fileName;
            FileDirectory = fileDirectory;
            Collection = collection;
        }

        public List<EntityAndContactModel> Collection { get; set; }
        public List<string> Headings { get; set; }
        public string FileName { get; set; }
        public string FileDirectory { get; set; }

        public void Write()
        {
            var Value = new List<string[]>();
            foreach (var item in Collection)
            {
                foreach (var Contact in item.Contact)
                {
                    var city = string.Empty;
                    foreach (var deco in item.Deco)
                    {
                        city = (deco.StructuredAddress == null) ? "unknown" : deco.StructuredAddress.City;
                            Value.Add(new string[] { Contact.FirstName, Contact.LastName, Contact.TelNo, Contact.CellNo, Contact.Email, string.Join(",", Contact.AdnConfiguration.Types),
                        Contact.AdnConfiguration.Email.ToString(), Contact.AdnConfiguration.Sms.ToString(), item.Name, item.Reference, deco.Name, city, item.ClientId}); 
                    }
                }
            }
            var newFile = new WriteToExcel();
            newFile.WriteToSheet(Headings, Value);
            newFile.Save(FileName, FileDirectory);

        }
    }
}
