using System;
using System.Collections.Generic;

namespace Prj_VectorloadEntityContacts
{
    class Program
    {
        static void Main(string[] args)
        {
            var VectorID = new List<string>() { "578" };
            var VectorHeadings = new List<string>() { "First Name", "Last Name", "Tel No", "Cell No", "Email", "Adn", "Adn Email", "Adn Sms", "Entity Name", "Entity Refernce" };
            var reference = new List<string>(){};
            var fileName = "KZN Secondary";
            var fileDirectory = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/Temp";
            var Entities = new List<EntityAndContactModel>();

            foreach (var ID in VectorID)
            {
                var entityLookUp = new EntityLookup(ID, reference);
                var LoadedEntity = entityLookUp.PullData();
                Entities.AddRange(LoadedEntity);
            }

            var writeFile = new WriteToExcel(Entities, fileName, VectorHeadings);
            writeFile.Write(fileDirectory);
        }

    }
}
