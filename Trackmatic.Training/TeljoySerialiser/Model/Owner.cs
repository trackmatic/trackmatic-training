using System.Runtime.Serialization;

namespace TeljoySerialiser.Model
{
    [DataContract(Name = "Owner", Namespace = "http://wwww.trackmatic.co.za/schema/Integration/Teljoy")]
    public class Owner
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }
        [DataMember(Order = 1)]
        public string Reference { get; set; }
    }
}
