using System;
using System.Collections.Generic;

namespace Prj_VectorloadEntityContacts
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] VectorID = { "212", "213", "300", "216", "217", "266", "202", "221", "218", "219", "214", "220", "215", "222", "223", "349", "297", "204" };
            var Entities = new List<Model>();
            foreach (var ID in VectorID)
            {
                var eLookUp = new EntityLookupAndMatch(ID);
                eLookUp.PullData();
                Entities.AddRange(eLookUp.Entities);
            }

            var writeE = new WriteToExcel(Entities, "Vector");
            writeE.Write();
        }
    }
}
