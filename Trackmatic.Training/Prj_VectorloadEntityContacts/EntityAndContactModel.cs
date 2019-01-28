using Trackmatic.Rest.Routing.Model;
using System.Collections.Generic;

namespace Prj_VectorloadEntityContacts
{
    public class EntityAndContactModel
    {
        public EntityAndContactModel(string name, string reference, List<EntityContact> contact)
        {
            Name = name;
            Reference = reference;
            Contact = contact;
        }

        public string Name { get; set; }
        public string Reference { get; set; }
        public List<EntityContact> Contact { get; set; }


    }
}
