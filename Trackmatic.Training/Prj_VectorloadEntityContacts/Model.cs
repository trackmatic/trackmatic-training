using Trackmatic.Rest.Routing.Model;
using System.Collections.Generic;

namespace Prj_VectorloadEntityContacts
{
    public class Model
    {
        public Model(string iD, List<EntityContact> contact)
        {
            ID = iD;
            Contact = contact;
        }

        public string ID { get; set; }
        public List<EntityContact> Contact { get; set; }


    }
}
