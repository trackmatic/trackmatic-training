using System;
using System.Collections.Generic;

namespace LoadEntityAndEntityLoaction
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientID = "110";
            var fileName = "EntitiesLocation";
            var directory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var clientheadings = new List<string> { "Entity Name", "Entity Reference", "Loaction Name", "Location Reference" };
            var entitiesLoactions = FecthEntitiesAndLoactions(clientID);
            WriteToFile(entitiesLoactions, fileName, clientheadings, directory);

        }

        private static List<EntityAndLocationModel> FecthEntitiesAndLoactions(string clientID)
        {
            var entityLoactionLookUp = new EntityAndLocationLookup(clientID);
            return entityLoactionLookUp.PullData();
        }

        private static void WriteToFile(List<EntityAndLocationModel> entitiesLoactions, string fileName, List<string> headings, string directory)
        {
            var writeFile = new WriteToExcel(entitiesLoactions, fileName, headings);
            writeFile.Write(directory);
        }

    }
}
