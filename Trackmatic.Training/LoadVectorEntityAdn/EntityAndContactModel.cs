using Trackmatic.Rest.Routing.Model;
using System.Collections.Generic;

namespace LoadVectorEntityAdn
{
    public class EntityAndContactModel
    {
        public EntityAndContactModel(string name, string reference, List<EntityContact> contact, string clientId, List<DecoAlias> deco)
        {
            Name = name;
            Reference = reference;
            Contact = contact;
            ClientId = clientId;
            Deco = deco;

        }

        public string Name { get; set; }
        public string Reference { get; set; }
        public List<EntityContact> Contact { get; set; }
        public string ClientId { get; set; }
        public List<DecoAlias> Deco { get; set; }


    }
}
