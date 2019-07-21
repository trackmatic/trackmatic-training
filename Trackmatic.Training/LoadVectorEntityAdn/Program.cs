using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackmatic.Excel;

namespace LoadVectorEntityAdn
{
    class Program
    {
        static void Main(string[] args)
        {
            var VectorID = new List<string>() { "578" };
            var VectorHeadings = new List<string>() { "First Name", "Last Name", "Tel No", "Cell No", "Email", "Adn", "Adn Email", "Adn Sms", "Entity Name", "Entity Refernce", "Location Name", "Location City", "Client ID" };
            var fileName = "Unitrans EntitConatcts";
            var fileDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var Entities = new List<EntityAndContactModel>();
            var compareID = getCompareId();

            foreach (var ID in VectorID)
            {
                var entityLookUp = new EntityLookup(ID, compareID);
                var LoadedEntity = entityLookUp.PullData();
                Entities.AddRange(LoadedEntity);
            }

            var Write = new WriteToFile(VectorHeadings, fileName, fileDirectory, Entities);
            Write.Write();

        }

        public static List<string> getCompareId()
        {
            var CompareId = new List<string>();
            var openFile = new WriteToExcel();
            openFile.Open("Nandos contact list 25.10.2018", $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp", "owssvr (1)");
            var importCellsRef = openFile.WorkSheet.Cells["A2:A"];
            foreach (var Cell in importCellsRef)
            {
                if (Cell.GetValue<string>() != "")
                {
                    CompareId.Add(Cell.GetValue<string>());
                }
            }

            return CompareId;
        }
    }
}
