using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Consignee", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Consignee
    {
        [DataMember(Order = 1)]
        public Address Address { get; set; }
        [DataMember(Order = 2)]
        public List<Contact> Contacts { get; set; }
        [DataMember(Order = 3)]
        public string MST { get; set; }
        [DataMember(Order = 4)]
        public string Name { get; set; }
        [DataMember(Order = 5)]
        public string LegalName { get; set; }
        [DataMember(Order = 6)]
        public Position Position { get; set; }
        [DataMember(Order = 7)]
        public string Reference { get; set; }
        [DataMember(Order = 8)]
        public string VAT { get; set; }
        [DataMember(Order = 9)]
        public string ConsigneeCode { get; set; }
    }
}
