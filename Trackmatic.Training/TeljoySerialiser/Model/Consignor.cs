using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Consignor", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Consignor
    {
        [DataMember(Order = 0, IsRequired = true)]
        public string Id { get; set; }
        [DataMember(Order = 1, IsRequired = true)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public bool IsAdHoc { get; set; }
    }
}
