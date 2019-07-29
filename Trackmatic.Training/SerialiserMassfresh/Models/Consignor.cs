using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Massfresh.Models
{
    [DataContract(Name = "Consignor", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Massfresh")]
    public class Consignor
    {
        [DataMember(Order = 1)]
        public Address Address { get; set; }
        [DataMember(Order = 2)]
        public List<Contact> Contacts { get; set; }
        [DataMember(Order = 3)]
        public string Name { get; set; }
        [DataMember(Order = 4)]
        public string LegalName { get; set; }
        [DataMember(Order = 5)]
        public string Reference { get; set; }
        [DataMember(Order = 6)]
        public string VAT { get; set; }
    }
}
