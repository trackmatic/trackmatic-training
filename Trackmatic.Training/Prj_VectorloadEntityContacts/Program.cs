using System;
using System.Collections.Generic;

namespace Prj_VectorloadEntityContacts
{
    class Program
    {
        static void Main(string[] args)
        {
            var VectorID = new List<string>() { "212", "213", "300", "216", "217", "266", "202", "221", "218", "219", "214", "220", "215", "222", "223", "349", "297", "204" };
            var VectorHeadings = new List<string>() { "First Name", "Last Name", "Tel No", "Cell No", "Email", "Adn", "Adn Email", "Adn Sms", "Entity Name", "Entity Refernce" };
            var fileName = "Vector";
            var fileDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var Entities = new List<EntityAndContactModel>();

            foreach (var ID in VectorID)
            {
                var entityLookUp = new EntityLookup(ID);
                var LoadedEntity = entityLookUp.PullData();
                Entities.AddRange(LoadedEntity);
            }

            var writeFile = new WriteToExcel(Entities, fileName, VectorHeadings);
            writeFile.Write(fileDirectory);
        }
    }
}
