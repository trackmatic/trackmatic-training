using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SerialiserConsoleApp.Models
{
    [DataContract(Name = "Address", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Template")]
    public class Address
    {
        [DataMember(Order = 1)]
        public string BuildingName { get; set; }
        [DataMember(Order = 2)]
        public string City { get; set; }
        [DataMember(Order = 3)]
        public string MapCode { get; set; }
        [DataMember(Order = 4)]
        public string Province { get; set; }
        [DataMember(Order = 5)]
        public string PostalCode { get; set; }
        [DataMember(Order = 6)]
        public string Street { get; set; }
        [DataMember(Order = 7)]
        public string StreetNo { get; set; }
        [DataMember(Order = 8)]
        public string SubDivisionNumber { get; set; }
        [DataMember(Order = 9)]
        public string Suburb { get; set; }
        [DataMember(Order = 10)]
        public string UnitNo { get; set; }
    }
}
