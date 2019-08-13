using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{

    [DataContract(Name = "Consignee", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Consignee
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string Id { get; set; }
        [DataMember(Order = 1, IsRequired = true)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string LegalName { get; set; }
        [DataMember(Order = 3)]
        public string Registration { get; set; }
        [DataMember(Order = 4)]
        public string VAT { get; set; }
        [DataMember(Order = 5)]
        public Address Address { get; set; }
        [DataMember(Order = 6)]
        public List<Contact> Contacts { get; set; }
        [DataMember(Order = 7)]
        public int FixedMst { get; set; }
        [DataMember(Order = 8)]
        public int VariableMSt { get; set; }
        [DataMember(Order = 9)]
        public bool IsAdHoc { get; set; }
        [DataMember(Order = 10)]
        public int Sequence { get; set; }
    }
}
