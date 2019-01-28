using System;
using System.Collections.Generic;

namespace LoadEntityAndEntityLoaction
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientId = "556";
            var fileName = "EntitiesLocation";
            var directory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var clientheadings = new List<string> { "Entity Name", "Entity Reference", "Loaction Name", "Location Reference" };
            var entitiesLoactions = FecthEntitiesAndLoactions(clientId);
            WriteToFile(entitiesLoactions, fileName, clientheadings, directory);
        }

        private static List<EntityAndLocationModel> FecthEntitiesAndLoactions(string clientId)
        {
            var entityLoactionLookUp = new EntityAndLocationLookup(clientId);
            return entityLoactionLookUp.PullData();
        }

        private static void WriteToFile(List<EntityAndLocationModel> entitiesLoactions, string fileName, List<string> headings, string directory)
        {
            var writeFile = new WriteToExcel(entitiesLoactions, fileName, headings, directory);
            writeFile.Write();
        }
    }
}
