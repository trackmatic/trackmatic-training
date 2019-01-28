using System;
using System.Collections.Generic;

namespace LoadLubeExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            var ClientID = "579";
            var ClientHeadings = new List<string>() { "Entity Name", "Entity Refernce" };
            var fileName = "LubeMarketing";
            var fileDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var entityLookUp = new EntityLookup(ClientID);
            var entities = entityLookUp.PullData();
            var writeFile = new WriteToExcel(entities, fileName, ClientHeadings);
            writeFile.Write(fileDirectory);
        }
    }
}
