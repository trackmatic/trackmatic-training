using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Address", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Address
    {
        [DataMember(Order = 0)]
        public string UnitNo { get; set; }
        [DataMember(Order = 1)]
        public string BuildingName { get; set; }
        [DataMember(Order = 2)]
        public string StreetNo { get; set; }
        [DataMember(Order = 3)]
        public string SubDivisionNumber { get; set; }
        [DataMember(Order = 4)]
        public string StreetName { get; set; }
        [DataMember(Order = 5)]
        public string Suburb { get; set; }
        [DataMember(Order = 6)]
        public string City { get; set; }
        [DataMember(Order = 7)]
        public string PostalCode { get; set; }
        [DataMember(Order = 8)]
        public string Province { get; set; }
        [DataMember(Order = 9)]
        public string MapReference { get; set; }
        [DataMember(Order = 10)]
        public double Latitude { get; set; }
        [DataMember(Order = 11)]
        public double Longitude { get; set; }
    }
}
